using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingHashST<K, V> : ISymbolTable<K, V> where K : IComparable
    {
        protected int M;
        protected int _count;
        protected SequentialSearchST<K, V>[] _st;

        public SeperateChainingHashST(int m)
        {
            M = m;
            Init();
        }

        public void Init()
        {
            _count = 0;
            _st = new SequentialSearchST<K, V>[M];
            for (int i = 0; i < M; i++)
                _st[i] = new SequentialSearchST<K, V>();
        }

        public V this[K key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public V Get(K key) => _st[Hash(key)].Get(key);

        public void Put(K key, V value) => _st[Hash(key)].Put(key, value);

        public bool Contains(K key) => _st[Hash(key)].Contains(key);

        public bool Delete(K key) => _st[Hash(key)].Delete(key);

        public IEnumerable<K> Keys()
        {
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (K key in st.Keys())
                    yield return key;
        }

        public int Size() => _count;

        public bool IsEmpty => _count == 0;

        protected int Hash(K key) => (key.GetHashCode() & 0x7fff_ffff) % M;
    }
}
