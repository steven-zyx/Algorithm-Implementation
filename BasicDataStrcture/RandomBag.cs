using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class RandomBag<T> : IEnumerable<T>
    {
        private T[] _data;
        private int _count;

        public RandomBag()
        {
            _count = 0;
            _data = new T[4];
        }

        public int Size => _count;

        public bool IsEmpty => _count == 0;

        public void Add(T t)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);
            _data[_count] = t;
            _count++;
        }




        private void Resize(int newSize)
        {
            T[] newData = new T[newSize];
            Array.Copy(_data, newData, _data.Length);
            _data = newData;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RandomBagEnumerator(_data, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class RandomBagEnumerator : IEnumerator<T>
        {
            private T[] _data;
            private int _count;
            private int _index;

            public RandomBagEnumerator(T[] data, int count)
            {
                _data = data;
                _count = count;
                Shuffle();
                _index = -1;
            }

            public T Current => _data[_index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _data = null;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _count;
            }

            public void Reset()
            {
                _index = -1;
            }

            private void Shuffle()
            {
                Random r = new Random(DateTime.Now.Second);
                for (int i = 0; i < _count; i++)
                {
                    T temp = _data[i];
                    int randomIndex = r.Next(0, _count);
                    _data[i] = _data[randomIndex];
                    _data[randomIndex] = temp;
                }
            }
        }
    }
}
