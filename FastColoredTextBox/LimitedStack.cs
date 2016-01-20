namespace FastColoredTextBoxNS
{
    using System;

    public class LimitedStack<T>
    {
        private int count;
        private T[] items;
        private int start;

        public LimitedStack(int maxItemCount)
        {
            this.items = new T[maxItemCount];
            this.count = 0;
            this.start = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public int MaxItemCount
        {
            get
            {
                return this.items.Length;
            }
        }

        private int LastIndex
        {
            get
            {
                return (((this.start + this.count) - 1) % this.items.Length);
            }
        }

        public void Clear()
        {
            this.items = new T[this.items.Length];
            this.count = 0;
            this.start = 0;
        }

        public T Peek()
        {
            if (this.count == 0)
            {
                return default(T);
            }
            return this.items[this.LastIndex];
        }

        public T Pop()
        {
            if (this.count == 0)
            {
                throw new Exception("Stack is empty");
            }
            int lastIndex = this.LastIndex;
            T local = this.items[lastIndex];
            this.items[lastIndex] = default(T);
            this.count--;
            return local;
        }

        public void Push(T item)
        {
            if (this.count == this.items.Length)
            {
                this.start = (this.start + 1) % this.items.Length;
            }
            else
            {
                this.count++;
            }
            this.items[this.LastIndex] = item;
        }

        public T[] ToArray()
        {
            T[] localArray = new T[this.count];
            for (int i = 0; i < this.count; i++)
            {
                localArray[i] = this.items[(this.start + i) % this.items.Length];
            }
            return localArray;
        }
    }
}