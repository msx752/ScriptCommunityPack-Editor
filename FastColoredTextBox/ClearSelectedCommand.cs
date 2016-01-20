namespace FastColoredTextBoxNS
{
    using System;

    internal class ClearSelectedCommand : UndoableCommand
    {
        private string deletedText;

        public ClearSelectedCommand(TextSource ts) : base(ts)
        {
        }

        public override void Execute()
        {
            FastColoredTextBox currentTB = base.ts.CurrentTB;
            string text = null;
            base.ts.OnTextChanging(ref text);
            if (text == "")
            {
                throw new ArgumentOutOfRangeException();
            }
            this.deletedText = currentTB.Selection.Text;
            ClearSelected(base.ts);
            base.lastSel = new RangeInfo(currentTB.Selection);
            base.ts.OnTextChanged(base.lastSel.Start.iLine, base.lastSel.Start.iLine);
        }

        public override void Undo()
        {
            base.ts.CurrentTB.Selection.Start = new Place(base.sel.FromX, Math.Min(base.sel.Start.iLine, base.sel.End.iLine));
            base.ts.OnTextChanging();
            InsertTextCommand.InsertText(this.deletedText, base.ts);
            base.ts.OnTextChanged(base.sel.Start.iLine, base.sel.End.iLine);
            base.ts.CurrentTB.Selection.Start = base.sel.Start;
            base.ts.CurrentTB.Selection.End = base.sel.End;
        }

        internal static void ClearSelected(TextSource ts)
        {
            FastColoredTextBox currentTB = ts.CurrentTB;
            Place start = currentTB.Selection.Start;
            Place end = currentTB.Selection.End;
            int i = Math.Min(end.iLine, start.iLine);
            int iToLine = Math.Max(end.iLine, start.iLine);
            int fromX = currentTB.Selection.FromX;
            int toX = currentTB.Selection.ToX;
            if (i >= 0)
            {
                if (i == iToLine)
                {
                    ts[i].RemoveRange(fromX, toX - fromX);
                }
                else
                {
                    ts[i].RemoveRange(fromX, ts[i].Count - fromX);
                    ts[iToLine].RemoveRange(0, toX);
                    ts.RemoveLine(i + 1, (iToLine - i) - 1);
                    InsertCharCommand.MergeLines(i, ts);
                }
                currentTB.Selection.Start = new Place(fromX, i);
                ts.NeedRecalc(new TextSource.TextChangedEventArgs(i, iToLine));
            }
        }
    }
}