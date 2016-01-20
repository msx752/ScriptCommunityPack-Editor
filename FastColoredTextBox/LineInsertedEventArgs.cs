namespace FastColoredTextBoxNS
{
    using System;

    public class LineInsertedEventArgs : EventArgs
    {
        public LineInsertedEventArgs(int index, int count)
        {
            this.Index = index;
            this.Count = count;
        }

        public int Count { get; private set; }

        public int Index { get; private set; }
    }
}