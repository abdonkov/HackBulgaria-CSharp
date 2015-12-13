using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollections
{
    public class Map<T, U>
    {
        private DynamicArray<T> keys;
        private DynamicArray<U> values;
        public int Count { get { return keys.Count; } }
        public U this[T key]
        {
            get
            {
                int index = keys.IndexOf(key);
                if (index == -1) throw new ArgumentException();
                return values[index];
            }
            set
            {
                if (keys.IndexOf(key) != -1) throw new ArgumentException();
                Add(key, value);
            }
        }

        public Map()
        {
            keys = new DynamicArray<T>();
            values = new DynamicArray<U>();
        }

        public void Add(T key, U value)
        {
            if (keys.Contains(key)) throw new ArgumentException();
            keys.Add(key);
            values.Add(value);
        }

        public bool ContainsKey(T key)
        {
            return keys.Contains(key);
        }

        public bool ContainsValue(U value)
        {
            return values.Contains(value);
        }

        public bool Remove(T key)
        {
            int index = keys.IndexOf(key);
            if (index == -1) return false;
            keys.RemoveAt(index);
            values.RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }
    }
}
