using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class LinearProbingHashST<K, V> : HashST<K, V> where K : struct where V : struct
    {
        protected int _count;
        protected K?[] _keys;
        protected V?[] _values;

        public LinearProbingHashST() : this(16) { }

        protected LinearProbingHashST(int size) : base(size)
        {
            _count = 0;
            _keys = new K?[M];
            _values = new V?[M];
        }

        public override void Put(K key, V value)
        {
            int i;
            for (i = Hash(key); _keys[i].HasValue; Increment(ref i))
                if (key.Equals(_keys[i]))
                {
                    _values[i] = value;
                    return;
                }
            _keys[i] = key;
            _values[i] = value;
            _count++;

            if (_count > M / 2) Resize(M * 2);
        }

        public override V Get(K key)
        {
            for (int i = Hash(key); _keys[i].HasValue; Increment(ref i))
                if (key.Equals(_keys[i]))
                    return _values[i].Value;
            return default(V);
        }

        public override bool Contains(K key)
        {
            for (int i = Hash(key); _keys[i].HasValue; Increment(ref i))
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
            while (_keys[i].HasValue)
            {
                K keyRedo = _keys[i].Value;
                V valueRedo = _values[i].Value;
                Clear(i);
                Put(keyRedo, valueRedo);
                Increment(ref i);
            }

            if (_count < M / 8) Resize(M / 2);
            return true;
        }

        public override IEnumerable<K> Keys()
        {
            foreach (K? key in _keys)
                if (key.HasValue)
                    yield return key.Value;
        }

        public override int Size() => _count;

        public override bool IsEmpty => _count == 0;

        protected void Increment(ref int i)
        {
            i = (i + 1) % M;
        }

        protected void Clear(int i)
        {
            _keys[i] = null;
            _values[i] = null;
            _count--;
        }

        protected virtual void Resize(int size)
        {
            LinearProbingHashST<K, V> newST = new LinearProbingHashST<K, V>(size);
            for (int i = 0; i < M; i++)
                if (_keys[i].HasValue)
                    newST.Put(_keys[i].Value, _values[i].Value);

            _keys = newST._keys;
            _values = newST._values;
            M = newST.M;
        }
    }
}
