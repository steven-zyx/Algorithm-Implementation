using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class LinearProbingHashST<K, V> : HashST<K, V> where K : IComparable
    {
        protected int _count;
        protected K[] _keys;
        protected V[] _values;

        public LinearProbingHashST() : base(16)
        {
            Init();
        }

        public override void Init()
        {
            _count = 0;
            _keys = new K[M];
            _values = new V[M];
        }

        public override void Put(K key, V value)
        {
            int i;
            for (i = Hash(key); _keys[i] != null; i = (i + 1) % M)
                if (key.CompareTo(_keys[i]) == 0)
                {
                    _values[i] = value;
                    return;
                }
            _keys[i] = key;
            _values[i] = value;
            _count++;
        }

        public override V Get(K key)
        {
            for (int i = Hash(key); _keys[i] != null; i = (i + 1) % M)
                if (key.CompareTo(_keys[i]) == 0)
                    return _values[i];
            return default(V);
        }


        public override bool Contains(K key)
        {
            for (int i = Hash(key); _keys[i] != null; i = (i + 1) % M)
                if (key.CompareTo(_keys[i]) == 0)
                    return true;
            return false;
        }

        public override bool Delete(K key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<K> Keys()
        {
            foreach (K key in _keys)
                if (key != null)
                    yield return key;
        }

        public override int Size() => _count;

        public override bool IsEmpty => _count == 0;
    }
}
