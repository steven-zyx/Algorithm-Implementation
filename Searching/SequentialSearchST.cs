using BasicDataStrcture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SequentialSearchST<K, V> : ISymbolTable<K, V>
    {
        protected Node_P<K, V> _start;
        protected int _count = 0;

        public SequentialSearchST()
        {
            _start = null;
            _count = 0;
        }

        public virtual void Put(K key, V value) => PutAndCheck(key, value);

        public bool PutAndCheck(K key, V value)
        {
            var result = SearchByKey(key);
            if (result.isFound)
                result.element.Value = value;
            else
            {
                Node_P<K, V> newOne = new Node_P<K, V>(key, value, _start);
                _start = newOne;
                _count++;
            }
            return !result.isFound;
        }

        public virtual V Get(K key)
        {
            var result = SearchByKey(key);
            return result.isFound ? result.element.Value : default(V);
        }

        public bool Delete(K key)
        {
            Node_P<K, V> beforeStart = new Node_P<K, V>(default(K), default(V), _start);
            for (var p = beforeStart; p.Next != null; p = p.Next)
                if (p.Next.Key.Equals(key))
                {
                    p.Next = p.Next.Next;
                    _start = beforeStart.Next;
                    _count--;
                    return true;
                }
            return false;
        }

        public bool IsEmpty => _count == 0;

        public int Size() => _count;

        public virtual bool Contains(K key) => SearchByKey(key).isFound;

        public IEnumerable<K> Keys()
        {
            for (var c = _start; c != null; c = c.Next)
                yield return c.Key;
        }

        public IEnumerable<(K key, V value)> Pairs()
        {
            for (var c = _start; c != null; c = c.Next)
                yield return (c.Key, c.Value);
        }

        protected virtual (bool isFound, Node_P<K, V> element) SearchByKey(K key)
        {
            for (var c = _start; c != null; c = c.Next)
                if (key.Equals(c.Key))
                    return (true, c);
            return (false, null);
        }
    }
}
