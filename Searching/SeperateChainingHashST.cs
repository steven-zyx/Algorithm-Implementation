using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingHashST<K, V> : HashST<K, V>
    {
        protected int _count;
        protected SequentialSearchST<K, V>[] _st;

        public SeperateChainingHashST(int m) : base(m)
        {
            _count = 0;
            _st = new SequentialSearchST<K, V>[M];
            for (int i = 0; i < M; i++)
                _st[i] = new SequentialSearchST<K, V>();
        }

        public SeperateChainingHashST() : this(17) { }

        public override V Get(K key) => _st[Hash(key)].Get(key);

        public override void Put(K key, V value)
        {
            bool result = _st[Hash(key)].PutAndCheck(key, value);
            if (result && ++_count > M * 8) Resize(M * 2);
        }

        public override bool Contains(K key) => _st[Hash(key)].Contains(key);

        public override bool Delete(K key)
        {
            bool result = _st[Hash(key)].Delete(key);
            if (result && --_count < M * 2 && M > 1) Resize(M / 2);
            return result;
        }

        public override IEnumerable<K> Keys()
        {
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (K key in st.Keys())
                    yield return key;
        }

        public override int Size() => _count;

        public override bool IsEmpty => _count == 0;

        protected virtual void Resize(int size)
        {
            SeperateChainingHashST<K, V> newST = new SeperateChainingHashST<K, V>(size);
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (var p in st.Pairs())
                    newST.Put(p.key, p.value);

            _st = newST._st;
            M = newST.M;
        }
    }
}
