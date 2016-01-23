namespace FastColoredTextBoxNS
{
    using System;

    internal class InsertCharCommand : UndoableCommand
    {
        private char c;
        private char deletedChar;

        public InsertCharCommand(TextSource ts, char c) : base(ts)
        {
            this.deletedChar = '\0';
            this.c = c;
        }

        public override void Execute()
        {
            base.ts.CurrentTB.ExpandBlock(base.ts.CurrentTB.Selection.Start.iLine);
            string text = this.c.ToString();
            base.ts.OnTextChanging(ref text);
            if (text.Length == 1)
            {
                this.c = text[0];
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (base.ts.Count == 0)
            {
                InsertLine(base.ts);
            }
            InsertChar(this.c, ref this.deletedChar, base.ts);
            base.ts.NeedRecalc(new TextSource.TextChangedEventArgs(base.ts.CurrentTB.Selection.Start.iLine, base.ts.CurrentTB.Selection.Start.iLine));
            base.Execute();
        }

        public override void Undo()
        {
            base.ts.OnTextChanging();
            switch (this.c)
            {
                case '\b':
                    {
                        base.ts.CurrentTB.Selection.Start = base.lastSel.Start;
                        char deletedChar = '\0';
                        if (this.deletedChar != '\0')
                        {
                            base.ts.CurrentTB.ExpandBlock(base.ts.CurrentTB.Selection.Start.iLine);
                            InsertChar(this.deletedChar, ref deletedChar, base.ts);
                        }
                        break;
                    }
                case '\n':
                    MergeLines(base.sel.Start.iLine, base.ts);
                    break;

                case '\r':
                    break;

                default:
                    base.ts.CurrentTB.ExpandBlock(base.sel.Start.iLine);
                    base.ts[base.sel.Start.iLine].RemoveAt(base.sel.Start.iChar);
                    base.ts.CurrentTB.Selection.Start = base.sel.Start;
                    break;
            }
            base.ts.NeedRecalc(new TextSource.TextChangedEventArgs(base.sel.Start.iLine, base.sel.Start.iLine));
            base.Undo();
        }

        internal static void BreakLines(int iLine, int pos, TextSource ts)
        {
            Line line = ts.CreateLine();
            for (int i = pos; i < ts[iLine].Count; i++)
            {
                line.Add(ts[iLine][i]);
            }
            ts[iLine].RemoveRange(pos, ts[iLine].Count - pos);
            ts.InsertLine(iLine + 1, line);
        }

        internal static void InsertChar(char c, ref char deletedChar, TextSource ts)
        {
            FastColoredTextBox currentTB = ts.CurrentTB;
            switch (c)
            {
                case '\b':
                    if ((currentTB.Selection.Start.iChar != 0) || (currentTB.Selection.Start.iLine != 0))
                    {
                        if (currentTB.Selection.Start.iChar == 0)
                        {
                            if (currentTB.lineInfos[currentTB.Selection.Start.iLine - 1].VisibleState != VisibleState.Visible)
                            {
                                currentTB.ExpandBlock(currentTB.Selection.Start.iLine - 1);
                            }
                            deletedChar = '\n';
                            MergeLines(currentTB.Selection.Start.iLine - 1, ts);
                        }
                        else
                        {
                            deletedChar = ts[currentTB.Selection.Start.iLine][currentTB.Selection.Start.iChar - 1].c;
                            ts[currentTB.Selection.Start.iLine].RemoveAt(currentTB.Selection.Start.iChar - 1);
                            currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar - 1, currentTB.Selection.Start.iLine);
                        }
                        return;
                    }
                    return;

                case '\t':
                    for (int i = 0; i < currentTB.TabLength; i++)
                    {
                        ts[currentTB.Selection.Start.iLine].Insert(currentTB.Selection.Start.iChar, new Char(' '));
                    }
                    currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar + currentTB.TabLength, currentTB.Selection.Start.iLine);
                    return;

                case '\n':
                    if (ts.Count == 0)
                    {
                        InsertLine(ts);
                    }
                    InsertLine(ts);
                    return;

                case '\r':
                    return;
            }
            ts[currentTB.Selection.Start.iLine].Insert(currentTB.Selection.Start.iChar, new Char(c));
            currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar + 1, currentTB.Selection.Start.iLine);
        }

        internal static void InsertLine(TextSource ts)
        {
            FastColoredTextBox currentTB = ts.CurrentTB;
            if (currentTB.Multiline || (currentTB.LinesCount <= 0))
            {
                if (ts.Count == 0)
                {
                    ts.InsertLine(currentTB.Selection.Start.iLine + 1, ts.CreateLine());
                }
                else
                {
                    BreakLines(currentTB.Selection.Start.iLine, currentTB.Selection.Start.iChar, ts);
                }
                currentTB.Selection.Start = new Place(0, currentTB.Selection.Start.iLine + 1);
                ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
            }
        }

        internal static void MergeLines(int i, TextSource ts)
        {
            FastColoredTextBox currentTB = ts.CurrentTB;
            if ((i + 1) < ts.Count)
            {
                currentTB.ExpandBlock(i);
                currentTB.ExpandBlock(i + 1);
                int count = ts[i].Count;
                if (ts[i].Count == 0)
                {
                    ts.RemoveLine(i);
                }
                else if (ts[i + 1].Count == 0)
                {
                    ts.RemoveLine(i + 1);
                }
                else
                {
                    ts[i].AddRange(ts[i + 1]);
                    ts.RemoveLine(i + 1);
                }
                currentTB.Selection.Start = new Place(count, i);
                ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
            }
        }
    }
}