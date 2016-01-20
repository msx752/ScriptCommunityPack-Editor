namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;

    public class TextSource : IList<Line>, ICollection<Line>, IEnumerable<Line>, IEnumerable, IDisposable
    {
        public readonly Style[] Styles = new Style[0x10];
        protected readonly List<Line> lines = new List<Line>();
        private FastColoredTextBox currentTB;
        private int lastLineUniqueId;
        private LinesAccessor linesAccessor;
        public TextSource(FastColoredTextBox currentTB)
        {
            this.CurrentTB = currentTB;
            this.linesAccessor = new LinesAccessor(this);
            this.Manager = new CommandManager(this);
            this.InitDefaultStyle();
        }

        public event EventHandler CurrentTBChanged;

        public event EventHandler<LineInsertedEventArgs> LineInserted;

        public event EventHandler<LineRemovedEventArgs> LineRemoved;

        public event EventHandler<TextChangedEventArgs> RecalcNeeded;

        public event EventHandler<TextChangedEventArgs> TextChanged;

        public event EventHandler<TextChangingEventArgs> TextChanging;
        public virtual int Count
        {
            get
            {
                return this.lines.Count;
            }
        }

        public FastColoredTextBox CurrentTB
        {
            get
            {
                return this.currentTB;
            }
            set
            {
                this.currentTB = value;
                this.OnCurrentTBChanged();
            }
        }

        public TextStyle DefaultStyle { get; set; }

        public bool IsNeedBuildRemovedLineIds
        {
            get
            {
                return (this.LineRemoved != null);
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public IList<string> Lines
        {
            get
            {
                return this.linesAccessor;
            }
        }

        internal CommandManager Manager { get; private set; }

        public virtual Line this[int i]
        {
            get
            {
                return this.lines[i];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual void Add(Line item)
        {
            this.InsertLine(this.Count, item);
        }

        public int BinarySearch(Line item, IComparer<Line> comparer)
        {
            return this.lines.BinarySearch(item, comparer);
        }

        public virtual void Clear()
        {
            this.RemoveLine(0, this.Count);
        }

        public virtual void ClearIsChanged()
        {
            foreach (Line line in this.lines)
            {
                line.IsChanged = false;
            }
        }

        public virtual bool Contains(Line item)
        {
            return this.lines.Contains(item);
        }

        public virtual void CopyTo(Line[] array, int arrayIndex)
        {
            this.lines.CopyTo(array, arrayIndex);
        }

        public virtual Line CreateLine()
        {
            return new Line(this.GenerateUniqueLineId());
        }

        public virtual void Dispose()
        {
        }

        public int GenerateUniqueLineId()
        {
            return this.lastLineUniqueId++;
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return this.lines.GetEnumerator();
        }

        public virtual int GetLineLength(int i)
        {
            return this.lines[i].Count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.lines as IEnumerator);
        }

        public virtual int IndexOf(Line item)
        {
            return this.lines.IndexOf(item);
        }

        public void InitDefaultStyle()
        {
            this.DefaultStyle = new TextStyle(null, null, FontStyle.Regular);
        }

        public virtual void Insert(int index, Line item)
        {
            this.InsertLine(index, item);
        }

        public virtual void InsertLine(int index, Line line)
        {
            this.lines.Insert(index, line);
            this.OnLineInserted(index);
        }

        public virtual bool IsLineLoaded(int iLine)
        {
            return (this.lines[iLine] != null);
        }

        public virtual bool LineHasFoldingEndMarker(int iLine)
        {
            return !string.IsNullOrEmpty(this.lines[iLine].FoldingEndMarker);
        }

        public virtual bool LineHasFoldingStartMarker(int iLine)
        {
            return !string.IsNullOrEmpty(this.lines[iLine].FoldingStartMarker);
        }

        public void OnLineInserted(int index)
        {
            this.OnLineInserted(index, 1);
        }

        public void OnLineInserted(int index, int count)
        {
            if (this.LineInserted != null)
            {
                this.LineInserted(this, new LineInsertedEventArgs(index, count));
            }
        }

        public void OnLineRemoved(int index, int count, List<int> removedLineIds)
        {
            if ((count > 0) && (this.LineRemoved != null))
            {
                this.LineRemoved(this, new LineRemovedEventArgs(index, count, removedLineIds));
            }
        }

        public void OnTextChanged(int fromLine, int toLine)
        {
            if (this.TextChanged != null)
            {
                this.TextChanged(this, new TextChangedEventArgs(Math.Min(fromLine, toLine), Math.Max(fromLine, toLine)));
            }
        }

        public virtual bool Remove(Line item)
        {
            int index = this.IndexOf(item);
            if (index >= 0)
            {
                this.RemoveLine(index);
                return true;
            }
            return false;
        }

        public virtual void RemoveAt(int index)
        {
            this.RemoveLine(index);
        }

        public virtual void RemoveLine(int index)
        {
            this.RemoveLine(index, 1);
        }

        public virtual void RemoveLine(int index, int count)
        {
            List<int> removedLineIds = new List<int>();
            if ((count > 0) && this.IsNeedBuildRemovedLineIds)
            {
                for (int i = 0; i < count; i++)
                {
                    removedLineIds.Add(this[index + i].UniqueId);
                }
            }
            this.lines.RemoveRange(index, count);
            this.OnLineRemoved(index, count, removedLineIds);
        }

        public virtual void SaveToFile(string fileName, Encoding enc)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, enc))
            {
                for (int i = 0; i < (this.Count - 1); i++)
                {
                    writer.WriteLine(this.lines[i].Text);
                }
                writer.Write(this.lines[this.Count - 1].Text);
            }
        }

        internal void NeedRecalc(TextChangedEventArgs args)
        {
            if (this.RecalcNeeded != null)
            {
                this.RecalcNeeded(this, args);
            }
        }

        internal void OnTextChanging()
        {
            string text = null;
            this.OnTextChanging(ref text);
        }

        internal void OnTextChanging(ref string text)
        {
            if (this.TextChanging != null)
            {
                TextChangingEventArgs e = new TextChangingEventArgs
                {
                    InsertingText = text
                };
                this.TextChanging(this, e);
                text = e.InsertingText;
                if (e.Cancel)
                {
                    text = string.Empty;
                }
            }
        }

        private void OnCurrentTBChanged()
        {
            if (this.CurrentTBChanged != null)
            {
                this.CurrentTBChanged(this, EventArgs.Empty);
            }
        }
        public class TextChangedEventArgs : EventArgs
        {
            public int iFromLine;
            public int iToLine;

            public TextChangedEventArgs(int iFromLine, int iToLine)
            {
                this.iFromLine = iFromLine;
                this.iToLine = iToLine;
            }
        }
    }
}