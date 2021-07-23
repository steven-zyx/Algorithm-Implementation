using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Searching
{
    public class DoubleHashing<K, V> : LinearProbingHashST<K, V> where K : struct where V : struct
    {
        protected int _tombStone;

        public DoubleHashing() : this(31) { }

        public DoubleHashing(int size) : base(size)
        {
            _tombStone = 0;
        }

        public override void Put(K key, V value)
        {
            int i;
            int step = Step(key);
            for (i = Hash(key); _keys[i].HasValue; i = (i + step) % M)
                if (_keys[i].Equals(key))
                {
                    if (!_values[i].HasValue)
                        _tombStone--;
                    _values[i] = value;
                    return;
                }

            _keys[i] = key;
            _values[i] = value;
            if (++_count + _tombStone > M / 2) Resize(true);
        }

        public override bool Delete(K key)
        {
            int step = Step(key);
            for (int i = Hash(key); _keys[i].HasValue; i = (i + step) % M)
                if (_keys[i].Equals(key))
                {
                    if (_values[i].HasValue)
                    {
                        _values[i] = null;
                        _tombStone++;
                        if (--_count < M / 8) Resize(false);
                        return true;
                    }
                    else
                        return false;
                }
            return false;
        }

        public override V Get(K key)
        {
            int step = Step(key);
            for (int i = Hash(key); _keys[i].HasValue; i = (i + step) % M)
                if (key.Equals(_keys[i]))
                    if (_values[i].HasValue)
                        return _values[i].Value;
                    else
                        return default(V);
            return default(V);
        }

        public override bool Contains(K key)
        {
            int step = Step(key);
            for (int i = Hash(key); _keys[i].HasValue; i = (i + step) % M)
                if (key.Equals(_keys[i]))
                    return _values[i].HasValue;
            return false;
        }

        protected int Step(K key)
        {
            int result = (key.GetHashCode() & 0x7fff_ffff) % M;
            if (result == 0)
                result = M + 1;
            return result;
        }

        protected void Resize(bool doIncrease)
        {
            for (int i = 0; i < Util.Primes.Length; i++)
            {
                if (Util.Primes[i] == M)
                {
                    if (doIncrease)
                        Resize(Util.Primes[i + 1]);
                    else
                        Resize(Util.Primes[i - 1]);
                    return;
                }
            }
        }

        protected override void Resize(int size)
        {
            DoubleHashing<K, V> newST = new DoubleHashing<K, V>(size);
            for (int i = 0; i < _keys.Length; i++)
                if (_keys[i].HasValue && _values[i].HasValue)
                    newST.Put(_keys[i].Value, _values[i].Value);

            _keys = newST._keys;
            _values = newST._values;
            M = size;
            _tombStone = 0;
        }
    }
}
