using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class RandomSymbolTable<K, V>
    {
        private ISymbolTable<K, V> _st;
        private RandomQueue<K> _queue;

        public RandomSymbolTable()
        {
            _st = new SeperateChainingHashST<K, V>();
            _queue = new RandomQueue<K>();
        }

        public void Put(K key, V value)
        {
            if (!_st.Contains(key))
                _queue.Enqueue(key);
            _st.Put(key, value);
        }

        public V Get(K key) => _st.Get(key);

        public K RandomDelete()
        {
            K key = _queue.Dequeue();
            _st.Delete(key);
            return key;
        }
    }
}
