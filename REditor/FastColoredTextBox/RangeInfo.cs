using System;

namespace FastColoredTextBoxNS
{
    internal class RangeInfo
    {
        public RangeInfo(Range r)
        {
            this.Start = r.Start;
            this.End = r.End;
        }

        public Place End { get; set; }

        public Place Start { get; set; }

        internal int FromX
        {
            get
            {
                if (this.End.iLine < this.Start.iLine)
                {
                    return this.End.iChar;
                }
                if (this.End.iLine > this.Start.iLine)
                {
                    return this.Start.iChar;
                }
                return Math.Min(this.End.iChar, this.Start.iChar);
            }
        }
    }
}