namespace FastColoredTextBoxNS
{
    using System;

    public class LinePushedEventArgs : EventArgs
    {
        public LinePushedEventArgs(string sourceLineText, int displayedLineIndex, string displayedLineText)
        {
            this.SourceLineText = sourceLineText;
            this.DisplayedLineIndex = displayedLineIndex;
            this.DisplayedLineText = displayedLineText;
            this.SavedText = displayedLineText;
        }

        public int DisplayedLineIndex { get; private set; }

        public string DisplayedLineText { get; private set; }

        public string SavedText { get; set; }

        public string SourceLineText { get; private set; }
    }
}