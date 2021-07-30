using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class BinarySearchMultiSET<K> : BinarySearchSET<K>, IMultiSet<K> where K : IComparable
    {
        public override void Put(K key)
        {
            int index = Rank(key);
            for (int i = _count; i > index; i--)
                _keys[i] = _keys[i - 1];
            _keys[index] = key;

            if (++_count >= _keys.Length)
                Resize(_keys.Length * 2);
        }

        public int Count(K key)
        {
            int first = Rank(key, true);
            if (KeyEquals(first, key))
                return Rank(key, false) - first + 1;
            else
                return 0;
        }

        public int DeleteAll(K key)
        {
            int i = Rank(key, true);
            if (KeyEquals(i, key))
            {
                int interval = Rank(key, false) - i + 1;
                for (; i < _count - interval; i++)
                    _keys[i] = _keys[i + interval];
                for (int j = _count - interval; j < _count; j++)
                    _keys[i] = default(K);
                _count -= interval;
                if (_count < _keys.Length / 4) Resize(_keys.Length / 2);
                return interval;
            }
            else
                return 0;
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

        protected bool KeyEquals(int a, int b) => _keys[a].CompareTo(_keys[b]) == 0;
    }
}
