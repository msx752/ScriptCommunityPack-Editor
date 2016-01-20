namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections.Generic;

    public class LineRemovedEventArgs : EventArgs
    {
        public LineRemovedEventArgs(int index, int count, List<int> removedLineIds)
        {
            this.Index = index;
            this.Count = count;
            this.RemovedLineUniqueIds = removedLineIds;
        }

        public int Count { get; private set; }

        public int Index { get; private set; }

        public List<int> RemovedLineUniqueIds { get; private set; }
    }
}