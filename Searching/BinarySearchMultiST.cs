using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class BinarySearchMultiST<K, V> : BinarySearchST<K, V>, IMultiSymbolTable<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            int index = Rank(key);
            for (int i = _count; i > index; i--)
            {
                _keys[i] = _keys[i - 1];
                _values[i] = _values[i - 1];
            }
            _keys[index] = key;
            _values[index] = value;

            _count++;
            if (_count >= _keys.Length)
                Resize(_keys.Length * 2);
        }

        public int DeleteAll(K key)
        {
            int i = Rank(key, true);
            if (KeyEquals(i, key))
            {
                int interval = Rank(key, false) - i + 1;
                for (; i < _count - interval; i++)
                {
                    _keys[i] = _keys[i + interval];
                    _values[i] = _values[i + interval];
                }
                for (int j = _count - interval; j < _count; j++)
                {
                    _keys[i] = default(K);
                    _values[i] = default(V);
                }
                _count -= interval;
                if (_count < _keys.Length / 4) Resize(_keys.Length / 2);
                return interval;
            }
            else
                return 0;
        }

        public IEnumerable<V> GetAll(K key)
        {
            int first = Rank(key, true);
            if (KeyEquals(first, key))
            {
                int last = Rank(key, false);
                for (; first <= last; first++)
                    yield return _values[first];
            }
        }

        public override int Rank(K key) => Rank(key, false);

        protected int Rank(K key, bool firstOccurance)
        {
            int lo = 0, hi = _count - 1;
            while (hi >= lo)
            {
                int mid = lo + (hi - lo) / 2;
                int dif = key.CompareTo(_keys[mid]);
                if (dif == 0)
                {
                    if (firstOccurance)
                        while (mid > 0 && KeyEquals(mid, mid - 1)) mid--;
                    else
                        while (mid + 1 < _count && KeyEquals(mid, mid + 1)) mid++;
                    return mid;
                }
                else if (dif < 0) hi = mid - 1;
                else lo = mid + 1;
            }
            return lo;
        }

        public override void Certificate()
        {
            K current = _keys[0];
            for (int i = 1; i < _count; i++)
            {
                K key = _keys[i];
                if (current.CompareTo(key) >= 0 ||
                    key.CompareTo(Select(Rank(key))) != 0)
                {
                    throw new Exception("Inconsistant");
                }
                current = key;
            }
        }

        protected bool KeyEquals(int a, int b) => _keys[a].CompareTo(_keys[b]) == 0;
    }
}
