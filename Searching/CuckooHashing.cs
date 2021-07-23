using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Searching
{
    public class CuckooHashing<K, V> : LinearProbingHashST<K, V> where K : struct where V : struct
    {
        private int _ranNum1;
        private int _ranNum2;
        private K?[] _keys2;
        private V?[] _values2;
        protected readonly int[] _primes = {
            3, 7, 13, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191, 16381, 32749, 65521, 131071, 262139, 524287, 1048573, 2097143, 4194301,
            8388593, 16777213, 33554393, 67108859, 134217689, 268435399, 536870909, 1073741789, 2147483647 };

        public CuckooHashing(int size) : base(size)
        {
            _ranNum1 = Util.Ran.Next(0, int.MaxValue);
            _ranNum2 = Util.Ran.Next(0, int.MaxValue);
            _keys2 = new K?[size];
            _values2 = new V?[size];
        }

        public CuckooHashing() : this(31) { }

        public override V Get(K key)
        {
            int i = Hash(key);
            if (key.Equals(_keys[i]))
                return _values[i].Value;
            else
            {
                i = Hash2(key);
                if (key.Equals(_keys2[i]))
                    return _values2[i].Value;
                else
                    return default(V);
            }
        }

        public override bool Contains(K key) => key.Equals(_keys[Hash(key)]) || key.Equals(_keys[Hash2(key)]);

        public override bool Delete(K key)
        {
            int i = Hash(key);
            if (key.Equals(_keys[i]))
            {
                _keys[i] = null;
                _values[i] = null;
            }
            else
            {
                i = Hash2(key);
                if (key.Equals(_keys2[i]))
                {
                    _keys2[i] = null;
                    _values2[i] = null;
                }
                else
                    return false;
            }

            if (--_count < M / 4) Resize(false);
            return true;
        }

        public override void Put(K key, V value)
        {
            RecrusivePut(key, value, 1);
        }

        private void RecrusivePut(K key, V value, int level)
        {
            if (level > M)
            {
                ReHash();
                RecrusivePut(key, value, 1);
                return;
            }

            //Choose table and index to insert the new key
            K?[] keys;
            V?[] values;
            int i;
            if (level % 2 == 1)
            {
                i = Hash(key);
                keys = _keys;
                values = _values;
            }
            else
            {
                i = Hash2(key);
                keys = _keys2;
                values = _values2;
            }

            //Insert if empty, update value if key is equal, 
            //otherwise, replace the old key and value with new one, try to insert the old key into the other table.
            if (keys[i].HasValue)
            {
                if (keys[i].Equals(key))
                    values[i] = value;
                else
                {
                    K oldKey = keys[i].Value;
                    V oldValue = values[i].Value;
                    keys[i] = key;
                    values[i] = value;
                    RecrusivePut(oldKey, oldValue, ++level);
                }
            }
            else
            {
                keys[i] = key;
                values[i] = value;
                if (++_count > M)
                    Resize(true);
            }
        }

        private void ReHash()
        {
            Resize(M);
        }

        protected void Resize(bool doIncrease)
        {
            for (int i = 0; i < _primes.Length; i++)
            {
                if (_primes[i] == M)
                {
                    if (doIncrease)
                        Resize(_primes[i + 1]);
                    else
                        Resize(_primes[i - 1]);
                    return;
                }
            }
        }

        protected override void Resize(int size)
        {
            CuckooHashing<K, V> newST = new CuckooHashing<K, V>(size);
            for (int i = 0; i < M; i++)
                if (_keys[i].HasValue)
                    newST.Put(_keys[i].Value, _values[i].Value);
            for (int j = 0; j < M; j++)
                if (_keys2[j].HasValue)
                    newST.Put(_keys2[j].Value, _values2[j].Value);

            _keys = newST._keys;
            _values = newST._values;
            _keys2 = newST._keys2;
            _values2 = newST._values2;

            M = size;
            _ranNum1 = newST._ranNum1;
            _ranNum2 = newST._ranNum2;
        }

        protected override int Hash(K key)
        {
            int hash = key.GetHashCode();
            return ((hash >> 16) * _ranNum1 + (hash & 0xffff) * _ranNum1 & 0x7fff_ffff) % M;
        }
        protected int Hash2(K key)
        {
            int hash = key.GetHashCode();
            return ((hash >> 16) * _ranNum2 + (hash & 0xffff) * _ranNum2 & 0x7fff_ffff) % M;
        }
    }
}
