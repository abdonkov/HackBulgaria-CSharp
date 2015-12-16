using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollections
{
    public class HashMap<T, U>
    {
        class KeyValuePair
        {
            T key;
            U value;
            public T Key { get { return key; } }
            public U Value { get { return value; } }

            public KeyValuePair(T key, U value)
            {
                this.key = key;
                this.value = value;
            }

            public override bool Equals(object obj)
            {
                if (obj is KeyValuePair)
                {
                    var kvp = obj as KeyValuePair;
                    if (!object.Equals(key, kvp.key)) return false;
                    if (!object.Equals(value, kvp.value)) return false;
                    return true;
                }
                else return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (17 * 23 + key.GetHashCode()) * 27 + value.GetHashCode();
                }
            }
        }

        private DynamicArray<LinkedList<KeyValuePair>> entries;
        private bool[] usedBuckets;
        public int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < usedBuckets.Length; i++)
                {
                    if (usedBuckets[i])
                    {
                        count += entries[i].Count;
                    }
                }
                return count;
            }
        }
        public HashMap()
        {
            entries = new DynamicArray<LinkedList<KeyValuePair>>(100);
            usedBuckets = new bool[100];
        }

        public void Add(T key, U value)
        {
            if (ContainsKey(key)) throw new ArgumentException();
            int bucketToInsert = key.GetHashCode() % 100;
            if (entries[bucketToInsert] == null)
            {
                entries[bucketToInsert] = new LinkedList<KeyValuePair>();
                entries[bucketToInsert].Add(new KeyValuePair(key, value));
                usedBuckets[bucketToInsert] = true;
            }
            else
            {
                entries[bucketToInsert].Add(new KeyValuePair(key, value));
                usedBuckets[bucketToInsert] = true;
            }

        }

        public bool ContainsKey(T key)
        {
            int bucket = key.GetHashCode() % 100;
            if (entries[bucket] == null) return false;
            foreach (KeyValuePair kvp in entries[bucket])
            {
                if (object.Equals(kvp.Key, key)) return true;
            }
            return false;
        }

        public bool ContainsValue(U value)
        {
            for (int i = 0; i < usedBuckets.Length; i++)
            {
                if (usedBuckets[i])
                {
                    foreach (KeyValuePair kvp in entries[i])
                    {
                        if (object.Equals(kvp.Value, value)) return true;
                    }
                }
            }
            return false;
        }

        public bool Remove(T key)
        {
            int bucket = key.GetHashCode() % 100;
            if (entries[bucket] == null) return false;
            bool removed = false;
            foreach (KeyValuePair kvp in entries[bucket])
            {
                if (object.Equals(kvp.Key, key))
                {
                    entries[bucket].Remove(kvp);
                    removed = true;
                    break;
                }
            }
            if (removed)
            {
                if (entries[bucket].Count == 0) usedBuckets[bucket] = false;
                return true;
            }
            else return false;
        }

        public void Clear()
        {
            entries = new DynamicArray<LinkedList<KeyValuePair>>(100);
            usedBuckets = new bool[100];
        }
    }
}
