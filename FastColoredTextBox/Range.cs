using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace FastColoredTextBoxNS
{
    public class Range : IEnumerable<Place>, IEnumerable
    {
        public readonly FastColoredTextBox tb;
        private List<Place> cachedCharIndexToPlace;
        private string cachedText;
        private int cachedTextVersion;
        private Place end;
        private int preferedPos;
        private Place start;
        private int updating;

        public Range(FastColoredTextBox tb)
        {
            this.preferedPos = -1;
            this.updating = 0;
            this.cachedTextVersion = -1;
            this.tb = tb;
        }

        public Range(FastColoredTextBox tb, Place start, Place end)
            : this(tb)
        {
            this.start = start;
            this.end = end;
        }

        public Range(FastColoredTextBox tb, int iStartChar, int iStartLine, int iEndChar, int iEndLine)
            : this(tb)
        {
            this.start = new Place(iStartChar, iStartLine);
            this.end = new Place(iEndChar, iEndLine);
        }

        public char CharAfterStart
        {
            get
            {
                if (this.Start.iChar >= this.tb[this.Start.iLine].Count)
                {
                    return '\n';
                }
                return this.tb[this.Start.iLine][this.Start.iChar].c;
            }
        }

        public char CharBeforeStart
        {
            get
            {
                if (this.Start.iChar <= 0)
                {
                    return '\n';
                }
                return this.tb[this.Start.iLine][this.Start.iChar - 1].c;
            }
        }

        public Place End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
                this.OnSelectionChanged();
            }
        }

        public Place Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.end = this.start = value;
                this.preferedPos = -1;
                this.OnSelectionChanged();
            }
        }

        public string Text
        {
            get
            {
                int num = Math.Min(this.end.iLine, this.start.iLine);
                int num2 = Math.Max(this.end.iLine, this.start.iLine);
                int fromX = this.FromX;
                int toX = this.ToX;
                if (num < 0)
                {
                    return null;
                }
                StringBuilder builder = new StringBuilder();
                for (int i = num; i <= num2; i++)
                {
                    int num6 = (i == num) ? fromX : 0;
                    int num7 = (i == num2) ? (toX - 1) : (this.tb[i].Count - 1);
                    for (int j = num6; j <= num7; j++)
                    {
                        builder.Append(this.tb[i][j].c);
                    }
                    if ((i != num2) && (num != num2))
                    {
                        builder.AppendLine();
                    }
                }
                return builder.ToString();
            }
        }

        internal int FromX
        {
            get
            {
                if (this.end.iLine < this.start.iLine)
                {
                    return this.end.iChar;
                }
                if (this.end.iLine > this.start.iLine)
                {
                    return this.start.iChar;
                }
                return Math.Min(this.end.iChar, this.start.iChar);
            }
        }

        internal int ToX
        {
            get
            {
                if (this.end.iLine < this.start.iLine)
                {
                    return this.start.iChar;
                }
                if (this.end.iLine > this.start.iLine)
                {
                    return this.end.iChar;
                }
                return Math.Max(this.end.iChar, this.start.iChar);
            }
        }

        public static StyleIndex ToStyleIndex(int i)
        {
            return (StyleIndex)((ushort)(((int)1) << i));
        }

        public void BeginUpdate()
        {
            this.updating++;
        }

        public void ClearFoldingMarkers()
        {
            int num = Math.Min(this.End.iLine, this.Start.iLine);
            int num2 = Math.Max(this.End.iLine, this.Start.iLine);
            if (num >= 0)
            {
                for (int i = num; i <= num2; i++)
                {
                    this.tb[i].ClearFoldingMarkers();
                }
                this.tb.Invalidate();
            }
        }

        public void ClearStyle(params Style[] styles)
        {
            try
            {
                this.ClearStyle(this.tb.GetStyleIndexMask(styles));
            }
            catch
            {
            }
        }

        public void ClearStyle(StyleIndex styleIndex)
        {
            int fromLine = Math.Min(End.iLine, Start.iLine);
            int toLine = Math.Max(End.iLine, Start.iLine);
            int fromChar = FromX;
            int toChar = ToX;
            if (fromLine >= 0)
            {
                for (int y = fromLine; y <= toLine; y++)
                {
                    int fromX = y == fromLine ? fromChar : 0;
                    int toX = y == toLine ? Math.Min(toChar - 1, tb[y].Count - 1) : tb[y].Count - 1;
                    for (int x = fromX; x <= toX; x++)
                    {
                        Char c = tb[y][x];
                        c.style &= ~styleIndex;
                        tb[y][x] = c;
                    }
                }
                this.tb.Invalidate();
            }
        }

        public Range Clone()
        {
            return (Range)base.MemberwiseClone();
        }

        public bool Contains(Place place)
        {
            if (place.iLine < Math.Min(this.start.iLine, this.end.iLine))
            {
                return false;
            }
            if (place.iLine > Math.Max(this.start.iLine, this.end.iLine))
            {
                return false;
            }
            Place start = this.start;
            Place end = this.end;
            if ((start.iLine > end.iLine) || ((start.iLine == end.iLine) && (start.iChar > end.iChar)))
            {
                Place place4 = start;
                start = end;
                end = place4;
            }
            if ((place.iLine == start.iLine) && (place.iChar < start.iChar))
            {
                return false;
            }
            if ((place.iLine == end.iLine) && (place.iChar > end.iChar))
            {
                return false;
            }
            return true;
        }

        public void EndUpdate()
        {
            this.updating--;
            if (this.updating == 0)
            {
                this.OnSelectionChanged();
            }
        }

        public void Expand()
        {
            this.Normalize();
            this.start = new Place(0, this.start.iLine);
            int lineLength = this.tb.GetLineLength(this.end.iLine);
            this.end = new Place(lineLength, this.end.iLine);
        }

        public Range GetFragment(string allowedSymbolsPattern)
        {
            return this.GetFragment(allowedSymbolsPattern, RegexOptions.None);
        }

        public Range GetFragment(string allowedSymbolsPattern, RegexOptions options)
        {
            Range range = new Range(this.tb)
            {
                Start = this.Start
            };
            Regex regex = new Regex(allowedSymbolsPattern, options);
            while (range.GoLeftThroughFolded())
            {
                if (!regex.IsMatch(range.CharAfterStart.ToString()))
                {
                    range.GoRightThroughFolded();
                    break;
                }
            }
            Place start = range.Start;
            range.Start = this.Start;
            while (regex.IsMatch(range.CharAfterStart.ToString()) && range.GoRightThroughFolded())
            {
            }
            return new Range(this.tb, start, range.Start);
        }

        public Range GetIntersectionWith(Range range)
        {
            Range range2 = this.Clone();
            Range range3 = range.Clone();
            range2.Normalize();
            range3.Normalize();
            Place fromPlace = (range2.Start > range3.Start) ? range2.Start : range3.Start;
            Place toPlace = (range2.End < range3.End) ? range2.End : range3.End;
            if (toPlace < fromPlace)
            {
                return new Range(this.tb, this.start, this.start);
            }
            return this.tb.GetRange(fromPlace, toPlace);
        }

        public IEnumerable<Range> GetRanges(string regexPattern)
        {
            return this.GetRanges(regexPattern, RegexOptions.None);
        }

        public IEnumerable<Range> GetRanges(Regex regex)
        {
            List<Place> iteratorVariable1;
            string iteratorVariable0;
            this.GetText(out iteratorVariable0, out iteratorVariable1);
            IEnumerator enumerator = regex.Matches(iteratorVariable0).GetEnumerator();
            while (enumerator.MoveNext())
            {
                Match current = (Match)enumerator.Current;
                Range iteratorVariable3 = new Range(this.tb);
                Group iteratorVariable4 = current.Groups["range"];
                if (!iteratorVariable4.Success)
                {
                    iteratorVariable4 = current.Groups[0];
                }
                iteratorVariable3.Start = iteratorVariable1[iteratorVariable4.Index];
                iteratorVariable3.End = iteratorVariable1[iteratorVariable4.Index + iteratorVariable4.Length];
                yield return iteratorVariable3;
            }
        }

        public IEnumerable<Range> GetRanges(string regexPattern, RegexOptions options)
        {
            List<Place> iteratorVariable1;
            string iteratorVariable0;
            this.GetText(out iteratorVariable0, out iteratorVariable1);
            Regex iteratorVariable2 = new Regex(regexPattern, options);
            IEnumerator enumerator = iteratorVariable2.Matches(iteratorVariable0).GetEnumerator();
            while (enumerator.MoveNext())
            {
                Match current = (Match)enumerator.Current;
                Range iteratorVariable4 = new Range(this.tb);
                Group iteratorVariable5 = current.Groups["range"];
                if (!iteratorVariable5.Success)
                {
                    iteratorVariable5 = current.Groups[0];
                }
                iteratorVariable4.Start = iteratorVariable1[iteratorVariable5.Index];
                iteratorVariable4.End = iteratorVariable1[iteratorVariable5.Index + iteratorVariable5.Length];
                yield return iteratorVariable4;
            }
        }

        public IEnumerable<Range> GetRangesByLines(string regexPattern, RegexOptions options)
        {
            this.Normalize();
            Regex regex = new Regex(regexPattern, options);
            FileTextSource textSource = this.tb.TextSource as FileTextSource;
            for (int i = this.Start.iLine; i <= this.End.iLine; i++)
            {
                bool iteratorVariable3 = (textSource != null) ? textSource.IsLineLoaded(i) : true;
                Range iteratorVariable4 = new Range(this.tb, new Place(0, i), new Place(this.tb[i].Count, i));
                if ((i == this.Start.iLine) || (i == this.End.iLine))
                {
                    iteratorVariable4 = iteratorVariable4.GetIntersectionWith(this);
                }
                foreach (Range iteratorVariable5 in iteratorVariable4.GetRanges(regex))
                {
                    yield return iteratorVariable5;
                }
                if (!iteratorVariable3)
                {
                    textSource.UnloadLine(i);
                }
            }
        }

        public Range GetUnionWith(Range range)
        {
            Range range2 = this.Clone();
            Range range3 = range.Clone();
            range2.Normalize();
            range3.Normalize();
            Place fromPlace = (range2.Start < range3.Start) ? range2.Start : range3.Start;
            Place toPlace = (range2.End > range3.End) ? range2.End : range3.End;
            return this.tb.GetRange(fromPlace, toPlace);
        }

        public bool GoLeft()
        {
            Place start = this.start;
            this.GoLeft(false);
            return (start != this.start);
        }

        public void GoLeft(bool shift)
        {
            if ((this.start.iChar != 0) || (this.start.iLine != 0))
            {
                if ((this.start.iChar > 0) && (this.tb.lineInfos[this.start.iLine].VisibleState == VisibleState.Visible))
                {
                    this.start.Offset(-1, 0);
                }
                else
                {
                    int iLine = this.tb.FindPrevVisibleLine(this.start.iLine);
                    if (iLine == this.start.iLine)
                    {
                        return;
                    }
                    this.start = new Place(this.tb[iLine].Count, iLine);
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
            this.preferedPos = -1;
        }

        public bool GoLeftThroughFolded()
        {
            if ((this.start.iChar == 0) && (this.start.iLine == 0))
            {
                return false;
            }
            if (this.start.iChar > 0)
            {
                this.start.Offset(-1, 0);
            }
            else
            {
                this.start = new Place(this.tb[this.start.iLine - 1].Count, this.start.iLine - 1);
            }
            this.preferedPos = -1;
            this.end = this.start;
            this.OnSelectionChanged();
            return true;
        }

        public bool GoRight()
        {
            Place start = this.start;
            this.GoRight(false);
            return (start != this.start);
        }

        public void GoRight(bool shift)
        {
            if ((this.start.iLine < (this.tb.LinesCount - 1)) || (this.start.iChar < this.tb[this.tb.LinesCount - 1].Count))
            {
                if ((this.start.iChar < this.tb[this.start.iLine].Count) && (this.tb.lineInfos[this.start.iLine].VisibleState == VisibleState.Visible))
                {
                    this.start.Offset(1, 0);
                }
                else
                {
                    int iLine = this.tb.FindNextVisibleLine(this.start.iLine);
                    if (iLine == this.start.iLine)
                    {
                        return;
                    }
                    this.start = new Place(0, iLine);
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
            this.preferedPos = -1;
        }

        public bool GoRightThroughFolded()
        {
            if ((this.start.iLine >= (this.tb.LinesCount - 1)) && (this.start.iChar >= this.tb[this.tb.LinesCount - 1].Count))
            {
                return false;
            }
            if (this.start.iChar < this.tb[this.start.iLine].Count)
            {
                this.start.Offset(1, 0);
            }
            else
            {
                this.start = new Place(0, this.start.iLine + 1);
            }
            this.preferedPos = -1;
            this.end = this.start;
            this.OnSelectionChanged();
            return true;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Place>)this).GetEnumerator();
        }

        public void Inverse()
        {
            Place start = this.start;
            this.start = this.end;
            this.end = start;
        }

        public void Normalize()
        {
            if (this.Start > this.End)
            {
                this.Inverse();
            }
        }

        public void SelectAll()
        {
            this.Start = new Place(0, 0);
            if (this.tb.LinesCount == 0)
            {
                this.Start = new Place(0, 0);
            }
            else
            {
                this.end = new Place(0, 0);
                this.start = new Place(this.tb[this.tb.LinesCount - 1].Count, this.tb.LinesCount - 1);
            }
            if (this == this.tb.Selection)
            {
                this.tb.Invalidate();
            }
        }

        public void SetFoldingMarkers(string startFoldingPattern, string finishFoldingPattern)
        {
            this.SetFoldingMarkers(startFoldingPattern, finishFoldingPattern, RegexOptions.Compiled);
        }

        public void SetFoldingMarkers(string startFoldingPattern, string finishFoldingPattern, RegexOptions options)
        {
            if (startFoldingPattern == finishFoldingPattern)
            {
                SetFoldingMarkers(startFoldingPattern, options);
                return;
            }
            foreach (Range range in this.GetRanges(startFoldingPattern, options))
            {
                tb[range.Start.iLine].FoldingStartMarker = startFoldingPattern;
            }
            foreach (Range range in this.GetRanges(finishFoldingPattern, options))
            {
                tb[range.Start.iLine].FoldingEndMarker = startFoldingPattern;
            }
            tb.Invalidate();
        }

        /// <summary>
        /// Sets folding markers
        /// </summary>
        /// <param name="startEndFoldingPattern">Pattern for start and end folding line</param>
        public void SetFoldingMarkers(string foldingPattern, RegexOptions options)
        {
            foreach (var range in GetRanges(foldingPattern, options))
            {
                if (range.Start.iLine > 0)
                {
                    tb[range.Start.iLine - 1].FoldingEndMarker = foldingPattern;
                    tb[range.Start.iLine].FoldingStartMarker = foldingPattern;
                }
            }

            //tb.Invalidate();
        }

        public void SetStyle(Style style)
        {
            int orSetStyleLayerIndex = this.tb.GetOrSetStyleLayerIndex(style);
            this.SetStyle(ToStyleIndex(orSetStyleLayerIndex));
            this.tb.Invalidate();
        }

        public void SetStyle(StyleIndex styleIndex)
        {
            int num = Math.Min(this.End.iLine, this.Start.iLine);
            int num2 = Math.Max(this.End.iLine, this.Start.iLine);
            int fromX = this.FromX;
            int toX = this.ToX;
            if (num >= 0)
            {
                for (int i = num; i <= num2; i++)
                {
                    int num6 = (i == num) ? fromX : 0;
                    int num7 = (i == num2) ? (toX - 1) : (this.tb[i].Count - 1);
                    for (int j = num6; j <= num7; j++)
                    {
                        Char ch = this.tb[i][j];
                        ch.style = (StyleIndex)((ushort)(ch.style | styleIndex));
                        this.tb[i][j] = ch;
                    }
                }
            }
        }

        public void SetStyle(Style style, string regexPattern)
        {
            StyleIndex styleLayer = ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
            this.SetStyle(styleLayer, regexPattern, RegexOptions.None);
        }

        public void SetStyle(Style style, Regex regex)
        {
            StyleIndex styleLayer = ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
            this.SetStyle(styleLayer, regex);
        }

        public void SetStyle(StyleIndex styleLayer, Regex regex)
        {
            foreach (Range range in this.GetRanges(regex))
            {
                range.SetStyle(styleLayer);
            }
            this.tb.Invalidate();
        }

        public void SetStyle(Style style, string regexPattern, RegexOptions options)
        {
            StyleIndex styleLayer = ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
            this.SetStyle(styleLayer, regexPattern, options);
        }

        public void SetStyle(StyleIndex styleLayer, string regexPattern, RegexOptions options)
        {
            if (Math.Abs((int)(this.Start.iLine - this.End.iLine)) > 0x3e8)
            {
                options |= RegexOptions.Compiled;
            }
            foreach (Range range in this.GetRanges(regexPattern, options))
            {
                range.SetStyle(styleLayer);
            }
            this.tb.Invalidate();
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "Start: ", this.Start, " End: ", this.End });
        }

        internal void GetText(out string text, out List<Place> charIndexToPlace)
        {
            if (this.tb.TextVersion == this.cachedTextVersion)
            {
                text = this.cachedText;
                charIndexToPlace = this.cachedCharIndexToPlace;
            }
            else
            {
                int num = Math.Min(this.end.iLine, this.start.iLine);
                int num2 = Math.Max(this.end.iLine, this.start.iLine);
                int fromX = this.FromX;
                int toX = this.ToX;
                StringBuilder builder = new StringBuilder((num2 - num) * 50);
                charIndexToPlace = new List<Place>(builder.Capacity);
                if (num >= 0)
                {
                    for (int i = num; i <= num2; i++)
                    {
                        int num6 = (i == num) ? fromX : 0;
                        int num7 = (i == num2) ? (toX - 1) : (this.tb[i].Count - 1);
                        for (int j = num6; j <= num7; j++)
                        {
                            builder.Append(this.tb[i][j].c);
                            charIndexToPlace.Add(new Place(j, i));
                        }
                        if ((i != num2) && (num != num2))
                        {
                            foreach (char ch in Environment.NewLine)
                            {
                                builder.Append(ch);
                                charIndexToPlace.Add(new Place(this.tb[i].Count, i));
                            }
                        }
                    }
                }
                text = builder.ToString();
                charIndexToPlace.Add((this.End > this.Start) ? this.End : this.Start);
                this.cachedText = text;
                this.cachedCharIndexToPlace = charIndexToPlace;
                this.cachedTextVersion = this.tb.TextVersion;
            }
        }
        internal void GoDown(bool shift)
        {
            LineInfo info;
            if (this.preferedPos < 0)
            {
                info = this.tb.lineInfos[this.start.iLine];
                LineInfo info2 = this.tb.lineInfos[this.start.iLine];
                this.preferedPos = this.start.iChar - info.GetWordWrapStringStartPosition(info2.GetWordWrapStringIndex(this.start.iChar));
            }
            int wordWrapStringIndex = this.tb.lineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar);
            info = this.tb.lineInfos[this.start.iLine];
            if (wordWrapStringIndex >= (info.WordWrapStringsCount - 1))
            {
                if (this.start.iLine >= (this.tb.LinesCount - 1))
                {
                    return;
                }
                int num2 = this.tb.FindNextVisibleLine(this.start.iLine);
                if (num2 == this.start.iLine)
                {
                    return;
                }
                this.start.iLine = num2;
                wordWrapStringIndex = -1;
            }
            info = this.tb.lineInfos[this.start.iLine];
            if (wordWrapStringIndex < (info.WordWrapStringsCount - 1))
            {
                info = this.tb.lineInfos[this.start.iLine];
                int wordWrapStringFinishPosition = info.GetWordWrapStringFinishPosition(wordWrapStringIndex + 1, this.tb[this.start.iLine]);
                info = this.tb.lineInfos[this.start.iLine];
                this.start.iChar = info.GetWordWrapStringStartPosition(wordWrapStringIndex + 1) + this.preferedPos;
                if (this.start.iChar > (wordWrapStringFinishPosition + 1))
                {
                    this.start.iChar = wordWrapStringFinishPosition + 1;
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }

        internal void GoEnd(bool shift)
        {
            if ((this.start.iLine >= 0) && (this.tb.lineInfos[this.start.iLine].VisibleState == VisibleState.Visible))
            {
                this.start = new Place(this.tb[this.start.iLine].Count, this.start.iLine);
                if (!shift)
                {
                    this.end = this.start;
                }
                this.OnSelectionChanged();
                this.preferedPos = -1;
            }
        }

        internal void GoFirst(bool shift)
        {
            this.start = new Place(0, 0);
            if (this.tb.lineInfos[this.Start.iLine].VisibleState != VisibleState.Visible)
            {
                this.GoRight(shift);
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }

        internal void GoHome(bool shift)
        {
            if ((this.start.iLine >= 0) && (this.tb.lineInfos[this.start.iLine].VisibleState == VisibleState.Visible))
            {
                this.start = new Place(0, this.start.iLine);
                if (!shift)
                {
                    this.end = this.start;
                }
                this.OnSelectionChanged();
                this.preferedPos = -1;
            }
        }

        internal void GoLast(bool shift)
        {
            this.start = new Place(this.tb[this.tb.LinesCount - 1].Count, this.tb.LinesCount - 1);
            if (this.tb.lineInfos[this.Start.iLine].VisibleState != VisibleState.Visible)
            {
                this.GoLeft(shift);
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }
        internal void GoPageDown(bool shift)
        {
            LineInfo info;
            if (this.preferedPos < 0)
            {
                info = this.tb.lineInfos[this.start.iLine];
                LineInfo info2 = this.tb.lineInfos[this.start.iLine];
                this.preferedPos = this.start.iChar - info.GetWordWrapStringStartPosition(info2.GetWordWrapStringIndex(this.start.iChar));
            }
            int num = (this.tb.ClientRectangle.Height / this.tb.CharHeight) - 1;
            for (int i = 0; i < num; i++)
            {
                info = this.tb.lineInfos[this.start.iLine];
                int wordWrapStringIndex = info.GetWordWrapStringIndex(this.start.iChar);
                info = this.tb.lineInfos[this.start.iLine];
                if (wordWrapStringIndex >= (info.WordWrapStringsCount - 1))
                {
                    if (this.start.iLine >= (this.tb.LinesCount - 1))
                    {
                        break;
                    }
                    int num4 = this.tb.FindNextVisibleLine(this.start.iLine);
                    if (num4 == this.start.iLine)
                    {
                        break;
                    }
                    this.start.iLine = num4;
                    wordWrapStringIndex = -1;
                }
                info = this.tb.lineInfos[this.start.iLine];
                if (wordWrapStringIndex < (info.WordWrapStringsCount - 1))
                {
                    info = this.tb.lineInfos[this.start.iLine];
                    int wordWrapStringFinishPosition = info.GetWordWrapStringFinishPosition(wordWrapStringIndex + 1, this.tb[this.start.iLine]);
                    info = this.tb.lineInfos[this.start.iLine];
                    this.start.iChar = info.GetWordWrapStringStartPosition(wordWrapStringIndex + 1) + this.preferedPos;
                    if (this.start.iChar > (wordWrapStringFinishPosition + 1))
                    {
                        this.start.iChar = wordWrapStringFinishPosition + 1;
                    }
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }

        internal void GoPageUp(bool shift)
        {
            LineInfo info;
            if (this.preferedPos < 0)
            {
                info = this.tb.lineInfos[this.start.iLine];
                LineInfo info2 = this.tb.lineInfos[this.start.iLine];
                this.preferedPos = this.start.iChar - info.GetWordWrapStringStartPosition(info2.GetWordWrapStringIndex(this.start.iChar));
            }
            int num = (this.tb.ClientRectangle.Height / this.tb.CharHeight) - 1;
            for (int i = 0; i < num; i++)
            {
                info = this.tb.lineInfos[this.start.iLine];
                int wordWrapStringIndex = info.GetWordWrapStringIndex(this.start.iChar);
                if (wordWrapStringIndex == 0)
                {
                    if (this.start.iLine <= 0)
                    {
                        break;
                    }
                    int num4 = this.tb.FindPrevVisibleLine(this.start.iLine);
                    if (num4 == this.start.iLine)
                    {
                        break;
                    }
                    this.start.iLine = num4;
                    info = this.tb.lineInfos[this.start.iLine];
                    wordWrapStringIndex = info.WordWrapStringsCount;
                }
                if (wordWrapStringIndex > 0)
                {
                    info = this.tb.lineInfos[this.start.iLine];
                    int wordWrapStringFinishPosition = info.GetWordWrapStringFinishPosition(wordWrapStringIndex - 1, this.tb[this.start.iLine]);
                    info = this.tb.lineInfos[this.start.iLine];
                    this.start.iChar = info.GetWordWrapStringStartPosition(wordWrapStringIndex - 1) + this.preferedPos;
                    if (this.start.iChar > (wordWrapStringFinishPosition + 1))
                    {
                        this.start.iChar = wordWrapStringFinishPosition + 1;
                    }
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }
        internal void GoUp(bool shift)
        {
            LineInfo info;
            if (this.preferedPos < 0)
            {
                info = this.tb.lineInfos[this.start.iLine];
                LineInfo info2 = this.tb.lineInfos[this.start.iLine];
                this.preferedPos = this.start.iChar - info.GetWordWrapStringStartPosition(info2.GetWordWrapStringIndex(this.start.iChar));
            }
            info = this.tb.lineInfos[this.start.iLine];
            int wordWrapStringIndex = info.GetWordWrapStringIndex(this.start.iChar);
            if (wordWrapStringIndex == 0)
            {
                if (this.start.iLine <= 0)
                {
                    return;
                }
                int num2 = this.tb.FindPrevVisibleLine(this.start.iLine);
                if (num2 == this.start.iLine)
                {
                    return;
                }
                this.start.iLine = num2;
                info = this.tb.lineInfos[this.start.iLine];
                wordWrapStringIndex = info.WordWrapStringsCount;
            }
            if (wordWrapStringIndex > 0)
            {
                info = this.tb.lineInfos[this.start.iLine];
                int wordWrapStringFinishPosition = info.GetWordWrapStringFinishPosition(wordWrapStringIndex - 1, this.tb[this.start.iLine]);
                info = this.tb.lineInfos[this.start.iLine];
                this.start.iChar = info.GetWordWrapStringStartPosition(wordWrapStringIndex - 1) + this.preferedPos;
                if (this.start.iChar > (wordWrapStringFinishPosition + 1))
                {
                    this.start.iChar = wordWrapStringFinishPosition + 1;
                }
            }
            if (!shift)
            {
                this.end = this.start;
            }
            this.OnSelectionChanged();
        }

        internal void GoWordLeft(bool shift)
        {
            Place start;
            Range range = this.Clone();
            bool flag = this.IsIdentifierChar(range.CharBeforeStart);
            do
            {
                start = range.Start;
                if (this.IsIdentifierChar(range.CharBeforeStart) ^ flag)
                {
                    break;
                }
                range.GoLeft(shift);
            }
            while (start != range.Start);
            this.Start = range.Start;
            this.End = range.End;
            if (this.tb.lineInfos[this.Start.iLine].VisibleState != VisibleState.Visible)
            {
                this.GoRight(shift);
            }
        }

        internal void GoWordRight(bool shift)
        {
            Place start;
            Range range = this.Clone();
            bool flag = this.IsIdentifierChar(range.CharAfterStart);
            do
            {
                start = range.Start;
                if (this.IsIdentifierChar(range.CharAfterStart) ^ flag)
                {
                    break;
                }
                range.GoRight(shift);
            }
            while (start != range.Start);
            this.Start = range.Start;
            this.End = range.End;
            if (this.tb.lineInfos[this.Start.iLine].VisibleState != VisibleState.Visible)
            {
                this.GoLeft(shift);
            }
        }
        private bool IsIdentifierChar(char c)
        {
            return (char.IsLetterOrDigit(c) || (c == '_'));
        }
        private void OnSelectionChanged()
        {
            this.cachedTextVersion = -1;
            this.cachedText = null;
            this.cachedCharIndexToPlace = null;
            if ((this.tb.Selection == this) && (this.updating == 0))
            {
                this.tb.OnSelectionChanged();
            }
        }
        IEnumerator<Place> IEnumerable<Place>.GetEnumerator()
        {
            int iteratorVariable0 = Math.Min(this.end.iLine, this.start.iLine);
            int iteratorVariable1 = Math.Max(this.end.iLine, this.start.iLine);
            int fromX = this.FromX;
            int toX = this.ToX;
            if (iteratorVariable0 >= 0)
            {
                for (int i = iteratorVariable0; i <= iteratorVariable1; i++)
                {
                    int iteratorVariable5 = (i == iteratorVariable0) ? fromX : 0;
                    int iteratorVariable6 = (i == iteratorVariable1) ? (toX - 1) : (this.tb[i].Count - 1);
                    for (int j = iteratorVariable5; j <= iteratorVariable6; j++)
                    {
                        yield return new Place(j, i);
                    }
                }
            }
        }
    }
}