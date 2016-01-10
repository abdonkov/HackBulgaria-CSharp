using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public enum ItemChangeType { Add, Insert, Remove, Replace, ChangedProperty }
    public delegate void CollectionChange(object sender, ItemChangeType changeType, int changedItemIndex, string changedItemInfo = null);

    public class NotifyCollection<T> : IList<T> where T: INotifyPropertyChanged
    {
        List<T> items;

        public T this[int index]
        {
            get
            {
                return items[index];
            }

            set
            {
                if (!object.Equals(items[index], value))
                {
                    items[index].PropertyChanged -= Item_PropertyChanged;
                    items[index] = value;
                    items[index].PropertyChanged += Item_PropertyChanged;
                    CollectionChanged(this, ItemChangeType.Replace, index);
                }
            }
        }

        public int Count {get { return items.Count; } }

        public bool IsReadOnly { get { return false; } }

        public event CollectionChange CollectionChanged;

        public NotifyCollection(CollectionChange collectionChagned)
        {
            items = new List<T>();
            CollectionChanged = collectionChagned;
        }

        public void Add(T item)
        {
            items.Add(item);
            CollectionChanged(this, ItemChangeType.Add, items.Count - 1);
            item.PropertyChanged += Item_PropertyChanged;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CollectionChanged(sender, ItemChangeType.ChangedProperty, IndexOf((T)sender), e.PropertyName);
        }

        public void Clear()
        {
            foreach (var item in items)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
            items.Clear();
            CollectionChanged(this, ItemChangeType.Remove, -1);
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            items.Insert(index, item);
            CollectionChanged(this, ItemChangeType.Insert, index);
            item.PropertyChanged += Item_PropertyChanged;
        }

        public bool Remove(T item)
        {
            int removedIndex = 0;
            foreach (var curItem in items)
            {
                if (object.Equals(curItem, item))
                {
                    RemoveAt(removedIndex);
                    return true;
                }
                removedIndex++;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            items[index].PropertyChanged -= Item_PropertyChanged;
            items.RemoveAt(index);
            CollectionChanged(this, ItemChangeType.Remove, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
        
}
