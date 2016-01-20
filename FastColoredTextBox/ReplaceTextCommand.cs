using System.Collections.Generic;
namespace FastColoredTextBoxNS
{
    internal class ReplaceTextCommand : UndoableCommand
    {
        private string insertedText;
        private List<string> prevText;
        private List<Range> ranges;

        public ReplaceTextCommand(TextSource ts, List<Range> ranges, string insertedText) : base(ts)
        {
            this.prevText = new List<string>();
            ranges.Sort(delegate (Range r1, Range r2)
            {
                if (r1.Start.iLine == r2.Start.iLine)
                {
                    return r1.Start.iChar.CompareTo(r2.Start.iChar);
                }
                return r1.Start.iLine.CompareTo(r2.Start.iLine);
            });
            this.ranges = ranges;
            this.insertedText = insertedText;
            base.lastSel = base.sel = new RangeInfo(ts.CurrentTB.Selection);
        }

        public override void Execute()
        {
            FastColoredTextBox currentTB = base.ts.CurrentTB;
            this.prevText.Clear();
            base.ts.OnTextChanging(ref this.insertedText);
            currentTB.Selection.BeginUpdate();
            for (int i = this.ranges.Count - 1; i >= 0; i--)
            {
                currentTB.Selection.Start = this.ranges[i].Start;
                currentTB.Selection.End = this.ranges[i].End;
                this.prevText.Add(currentTB.Selection.Text);
                ClearSelectedCommand.ClearSelected(base.ts);
                InsertTextCommand.InsertText(this.insertedText, base.ts);
                base.ts.OnTextChanged(this.ranges[i].Start.iLine, this.ranges[i].End.iLine);
            }
            currentTB.Selection.EndUpdate();
            base.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
            base.lastSel = new RangeInfo(currentTB.Selection);
        }

        public override void Undo()
        {
            FastColoredTextBox currentTB = base.ts.CurrentTB;
            base.ts.OnTextChanging();
            currentTB.Selection.BeginUpdate();
            for (int i = 0; i < this.ranges.Count; i++)
            {
                currentTB.Selection.Start = this.ranges[i].Start;
                for (int j = 0; j < this.insertedText.Length; j++)
                {
                    currentTB.Selection.GoRight(true);
                }
                ClearSelectedCommand.ClearSelected(base.ts);
                InsertTextCommand.InsertText(this.prevText[(this.prevText.Count - i) - 1], base.ts);
                base.ts.OnTextChanged(this.ranges[i].Start.iLine, this.ranges[i].Start.iLine);
            }
            currentTB.Selection.EndUpdate();
            base.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
        }
    }
}