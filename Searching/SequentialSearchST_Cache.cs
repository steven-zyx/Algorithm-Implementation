using BasicDataStrcture;
using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SequentialSearchST_Cache<K, V> : SequentialSearchST<K, V> where K : IComparable
    {
        private Node_P<K, V> _cache;

        public SequentialSearchST_Cache()
        {
            _cache = null;
        }

        public override void Put(K key, V value)
        {
            var result = SearchByKey(key);
            if (result.isFound)
            {
                result.element.Value = value;
                _cache = result.element;
            }
            else
            {
                Node_P<K, V> newOne = new Node_P<K, V>(key, value, _start);
                _start = newOne;
                _count++;
                _cache = _start;
            }
        }

        public override V Get(K key)
        {
            var result = SearchByKey(key);
            if (result.isFound)
            {
                _cache = result.element;
                return result.element.Value;
            }
            else
                return default(V);
        }

        public override bool Contains(K key)
        {
            var result = SearchByKey(key);
            if (result.isFound)
            {
                _cache = result.element;
                return true;
            }
            return false;
        }

        protected override (bool isFound, Node_P<K, V> element) SearchByKey(K key)
        {
            if (_cache != null && _cache.Key.CompareTo(key) == 0)
                return (true, _cache);
            else
                return base.SearchByKey(key);
        }
    }
}
