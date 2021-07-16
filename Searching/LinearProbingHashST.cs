using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class LinearProbingHashST<K, V> : HashST<K, V>
    {
        protected int _count;
        protected K[] _keys;
        protected V[] _values;
        protected bool[] _isOccupied;

        public LinearProbingHashST() : this(16) { }

        protected LinearProbingHashST(int size) : base(size)
        {
            _count = 0;
            _keys = new K[M];
            _values = new V[M];
            _isOccupied = new bool[M];
        }

        public override void Put(K key, V value)
        {
            int i;
            for (i = Hash(key); _isOccupied[i]; Increment(ref i))
                if (key.Equals(_keys[i]))
                {
                    _values[i] = value;
                    return;
                }
            _keys[i] = key;
            _values[i] = value;
            _isOccupied[i] = true;
            _count++;

            if (_count > M / 2) Resize(M * 2);
        }

        public override V Get(K key)
        {
            for (int i = Hash(key); _isOccupied[i]; Increment(ref i))
                if (key.Equals(_keys[i]))
                    return _values[i];
            return default(V);
        }


        public override bool Contains(K key)
        {
            for (int i = Hash(key); _isOccupied[i]; Increment(ref i))
                if (key.Equals(_keys[i]))
                    return true;
            return false;
        }

        public override bool Delete(K key)
        {
            if (!Contains(key)) return false;

            int i = Hash(key);
            while (!key.Equals(_keys[i]))
                Increment(ref i);
            Clear(i);

            Increment(ref i);
            while (_isOccupied[i])
            {
                K keyRedo = _keys[i];
                V valueRedo = _values[i];
                Clear(i);
                Put(keyRedo, valueRedo);
                Increment(ref i);
            }

            if (_count < M / 8) Resize(M / 2);
            return true;
        }

        public override IEnumerable<K> Keys()
        {
            foreach (K key in _keys)
                if (key != null)
                    yield return key;
        }

        public override int Size() => _count;

        public override bool IsEmpty => _count == 0;

        protected void Increment(ref int i)
        {
            i = (i + 1) % M;
        }

        protected void Clear(int i)
        {
            _keys[i] = default(K);
            _values[i] = default(V);
            _isOccupied[i] = false;
            _count--;
        }

        protected void Resize(int size)
        {
            LinearProbingHashST<K, V> newST = new LinearProbingHashST<K, V>(size);
            for (int i = 0; i < M; i++)
                if (_isOccupied[i])
                    newST.Put(_keys[i], _values[i]);

            _keys = newST._keys;
            _values = newST._values;
            _isOccupied = newST._isOccupied;
            M = newST.M;
        }
    }
}
