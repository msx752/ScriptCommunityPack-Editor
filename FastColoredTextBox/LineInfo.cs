namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct LineInfo
    {
        private List<int> cutOffPositions;
        internal int startY;
        public VisibleState VisibleState;

        public LineInfo(int startY)
        {
            this.cutOffPositions = null;
            this.VisibleState = VisibleState.Visible;
            this.startY = startY;
        }

        public List<int> CutOffPositions
        {
            get
            {
                if (this.cutOffPositions == null)
                {
                    this.cutOffPositions = new List<int>();
                }
                return this.cutOffPositions;
            }
        }

        public int WordWrapStringsCount
        {
            get
            {
                switch (this.VisibleState)
                {
                    case VisibleState.Visible:
                        if (this.cutOffPositions != null)
                        {
                            return (this.cutOffPositions.Count + 1);
                        }
                        return 1;

                    case VisibleState.StartOfHiddenBlock:
                        return 1;

                    case VisibleState.Hidden:
                        return 0;
                }
                return 0;
            }
        }

        internal int GetWordWrapStringStartPosition(int iWordWrapLine)
        {
            return ((iWordWrapLine == 0) ? 0 : this.CutOffPositions[iWordWrapLine - 1]);
        }

        internal int GetWordWrapStringFinishPosition(int iWordWrapLine, Line line)
        {
            if (this.WordWrapStringsCount <= 0)
            {
                return 0;
            }
            return ((iWordWrapLine == (this.WordWrapStringsCount - 1)) ? (line.Count - 1) : (this.CutOffPositions[iWordWrapLine] - 1));
        }

        public int GetWordWrapStringIndex(int iChar)
        {
            if ((this.cutOffPositions == null) || (this.cutOffPositions.Count == 0))
            {
                return 0;
            }
            for (int i = 0; i < this.cutOffPositions.Count; i++)
            {
                if (this.cutOffPositions[i] > iChar)
                {
                    return i;
                }
            }
            return this.cutOffPositions.Count;
        }

        internal void CalcCutOffs(int maxCharsPerLine, bool allowIME, bool charWrap, Line line)
        {
            int num = 0;
            int item = 0;
            this.CutOffPositions.Clear();
            for (int i = 0; i < line.Count; i++)
            {
                char c = line[i].c;
                if (charWrap)
                {
                    item = Math.Min((int)(i + 1), (int)(line.Count - 1));
                }
                else if (allowIME && this.isCJKLetter(c))
                {
                    item = i;
                }
                else if (!(char.IsLetterOrDigit(c) || (c == '_')))
                {
                    item = Math.Min((int)(i + 1), (int)(line.Count - 1));
                }
                num++;
                if (num == maxCharsPerLine)
                {
                    if ((item == 0) || ((this.cutOffPositions.Count > 0) && (item == this.cutOffPositions[this.cutOffPositions.Count - 1])))
                    {
                        item = i + 1;
                    }
                    this.CutOffPositions.Add(item);
                    num = (1 + i) - item;
                }
            }
        }

        private bool isCJKLetter(char c)
        {
            int num = Convert.ToInt32(c);
            return ((((((((num >= 0x3300) && (num <= 0x33ff)) || ((num >= 0xfe30) && (num <= 0xfe4f))) || (((num >= 0xf900) && (num <= 0xfaff)) || ((num >= 0x2e80) && (num <= 0x2eff)))) || ((((num >= 0x31c0) && (num <= 0x31ef)) || ((num >= 0x4e00) && (num <= 0x9fff))) || (((num >= 0x3400) && (num <= 0x4dbf)) || ((num >= 0x3200) && (num <= 0x32ff))))) || (((((num >= 0x2460) && (num <= 0x24ff)) || ((num >= 0x3040) && (num <= 0x309f))) || (((num >= 0x2f00) && (num <= 0x2fdf)) || ((num >= 0x31a0) && (num <= 0x31bf)))) || ((((num >= 0x4dc0) && (num <= 0x4dff)) || ((num >= 0x3100) && (num <= 0x312f))) || (((num >= 0x30a0) && (num <= 0x30ff)) || ((num >= 0x31f0) && (num <= 0x31ff)))))) || (((((num >= 0x2ff0) && (num <= 0x2fff)) || ((num >= 0x1100) && (num <= 0x11ff))) || (((num >= 0xa960) && (num <= 0xa97f)) || ((num >= 0xd7b0) && (num <= 0xd7ff)))) || ((num >= 0x3130) && (num <= 0x318f)))) || ((num >= 0xac00) && (num <= 0xd7af)));
        }
    }
}