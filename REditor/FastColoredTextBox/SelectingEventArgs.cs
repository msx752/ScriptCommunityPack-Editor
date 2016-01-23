using System;

namespace FastColoredTextBoxNS
{
    public class SelectingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }

        public bool Handled { get; set; }

        public AutoCompleteItem Item { get; internal set; }

        public int SelectedIndex { get; set; }
    }
}