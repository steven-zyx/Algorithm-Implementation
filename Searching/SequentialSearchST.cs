using BasicDataStrcture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SequentialSearchST<K, V> : ISymbolTable<K, V> where K : IComparable
    {
        private Node_P<K, V> _start;
        private int _count = 0;

        public void Init()
        {
            _start = null;
            _count = 0;
        }

        public V this[K key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public void Put(K key, V value)
        {
            for (var c = _start; c != null; c = c.Next)
            {
                if (c.Key.CompareTo(key) == 0)
                {
                    c.Value = value;
                    return;
                }
            }
            Node_P<K, V> newOne = new Node_P<K, V>(key, value, _start);
            _start = newOne;
            _count++;
        }

        public V Get(K key)
        {
            for (var c = _start; c != null; c = c.Next)
            {
                if (c.Key.CompareTo(key) == 0)
                    return c.Value;
            }
            return default(V);
        }

        public bool Delete(K key)
        {
            if (_start == null)
                return false;
            else if (_start.Key.CompareTo(key) == 0)
            {
                _start = _start.Next;
                _count--;
                return true;
            }

            for (var c = _start; c.Next != null; c = c.Next)
            {
                if (c.Next.Key.CompareTo(key) == 0)
                {
                    c.Next = c.Next.Next;
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public bool IsEmpty => _count == 0;
        public int Size() => _count;

        public bool Contains(K key)
        {
            for (var c = _start; c != null; c = c.Next)
            {
                if (c.Key.CompareTo(key) == 0)
                    return true;
            }
            return false;
        }

        public IEnumerable Keys()
        {
            for (var c = _start; c != null; c = c.Next)
                yield return c;
        }
    }
}
