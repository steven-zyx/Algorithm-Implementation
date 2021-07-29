using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SeperateChainingSET<K> : ISet<K>
    {
        protected SequentialSearchSET<K>[] _set;
        protected int M;
        protected int _count;

        public SeperateChainingSET() : this(17) { }

        protected SeperateChainingSET(int size)
        {
            M = size;
            _count = 0;
            Init();
        }

        protected virtual void Init()
        {
            _set = new SequentialSearchSET<K>[M];
            for (int i = 0; i < M; i++)
                _set[i] = new SequentialSearchSET<K>();
        }

        public virtual void Put(K key)
        {
            bool added = _set[Hash(key)].PutAndCheck(key);
            if (added && ++_count > M * 8) Resize(M * 2);
        }

        public bool Delete(K key)
        {
            bool deleted = _set[Hash(key)].Delete(key);
            if (deleted && --_count < M * 2 && M > 1) Resize(M / 2);
            return deleted;
        }

        public bool Contains(K key) => _set[Hash(key)].Contains(key);

        public IEnumerable<K> Keys()
        {
            foreach (var subSet in _set)
                foreach (var key in subSet.Keys())
                    yield return key;
        }

        public int Size() => _count;

        public bool IsEmpty => _count == 0;

        protected int Hash(K key) => (key.GetHashCode() & 0x7fff_ffff) % M;

        protected virtual void Resize(int size)
        {
            SeperateChainingSET<K> newSET = new SeperateChainingSET<K>(size);
            foreach (K key in Keys())
                newSET.Put(key);

            _set = newSET._set;
            M = newSET.M;
        }
    }
}
