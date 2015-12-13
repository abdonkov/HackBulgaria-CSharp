using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class GenericDequeue<T>
    {
        private List<T> items;
        private int lastItem;

        public GenericDequeue() : this(8) { }

        public GenericDequeue(int capacity)
        {
            items = new List<T>(capacity);
            lastItem = -1;
        }

        public T RemoveFromFront()
        {
            if (lastItem == -1) throw new InvalidOperationException();

            T itemToReturn = items[0];
            items.RemoveAt(0);
            lastItem--;
            return itemToReturn;
        }

        public T RemoveFromEnd()
        {
            if (lastItem == -1) throw new InvalidOperationException();

            T itemToReturn = items[lastItem];
            items.RemoveAt(lastItem);
            lastItem--;
            return itemToReturn;
        }

        public void AddToFront(T item)
        {
            items.Insert(0, item);
            lastItem++;
        }

        public void AddToEnd(T item)
        {
            items.Add(item);
            lastItem++;
        }

        public T PeekFromFront()
        {
            return items[0];
        }

        public T PeekFromEnd()
        {
            return items[lastItem];
        }

        public void Clear()
        {
            items.Clear();
            lastItem = -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i <= lastItem; i++)
            {
                if (object.Equals(items[i], item)) return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            return lastItem == -1 ? true : false;
        }
    }
}
