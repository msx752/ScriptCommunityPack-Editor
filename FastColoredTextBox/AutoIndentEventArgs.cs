namespace FastColoredTextBoxNS
{
    using System;

    public class AutoIndentEventArgs : EventArgs
    {
        public AutoIndentEventArgs(int iLine, string lineText, string prevLineText, int tabLength)
        {
            this.iLine = iLine;
            this.LineText = lineText;
            this.PrevLineText = prevLineText;
            this.TabLength = tabLength;
        }

        public int iLine { get; internal set; }

        public string LineText { get; internal set; }

        public string PrevLineText { get; internal set; }

        public int Shift { get; set; }

        public int ShiftNextLines { get; set; }

        public int TabLength { get; internal set; }
    }
}