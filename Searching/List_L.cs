using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Utils;

namespace Searching
{
    public class List_L<V>
    {
        private IOrderedSymbolTable<KeyIntArray, V> _ost;
        private ISymbolTable<V, IOrderedSET<KeyIntArray>> _st;
        private readonly KeyIntArray _minValue;
        private readonly KeyIntArray _maxValue;

        public List_L()
        {
            _ost = new BST_23<KeyIntArray, V>();
            _st = new SeperateChainingHashST<V, IOrderedSET<KeyIntArray>>();
            _minValue = new KeyIntArray(int.MinValue);
            _maxValue = new KeyIntArray(int.MaxValue);
        }

        public void AddFront(V value)
        {
            KeyIntArray key;
            if (_ost.IsEmpty)
                key = new KeyIntArray(0);
            else
                key = KeyIntArray.GenerateMidean(_minValue, _ost.Min());

            _ost.Put(key, value);
            PutToST(value, key);
        }

        public void AddBack(V value)
        {
            KeyIntArray key;
            if (_ost.IsEmpty)
                key = new KeyIntArray(0);
            else
                key = KeyIntArray.GenerateMidean(_ost.Max(), _maxValue);

            _ost.Put(key, value);
            PutToST(value, key);
        }

        public void DeleteFront()
        {
            V value = _ost.Get(_ost.Min());
            _ost.DeleteMin();
            _st.Get(value).DeleteMin();
        }

        public void DeleteBack()
        {
            V value = _ost.Get(_ost.Max());
            _ost.DeleteMax();
            _st.Get(value).DeleteMax();
        }

        public void Delete(V value)
        {
            foreach (KeyIntArray key in _st.Get(value).Keys())
                _ost.Delete(key);
            _st.Delete(value);
        }

        public void Add(int i, V value)
        {
            if (i > Size())
                throw new InvalidOperationException($"Request postion({i}) is beyond max postion({Size() - 1}).");

            KeyIntArray previousKey, nextKey;
            if (i == 0)
                previousKey = _minValue;
            else
                previousKey = _ost.Select(i - 1);
            if (i == Size())
                nextKey = _maxValue;
            else
                nextKey = _ost.Select(i);

            KeyIntArray key = KeyIntArray.GenerateMidean(previousKey, nextKey);
            _ost.Put(key, value);
            PutToST(value, key);
        }

        public bool Contains(V value) => _st.Contains(value);

        public int Size() => _ost.Size();

        public IEnumerable<V> Keys()
        {
            foreach (KeyIntArray key in _ost.Keys())
                yield return _ost.Get(key);
        }

        public bool IsEmpty => _ost.IsEmpty;

        private void PutToST(V value, KeyIntArray key)
        {
            if (_st.Get(value) == null)
            {
                IOrderedSET<KeyIntArray> set = new BinarySearchSET<KeyIntArray>();
                set.Put(key);
                _st.Put(value, set);
            }
            else
                _st.Get(value).Put(key);
        }
    }

    public class KeyIntArray : IComparable
    {
        private int[] _data;

        public KeyIntArray(int value) : this(new int[] { value }) { }

        public KeyIntArray(int[] data)
        {
            _data = data;
        }

        public int CompareTo(object obj)
        {
            KeyIntArray other = obj as KeyIntArray;

            int a, b, aLength = _data.Length, bLength = other._data.Length, maxLength = Math.Max(aLength, bLength);
            for (int i = 0; i < maxLength; i++)
            {
                if (i < aLength)
                    a = _data[i];
                else
                    a = int.MinValue;

                if (i < bLength)
                    b = other._data[i];
                else
                    b = int.MinValue;

                int result = a.CompareTo(b);
                if (result != 0) return result;
            }
            return 0;
        }

        public static KeyIntArray GenerateMidean(KeyIntArray min, KeyIntArray max)
        {
            List<int> result = new List<int>();
            int a, b, minLength = min._data.Length, maxLength = max._data.Length;
            for (int i = 0; ; i++)
            {
                if (i < minLength)
                    a = min._data[i];
                else
                    a = int.MinValue;

                if (i < maxLength)
                    b = max._data[i];
                else
                    b = int.MaxValue;

                int median = Util.AVG(a, b);
                result.Add(median);
                if (median != a && median != b)
                    break;
            }
            return new KeyIntArray(result.ToArray());
        }

        public override string ToString() => string.Join(',', _data);
    }
}
