namespace FastColoredTextBoxNS
{
    using System;

    public class TextChangingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }

        public string InsertingText { get; set; }
    }
}