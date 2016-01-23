using System;

namespace FastColoredTextBoxNS
{
    public class SelectedEventArgs : EventArgs
    {
        public AutoCompleteItem Item { get; internal set; }

        public FastColoredTextBox Tb { get; set; }
    }
}