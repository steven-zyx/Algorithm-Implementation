using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    //Basic SET operation
    public partial class BinarySearchSET<K> : IOrderedSET<K> where K : IComparable
    {
        protected K[] _keys;
        protected int _count;

        public BinarySearchSET()
        {
            _keys = new K[16];
            _count = 0;
        }

        public virtual void Put(K key)
        {
            int index = Rank(key);
            if (!KeyEquals(index, key))
            {
                for (int i = _count; i > index; i--)
                    _keys[i] = _keys[i - 1];
                _keys[index] = key;

                if (++_count >= _keys.Length)
                    Resize(_keys.Length * 2);
            }
        }

        public bool Delete(K key)
        {
            int i = Rank(key);
            if (KeyEquals(i, key))
            {
                for (; i < _count - 1; i++)
                    _keys[i] = _keys[i + 1];
                _keys[_count - 1] = default(K);

                if (--_count < _keys.Length / 4) Resize(_keys.Length / 2);
                return true;
            }
            else
                return false;
        }

        public bool Contains(K key)
        {
            int index = Rank(key);
            return KeyEquals(index, key);
        }

        public bool IsEmpty => _count == 0;

        public int Size() => _count;

        public IEnumerable<K> Keys() => _keys;
    }

    //Ordered SET Operation
    public partial class BinarySearchSET<K>
    {
        public K Min() => _keys[0];

        public K Max() => _keys[_count - 1];

        public K Floor(K key)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
                return _keys[index];
            else if (index == 0)
                return default(K);
            else
                return _keys[index - 1];
        }

        public K Ceiling(K key) => _keys[Rank(key)];

        public virtual int Rank(K key)
        {
            int lo = 0, hi = _count - 1;
            while (hi >= lo)
            {
                int mid = lo + (hi - lo) / 2;
                int dif = key.CompareTo(_keys[mid]);
                if (dif == 0) return mid;
                else if (dif < 0) hi = mid - 1;
                else lo = mid + 1;
            }
            return lo;
        }

        public K Select(int index) => _keys[index];

        public void DeleteMin()
        {
            for (int i = 1; i < _count; i++)
                _keys[i - 1] = _keys[i];
            _keys[--_count] = default(K);
            if (_count < _keys.Length / 4)
                Resize(_keys.Length / 2);
        }

        public void DeleteMax()
        {
            _keys[--_count] = default(K);
            if (_count < _keys.Length / 4)
                Resize(_keys.Length / 2);
        }

        public int Size(K lo, K hi)
        {
            int loRank = Rank(lo);
            int hiRank = Rank(hi);
            if (KeyEquals(hiRank, hi))
                hiRank++;

            return hiRank - loRank;
        }

        public IEnumerable<K> Keys(K lo, K hi)
        {
            int loRank = Rank(lo);
            int hiRank = Rank(hi);
            for (; loRank < hiRank; loRank++)
                yield return _keys[loRank];

            if (KeyEquals(hiRank, hi))
                yield return _keys[hiRank];
        }
    }

    //Dedicated function
    public partial class BinarySearchSET<K>
    {
        protected bool KeyEquals(int index, K key) =>
            index < _count && _keys[index].CompareTo(key) == 0;

        protected void Resize(int size)
        {
            K[] newKeys = new K[size];
            Array.Copy(_keys, newKeys, _count);
            _keys = newKeys;
        }
    }
}
