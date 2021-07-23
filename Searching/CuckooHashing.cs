using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using System.Diagnostics;

namespace Searching
{
    public class CuckooHashing<K, V> : HashST<K, V> where K : struct where V : struct
    {
        private int _count;
        private List<K?[]> _keys;
        private List<V?[]> _values;
        private int[] _ranNumbers;
        protected readonly int[] _primes = {
            3, 7, 13, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191, 16381, 32749, 65521, 131071, 262139, 524287, 1048573, 2097143, 4194301,
            8388593, 16777213, 33554393, 67108859, 134217689, 268435399, 536870909, 1073741789, 2147483647 };

        public CuckooHashing(int size) : base(size)
        {
            Init();
        }

        private void Init()
        {
            _count = 0;
            _ranNumbers = new int[] {
                Util.Ran.Next(0, int.MaxValue),
                Util.Ran.Next(0, int.MaxValue)
            };
            _keys = new List<K?[]>
            {
                new K?[M],
                new K?[M],
            };
            _values = new List<V?[]>
            {
                new V?[M],
                new V?[M]
            };
        }

        public CuckooHashing() : this(31) { }

        public override V Get(K key)
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Hash(key, i);
                if (key.Equals(_keys[i][index]))
                    return _values[i][index].Value;
            }
            return default(V);
        }

        public override bool Contains(K key)
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Hash(key, i);
                if (key.Equals(_keys[i][index]))
                    return true;
            }
            return false;
        }

        public override bool Delete(K key)
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Hash(key, i);
                if (key.Equals(_keys[i][index]))
                {
                    _keys[i][index] = null;
                    _values[i][index] = null;
                    if (--_count < M / 8)
                        Resize(NextSize(false));
                    return true;
                }
            }
            return false;
        }

        public override void Put(K key, V value)
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Hash(key, i);
                if (!_keys[i][index].HasValue)
                {
                    _keys[i][index] = key;
                    _values[i][index] = value;
                    if (++_count > M / 2) Resize(NextSize(true));
                    return;
                }
                else if (_keys[i][index].Equals(key))
                {
                    _values[i][index] = value;
                    return;
                }
            }
            for (int j = 0; j < M; j++)
            {
                int tId = j % 2;
                int index = Hash(key, tId);
                if (_keys[tId][index].HasValue)
                {
                    K oldKey = _keys[tId][index].Value;
                    _keys[tId][index] = key;
                    key = oldKey;

                    V oldValue = _values[tId][index].Value;
                    _values[tId][index] = value;
                    value = oldValue;
                }
                else
                {
                    _keys[tId][index] = key;
                    _values[tId][index] = value;
                    if (++_count > M / 2) Resize(NextSize(true));
                    return;
                }
            }
            Resize(M);
            Put(key, value);
        }

        public override bool IsEmpty => _count == 0;

        public override int Size() => _count;

        public override IEnumerable<K> Keys()
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < M; j++)
                    if (_keys[i][j].HasValue)
                        yield return _keys[i][j].Value;
        }

        private void Resize(int size)
        {
            K[] tempKeys = new K[_count];
            V[] tempValues = new V[_count];
            int index = 0;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < M; j++)
                    if (_keys[i][j].HasValue)
                    {
                        tempKeys[index] = _keys[i][j].Value;
                        tempValues[index] = _values[i][j].Value;
                        index++;
                    }

            M = size;
            Init();

            for (int n = 0; n < tempKeys.Length; n++)
                Put(tempKeys[n], tempValues[n]);
        }

        private int NextSize(bool doIncrease)
        {
            for (int i = 0; i < _primes.Length; i++)
            {
                if (_primes[i] == M)
                {
                    if (doIncrease)
                        return _primes[i + 1];
                    else
                        return _primes[i - 1];
                }
            }
            return -1;
        }

        private int Hash(K key, int i) => ((key.GetHashCode() ^ _ranNumbers[i]) & 0x7fff_ffff) % M;
    }
}
