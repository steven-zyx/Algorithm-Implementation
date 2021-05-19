using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class RandomBag<T> : IEnumerable
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

        public IEnumerator GetEnumerator()
        {
            Shuffle();
            for (int i = 0; i < _count; i++)
                yield return _data[i];
        }
    }
}

