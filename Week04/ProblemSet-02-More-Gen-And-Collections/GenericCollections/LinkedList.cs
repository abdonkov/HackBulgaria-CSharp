using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericCollections
{
    public class LinkedList<T> : IEnumerable<T>
    {
        class Node
        {
            T value;
            Node next;
            public T Value
            {
                get { return value; }
                set { this.value = value; }
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public Node(T value, Node next)
            {
                this.value = value;
                this.next = next;
            }
        }

        private Node head;
        private int count;
        public int Count { get { return count; } }
        public T this[int index]
        {
            get
            {
                if (index >= count) throw new ArgumentOutOfRangeException();
                Node curNode = head;
                for (int i = 1; i <= index; i++)
                {
                    curNode = curNode.Next;
                }
                return curNode.Value;
            }
            set
            {
                if (index >= count) throw new ArgumentOutOfRangeException();
                Node curNode = head;
                for (int i = 1; i <= index; i++)
                {
                    curNode = curNode.Next;
                }
                if (curNode == null) throw new ArgumentOutOfRangeException();
                curNode.Value = value;
            }
        }

        public LinkedList()
        {
            head = null;
            count = 0;
        }

        public void Add(T value)
        {
            if (head == null) head = new Node(value, null);
            else
            {
                Node curNode = head;
                while (curNode.Next != null)
                {
                    curNode = curNode.Next;
                }
                curNode.Next = new Node(value, null);
            }
            count++;
        }

        public bool InsertAfter(T key, T value)
        {
            if (count == 0) return false;

            Node curNode = head;
            while (!object.Equals(curNode.Value, key))
            {
                curNode = curNode.Next;
                if (curNode == null) return false;
            }
            curNode.Next = new Node(value, curNode.Next);
            count++;
            return true;
        }

        public bool InsertBefore(T key, T value)
        {
            if (count == 0) return false;

            Node lastNode = head;
            Node curNode = head;
            while (!object.Equals(curNode.Value, key))
            {
                lastNode = curNode;
                curNode = curNode.Next;
                if (curNode == null) return false;
            }

            if (object.ReferenceEquals(curNode, head))
            {
                head = new Node(value, head);
                count++;
                return true;
            }
            else
            {
                lastNode.Next = new Node(value, lastNode.Next);
                count++;
                return true;
            }
        }

        public bool InsertAt(int index, T value)
        {
            if (index > count) return false;

            if (index == 0)
            {
                if (count == 0) head = new Node(value, null);
                else head = new Node(value, head);
            }
            else
            {
                Node lastNode = head;
                Node curNode = head;
                for (int i = 1; i < index; i++)
                {
                    lastNode = curNode;
                    curNode = curNode.Next;
                }
                lastNode.Next = new Node(value, lastNode.Next);
            }

            count++;
            return true;
        }

        public bool Remove(T value)
        {
            if (count == 0) return false;
            Node lastNode = head;
            Node curNode = head;
            while (!object.Equals(curNode.Value, value))
            {
                lastNode = curNode;
                curNode = curNode.Next;
                if (curNode == null) return false;
            }

            if (object.ReferenceEquals(curNode, head))
            {
                if (count == 1)
                {
                    head = null;
                }
                else
                {
                    head = head.Next;
                }
                count--;
                return true;
            }
            else
            {
                lastNode.Next = curNode.Next;
                count--;
                return true;
            }
        }

        public bool RemoveAt(int index)
        {
            if (index >= count) return false;

            if (index == 0)
            {
                if (count == 1) head = null;
                else head = head.Next;
            }
            else
            {
                Node curNode = head;
                for (int i = 1; i < index; i++)
                {
                    curNode = curNode.Next;
                }
                curNode.Next = curNode.Next.Next;
            }

            count++;
            return true;
        }

        public void Clear()
        {
            if (count != 0)
            {
                Node curNode = head;
                Node temp;
                while (curNode.Next != null)
                {
                    temp = curNode.Next;
                    curNode.Next = null;
                    curNode = temp;
                }
                head = null;
                count = 0;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node curNode = head;
            while (curNode != null)
            {
                yield return curNode.Value;
                curNode = curNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node curNode = head;
            while (curNode != null)
            {
                yield return curNode;
                curNode = curNode.Next;
            }
        }
    }
}
