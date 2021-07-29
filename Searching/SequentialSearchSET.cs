using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class SequentialSearchSET<K> : ISet<K>
    {
        protected Node<K> _start;
        protected int _count;

        public virtual void Put(K key)
        {
            PutAndCheck(key);
        }

        public bool PutAndCheck(K key)
        {
            for (var c = _start; c != null; c = c.Next)
                if (c.Value.Equals(key))
                    return true;
            Node<K> newStart = new Node<K>(key, _start);
            _start = newStart;
            _count++;
            return false;
        }

        public bool Delete(K key)
        {
            Node<K> beforeStart = new Node<K>(key, _start);
            for (var c = beforeStart; c.Next != null; c = c.Next)
                if (c.Next.Value.Equals(key))
                {
                    c.Next = c.Next.Next;
                    _start = beforeStart.Next;
                    _count--;
                    return true;
                }
            return false;
        }

        public bool Contains(K key)
        {
            for (var c = _start; c != null; c = c.Next)
                if (c.Value.Equals(key))
                    return true;
            return false;
        }

        public IEnumerable<K> Keys()
        {
            for (var c = _start; c != null; c = c.Next)
                yield return c.Value;
        }

        public int Size() => _count;

        public bool IsEmpty => _count == 0;
    }
}
