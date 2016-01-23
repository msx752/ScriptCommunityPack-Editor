namespace FastColoredTextBoxNS
{
    internal abstract class UndoableCommand : Command
    {
        internal bool autoUndo;
        internal RangeInfo lastSel;
        internal RangeInfo sel;

        public UndoableCommand(TextSource ts)
        {
            base.ts = ts;
            this.sel = new RangeInfo(ts.CurrentTB.Selection);
        }

        public override void Execute()
        {
            this.lastSel = new RangeInfo(base.ts.CurrentTB.Selection);
            this.OnTextChanged(false);
        }

        public virtual void Undo()
        {
            this.OnTextChanged(true);
        }

        protected virtual void OnTextChanged(bool invert)
        {
            bool flag = this.sel.Start.iLine < this.lastSel.Start.iLine;
            if (invert)
            {
                if (flag)
                {
                    base.ts.OnTextChanged(this.sel.Start.iLine, this.sel.Start.iLine);
                }
                else
                {
                    base.ts.OnTextChanged(this.sel.Start.iLine, this.lastSel.Start.iLine);
                }
            }
            else if (flag)
            {
                base.ts.OnTextChanged(this.sel.Start.iLine, this.lastSel.Start.iLine);
            }
            else
            {
                base.ts.OnTextChanged(this.lastSel.Start.iLine, this.lastSel.Start.iLine);
            }
        }
    }
}