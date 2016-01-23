namespace FastColoredTextBoxNS
{
    using System;

    public class LineNeededEventArgs : EventArgs
    {
        public LineNeededEventArgs(string sourceLineText, int displayedLineIndex)
        {
            this.SourceLineText = sourceLineText;
            this.DisplayedLineIndex = displayedLineIndex;
            this.DisplayedLineText = sourceLineText;
        }

        public int DisplayedLineIndex { get; private set; }

        public string DisplayedLineText { get; set; }

        public string SourceLineText { get; private set; }
    }
}