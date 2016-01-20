namespace FastColoredTextBoxNS
{
    using System.Collections.Generic;

    internal class ReplaceMultipleTextCommand : UndoableCommand
    {
        private List<string> prevText;
        private List<ReplaceRange> ranges;

        public ReplaceMultipleTextCommand(TextSource ts, List<ReplaceRange> ranges) : base(ts)
        {
            this.prevText = new List<string>();
            ranges.Sort(delegate (ReplaceRange r1, ReplaceRange r2)
            {
                if (r1.ReplacedRange.Start.iLine == r2.ReplacedRange.Start.iLine)
                {
                    return r1.ReplacedRange.Start.iChar.CompareTo(r2.ReplacedRange.Start.iChar);
                }
                return r1.ReplacedRange.Start.iLine.CompareTo(r2.ReplacedRange.Start.iLine);
            });
            this.ranges = ranges;
            base.lastSel = base.sel = new RangeInfo(ts.CurrentTB.Selection);
        }

        public override void Execute()
        {
            FastColoredTextBox currentTB = base.ts.CurrentTB;
            this.prevText.Clear();
            base.ts.OnTextChanging();
            currentTB.Selection.BeginUpdate();
            for (int i = this.ranges.Count - 1; i >= 0; i--)
            {
                currentTB.Selection.Start = this.ranges[i].ReplacedRange.Start;
                currentTB.Selection.End = this.ranges[i].ReplacedRange.End;
                this.prevText.Add(currentTB.Selection.Text);
                ClearSelectedCommand.ClearSelected(base.ts);
                InsertTextCommand.InsertText(this.ranges[i].ReplaceText, base.ts);
                base.ts.OnTextChanged(this.ranges[i].ReplacedRange.Start.iLine, this.ranges[i].ReplacedRange.End.iLine);
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
                currentTB.Selection.Start = this.ranges[i].ReplacedRange.Start;
                for (int j = 0; j < this.ranges[i].ReplaceText.Length; j++)
                {
                    currentTB.Selection.GoRight(true);
                }
                ClearSelectedCommand.ClearSelected(base.ts);
                int num3 = (this.ranges.Count - 1) - i;
                InsertTextCommand.InsertText(this.prevText[num3], base.ts);
                base.ts.OnTextChanged(this.ranges[i].ReplacedRange.Start.iLine, this.ranges[i].ReplacedRange.Start.iLine);
            }
            currentTB.Selection.EndUpdate();
            base.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
        }

        public class ReplaceRange
        {
            public Range ReplacedRange { get; set; }

            public string ReplaceText { get; set; }
        }
    }
}