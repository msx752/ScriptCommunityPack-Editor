namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class LinesAccessor : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
    {
        private IList<Line> ts;

        public LinesAccessor(IList<Line> ts)
        {
            this.ts = ts;
        }

        public int Count
        {
            get
            {
                return this.ts.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public string this[int index]
        {
            get
            {
                return this.ts[index].Text;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string item)
        {
            for (int i = 0; i < this.ts.Count; i++)
            {
                if (this.ts[i].Text == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            for (int i = 0; i < this.ts.Count; i++)
            {
                array[i + arrayIndex] = this.ts[i].Text;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < this.ts.Count; i++)
            {
                yield return this.ts[i].Text;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOf(string item)
        {
            for (int i = 0; i < this.ts.Count; i++)
            {
                if (this.ts[i].Text == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, string item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}