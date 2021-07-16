using System;
using System.Collections;
using System.Collections.Generic;

namespace Searching
{
    //Basic Symbol table ope
    public partial class BinarySearchST<K, V> : IOrderedSymbolTable<K, V> where K : IComparable
    {
        protected K[] _keys;
        protected V[] _values;
        protected int _count;

        public BinarySearchST()
        {
            _keys = new K[16];
            _values = new V[16];
            _count = 0;
        }

        public virtual void Put(K key, V value)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
            {
                _values[index] = value;
            }
            else
            {
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
        }

        public V Get(K key)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
                return _values[index];
            else
                return default(V);
        }

        public virtual bool Delete(K key)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
            {
                for (int i = index; i <= _count - 2; i++)
                {
                    _keys[i] = _keys[i + 1];
                    _values[i] = _values[i + 1];
                }
                _keys[_count - 1] = default(K);
                _values[_count - 1] = default(V);

                _count--;
                if (_count < _keys.Length / 4)
                    Resize(_keys.Length / 2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(K key)
        {
            int index = Rank(key);
            return KeyEquals(index, key);
        }

        public bool IsEmpty => _count == 0;

        public int Size() => _count;

        public IEnumerable<K> Keys()
        {
            for (int i = 0; i < _count; i++)
                yield return _keys[i];
        }
    }


    //Ordered symbol table operation
    public partial class BinarySearchST<K, V>
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

        public virtual K Select(int index) => _keys[index];

        public virtual void DeleteMin()
        {
            _count--;
            for (int i = 0; i <= _count - 1; i++)
            {
                _keys[i] = _keys[i + 1];
                _values[i] = _values[i + 1];
            }
            _keys[_count] = default(K);
            _values[_count] = default(V);
        }

        public virtual void DeleteMax()
        {
            _count--;
            if (_count < _keys.Length / 4)
                Resize(_keys.Length / 2);
            else
            {
                _keys[_count] = default(K);
                _values[_count] = default(V);
            }
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
            for (int i = loRank; i < hiRank; i++)
                yield return _keys[i];

            if (KeyEquals(hiRank, hi))
                yield return _keys[hiRank];
        }
    }

    //Dedicated funtions
    public partial class BinarySearchST<K, V>
    {
        protected bool KeyEquals(int index, K key) =>
            index < _count && _keys[index].CompareTo(key) == 0;

        protected void Resize(int size)
        {
            K[] newKeys = new K[size];
            Array.Copy(_keys, newKeys, _count);
            _keys = newKeys;

            V[] newValues = new V[size];
            Array.Copy(_values, newValues, _count);
            _values = newValues;
        }
    }
}
