namespace FastColoredTextBoxNS
{
    internal class InsertTextCommand : UndoableCommand
    {
        private string insertedText;

        public InsertTextCommand(TextSource ts, string insertedText) : base(ts)
        {
            this.insertedText = insertedText;
        }

        public override void Execute()
        {
            base.ts.OnTextChanging(ref this.insertedText);
            InsertText(this.insertedText, base.ts);
            base.Execute();
        }

        public override void Undo()
        {
            base.ts.CurrentTB.Selection.Start = base.sel.Start;
            base.ts.CurrentTB.Selection.End = base.lastSel.Start;
            base.ts.OnTextChanging();
            ClearSelectedCommand.ClearSelected(base.ts);
            base.Undo();
        }

        internal static void InsertText(string insertedText, TextSource ts)
        {
            FastColoredTextBox currentTB = ts.CurrentTB;
            try
            {
                currentTB.Selection.BeginUpdate();
                char deletedChar = '\0';
                if (ts.Count == 0)
                {
                    InsertCharCommand.InsertLine(ts);
                }
                currentTB.ExpandBlock(currentTB.Selection.Start.iLine);
                foreach (char ch2 in insertedText)
                {
                    InsertCharCommand.InsertChar(ch2, ref deletedChar, ts);
                }
                ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
            }
            finally
            {
                currentTB.Selection.EndUpdate();
            }
        }
    }
}