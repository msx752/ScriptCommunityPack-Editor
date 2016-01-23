namespace FastColoredTextBoxNS
{
    using System;

    public class TextChangedEventArgs : EventArgs
    {
        public TextChangedEventArgs(Range changedRange)
        {
            this.ChangedRange = changedRange;
        }

        public Range ChangedRange { get; set; }
    }
}