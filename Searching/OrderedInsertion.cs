using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class OrderedInsertion<K, V> : BinarySearchST<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            if (_count == 0 || key.CompareTo(_keys[_count - 1]) > 0)
            {
                _keys[_count] = key;
                _values[_count] = value;
                _count++;
                if (_count >= _keys.Length)
                    Resize(_keys.Length * 2);
            }
            else
                base.Put(key, value);
        }
    }
}
