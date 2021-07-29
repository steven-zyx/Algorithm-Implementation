using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingMultiSET<K> : SeperateChainingSET<K>, IMultiSet<K>
    {
        protected SequentialSearchMultiSET<K>[] MSET => _set as SequentialSearchMultiSET<K>[];

        public SeperateChainingMultiSET() : base() { }

        protected SeperateChainingMultiSET(int size) : base(size) { }

        protected override void Init()
        {
            _set = new SequentialSearchMultiSET<K>[M];
            for (int i = 0; i < M; i++)
                _set[i] = new SequentialSearchMultiSET<K>();
        }

        public override void Put(K key)
        {
            MSET[Hash(key)].Put(key);
            if (++_count > M * 8) Resize(M * 2);
        }

        public int Count(K key) => MSET[Hash(key)].Count(key);

        public int DeleteAll(K key)
        {
            int result = MSET[Hash(key)].DeleteAll(key);
            _count -= result;
            if (_count < M * 2 && M > 1) Resize(M / 2);
            return result;
        }

        protected override void Resize(int size)
        {
            SeperateChainingMultiSET<K> newSET = new SeperateChainingMultiSET<K>(size);
            foreach (K key in Keys())
                newSET.Put(key);

            _set = newSET._set;
            M = newSET.M;
        }
    }
}
