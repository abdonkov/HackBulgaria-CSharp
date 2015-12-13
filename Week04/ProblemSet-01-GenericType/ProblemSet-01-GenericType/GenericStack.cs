using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class GenericStack<T>
    {
        private T[] items;
        private int topItem;
        private int capacity;

        public int Count { get { return items.Length; } }

        public GenericStack() : this(8) { }

        public GenericStack(int capacity)
        {
            this.capacity = capacity;
            items = new T[capacity];
            topItem = -1;
        }

        public T Peek()
        {
            if (topItem == -1) throw new InvalidOperationException();
            
            return items[topItem];
        }

        private void Resize(bool increase)
        {
            if (increase)
            {
                if (capacity == 0) capacity = 1;
                else capacity *= 2;
                T[] newItems = new T[capacity];
                for (int i = 0; i < items.Length; i++)
                {
                    newItems[i] = items[i];
                }

                items = newItems;
            }
            else
            {
                capacity /= 2;
                T[] newItems = new T[capacity];
                for (int i = 0; i < capacity; i++)
                {
                    newItems[i] = items[i];
                }

                items = newItems;
            }
        }

        public T Pop()
        {
            if (topItem == -1) throw new InvalidOperationException();

            if (topItem + 1 == capacity / 2) Resize(false);

            return items[topItem--];
        }

        public void Push(T item)
        {
            if (topItem + 1 == capacity) Resize(true);

            items[++topItem] = item;
        }

        public void Clear()
        {
            topItem = -1;
            capacity = 8;
            items = new T[capacity];
        }

        public bool Contains(T item)
        {
            for (int i = 0; i <= topItem; i++)
            {
                if (object.Equals(items[i], item)) return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            return topItem == -1 ? true : false;
        }
    }
}
