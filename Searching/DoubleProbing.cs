using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class DoubleProbing<K, V> : SeperateChainingHashST<K, V>
    {
        public DoubleProbing() : base() { }

        public DoubleProbing(int size) : base(size) { }

        public override void Put(K key, V value)
        {
            ISymbolTable<K, V> st1 = _st[Hash(key)];
            ISymbolTable<K, V> st2 = _st[Hash2(key)];

            if (st1.Contains(key)) st1.Put(key, value);
            else if (st2.Contains(key)) st2.Put(key, value);
            else
            {
                if (st1.Size() < st2.Size())
                    st1.Put(key, value);
                else
                    st2.Put(key, value);
                if (++_count > M * 8) Resize(M * 2);
            }
        }

        public override V Get(K key)
        {
            V result = _st[Hash(key)].Get(key);
            if (result.Equals(default(V)))
            {
                result = _st[Hash2(key)].Get(key);
                if (result.Equals(default(V)))
                    result = default(V);
            }
            return result;
        }

        public override bool Delete(K key)
        {
            bool result = _st[Hash(key)].Delete(key) || _st[Hash2(key)].Delete(key);
            if (result && --_count < M * 2 && M > 1)
                Resize(M / 2);
            return result;
        }

        public override bool Contains(K key) =>
            _st[Hash(key)].Contains(key) || _st[Hash2(key)].Contains(key);

        protected override void Resize(int size)
        {
            DoubleProbing<K, V> newST = new DoubleProbing<K, V>(size);
            foreach (SequentialSearchST<K, V> st in _st)
                foreach (var p in st.Pairs())
                    newST.Put(p.key, p.value);

            _st = newST._st;
            M = newST.M;
        }

        protected override int Hash(K key) => (key.GetHashCode() * 11 & 0x7fff_ffff) % M;

        protected int Hash2(K key) => (key.GetHashCode() * 13 & 0x7fff_ffff) % M;
    }
}
