using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class BinarySearch_Cache<K, V> : BinarySearchST<K, V> where K : IComparable
    {
        private int _cache;

        public override void Put(K key, V value)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
            {
                _values[index] = value;
                _cache = index;
            }
            else
            {
                for (int i = _count; i > index; i--)
                {
                    _keys[i] = _keys[i - 1];
                    _values[i] = _values[i - 1];
                }
                _keys[index] = key;
                _values[index] = value;
                _cache = index;

                _count++;
                if (_count >= _keys.Length)
                    Resize(_keys.Length * 2);
            }
        }

        public override int Rank(K key)
        {
            if (KeyEquals(_cache, key))
                return _cache;
            else
            {
                _cache = base.Rank(key);
                return _cache;
            }
        }

        public override K Select(int index)
        {
            _cache = index;
            return _keys[index];
        }
    }
}
