using System;

namespace GenericCollections
{
    public class DynamicArray<T>
    {
        private T[] array;
        private int capacity;
        private int count;
        public int Capacity { get { return capacity; } }
        public int Count { get { return count; } }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new ArgumentOutOfRangeException();
                return array[index];
            }
            set
            {
                if (index < 0 || index >= count) throw new ArgumentOutOfRangeException();
                array[index] = value;
            }
        }

        public DynamicArray() : this(10) { }

        public DynamicArray(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException();

            this.capacity = capacity;
            array = new T[capacity];
            count = 0;
        }

        public bool Contains(T value)
        {
            for (int i = 0; i < count; i++)
            {
                if (object.Equals(array[i], value)) return true;
            }
            return false;
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < count; i++)
            {
                if (object.Equals(array[i], value)) return i;
            }
            return -1;
        }

        private void Resize(bool increase)
        {
            if (increase)
            {
                if (capacity < 2048)
                {
                    if (capacity == 0) capacity = 1;
                    else capacity *= 2;
                }
                else capacity += 256;
            }
            else capacity /= 2;

            T[] newArray = new T[capacity];
            for (int i = 0; i < count; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }

        public void Add(T value)
        {
            if (count == capacity) Resize(true);
            array[count++] = value;
        }

        public bool InsertAt(int index, T value)
        {
            if (index < 0 || index > count) return false;
            if (index == count)
            {
                Add(value);
                return true;
            }
            if (count == capacity) Resize(true);

            for (int i = count - 1; i >= 0; i--)
            {
                array[i + 1] = array[i];
                if (index == i)
                {
                    array[i] = value;
                    break;
                }
            }
            count++;
            return true;
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;
            if (index == count - 1)
            {
                count--;
                if (count == capacity / 3) Resize(false);
                return true;
            }

            for (int i = index; i < count - 1; i++)
            {
                array[i] = array[i + 1];
            }

            count--;
            if (count == capacity / 3) Resize(false);
            return true;
        }

        public void Clear()
        {
            capacity = 10;
            array = new T[capacity];
            count = 0;
        }

        public T[] ToArray()
        {
            T[] simpleArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                simpleArray[i] = array[i];
            }
            return simpleArray;
        }
    }
}
