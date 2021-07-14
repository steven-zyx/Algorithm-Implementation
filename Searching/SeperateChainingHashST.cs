using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingHashST<K, V> : HashST<K, V> where K : IComparable
    {
        protected int _count;
        protected SequentialSearchST<K, V>[] _st;

        public SeperateChainingHashST(int m) : base(m)
        {
            Init();
        }

        public override void Init()
        {
            _count = 0;
            _st = new SequentialSearchST<K, V>[M];
            for (int i = 0; i < M; i++)
                _st[i] = new SequentialSearchST<K, V>();
        }

        public override V Get(K key) => _st[Hash(key)].Get(key);

        public override void Put(K key, V value) => _st[Hash(key)].Put(key, value);

        public override bool Contains(K key) => _st[Hash(key)].Contains(key);

        public override bool Delete(K key) => _st[Hash(key)].Delete(key);

        public override IEnumerable<K> Keys()
        {
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (K key in st.Keys())
                    yield return key;
        }

        public override int Size() => _count;

        public override bool IsEmpty => _count == 0;
    }
}
