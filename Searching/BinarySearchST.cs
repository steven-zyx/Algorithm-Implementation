﻿using System;
using System.Collections;

namespace Searching
{
    public class BinarySearchST<K, V> where K : IComparable
    {
        private K[] _keys;
        private V[] _values;
        private int _count = 0;

        public BinarySearchST()
        {
            _keys = new K[16];
            _values = new V[16];
        }

        public V this[K key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public void Put(K key, V value)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
                _values[index] = value;
            else
            {
                for (int i = _count; i >= index; i++)
                {
                    _keys[i + 1] = _keys[i];
                    _values[i + 1] = _values[i];
                }
                _keys[index] = key;
                _values[index] = value;
            }

            _count++;
            if (_count >= _keys.Length)
                Resize(_keys.Length * 2);
        }

        public V Get(K key)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
                return _values[index];
            else
                return default(V);
        }

        public bool Delete(K key)
        {
            int index = Rank(key);
            if (KeyEquals(index, key))
            {
                for (int i = index + 1; i <= _count - 2; i++)
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
                return false;
        }

        public bool Contains(K key) => Get(key) != null;

        public bool IsEmpty() => _count == 0;

        public int Size() => _count;

        public V Min => _values[0];

        public V Max => _values[_count - 1];

        public V Floor(K key)
        {
            int index = Rank(key);
            if (index == 0)
                return default(V);
            else
                return _values[index];
        }

        public V Ceiling(K key) => _values[Rank(key)];
        public int Rank(K key)
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
            _count--;
            for (int i = 0; i <= _count - 1; i++)
            {
                _keys[i] = _keys[i + 1];
                _values[i] = _values[i + 1];
            }
            _keys[_count] = default(K);
            _values[_count] = default(V);
        }

        public void DeleteMax()
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

        public IEnumerable Keys(K lo, K hi)
        {
            int loRank = Rank(lo);
            int hiRank = Rank(hi);
            for (int i = loRank; i < hiRank; i++)
                yield return _keys[i];

            if (KeyEquals(hiRank, hi))
                yield return _keys[hiRank];
        }

        public IEnumerable Keys() => _keys;

        private bool KeyEquals(int index, K key) =>
            index < _count && _keys[index].CompareTo(key) == 0;

        private void Resize(int size)
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
