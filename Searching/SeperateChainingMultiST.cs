using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingMultiST<K, V> : SeperateChainingHashST<K, V>, IMultiSymbolTable<K, V>
    {
        protected SequentialSearchMultiST<K, V>[] MST => _st as SequentialSearchMultiST<K, V>[];

        public SeperateChainingMultiST() : base() { }

        protected SeperateChainingMultiST(int size) : base(size) { }

        protected override void Init()
        {
            _st = new SequentialSearchMultiST<K, V>[M];
            for (int i = 0; i < M; i++)
                _st[i] = new SequentialSearchMultiST<K, V>();
        }

        public override void Put(K key, V value)
        {
            _st[Hash(key)].Put(key, value);
            if (++_count > M * 8) Resize(M * 2);
        }

        public IEnumerable<V> GetAll(K key) => MST[Hash(key)].GetAll(key);

        public int DeleteAll(K key)
        {
            int result = MST[Hash(key)].DeleteAll(key);
            _count -= result;
            if (_count < M * 2 && M > 1) Resize(M / 2);
            return result;
        }

        protected override void Resize(int size)
        {
            SeperateChainingMultiST<K, V> newST = new SeperateChainingMultiST<K, V>(size);
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (var p in st.Pairs())
                    newST.Put(p.key, p.value);

            _st = newST._st;
            M = newST.M;
        }
    }
}
