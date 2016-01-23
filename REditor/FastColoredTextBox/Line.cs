namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public class Line : IList<Char>, ICollection<Char>, IEnumerable<Char>, IEnumerable
    {
        protected List<Char> chars;

        internal Line(int uid)
        {
            this.UniqueId = uid;
            this.chars = new List<Char>();
        }

        public int AutoIndentSpacesNeededCount { get; internal set; }

        public Brush BackgroundBrush { get; set; }

        public int Count
        {
            get
            {
                return this.chars.Count;
            }
        }

        public string FoldingEndMarker { get; set; }

        public string FoldingStartMarker { get; set; }

        public bool IsChanged { get; set; }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public DateTime LastVisit { get; set; }

        public int StartSpacesCount
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].c != ' ')
                    {
                        return num;
                    }
                    num++;
                }
                return num;
            }
        }

        public virtual string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder(this.Count);
                foreach (Char ch in this)
                {
                    builder.Append(ch.c);
                }
                return builder.ToString();
            }
        }

        public int UniqueId { get; private set; }

        public Char this[int index]
        {
            get
            {
                return this.chars[index];
            }
            set
            {
                this.chars[index] = value;
            }
        }

        public void Add(Char item)
        {
            this.chars.Add(item);
        }

        public virtual void AddRange(IEnumerable<Char> collection)
        {
            this.chars.AddRange(collection);
        }

        public void Clear()
        {
            this.chars.Clear();
        }

        public void ClearFoldingMarkers()
        {
            this.FoldingStartMarker = null;
            this.FoldingEndMarker = null;
        }

        /// <summary>
        /// Clears style of chars, delete folding markers
        /// </summary>
        public void ClearStyle(StyleIndex styleIndex)
        {
            FoldingStartMarker = null;
            FoldingEndMarker = null;
            for (int i = 0; i < Count; i++)
            {
                Char c = this[i];
                c.style &= ~styleIndex;
                this[i] = c;
            }
        }

        public bool Contains(Char item)
        {
            return this.chars.Contains(item);
        }

        public void CopyTo(Char[] array, int arrayIndex)
        {
            this.chars.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Char> GetEnumerator()
        {
            return this.chars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.chars.GetEnumerator();
        }

        public int IndexOf(Char item)
        {
            return this.chars.IndexOf(item);
        }

        public void Insert(int index, Char item)
        {
            this.chars.Insert(index, item);
        }

        public bool Remove(Char item)
        {
            return this.chars.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.chars.RemoveAt(index);
        }

        public virtual void RemoveRange(int index, int count)
        {
            this.chars.RemoveRange(index, count);
        }

        public virtual void TrimExcess()
        {
            this.chars.TrimExcess();
        }
    }
}