using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicDataStrcture
{
    public class Queue_A<T> : IQueue<T>, IEnumerable
    {
        private T[] _data;
        private int _startIndex = 0;
        private int _endIndex = 0;

        public Queue_A()
        {
            _data = new T[4];
        }

        public Queue_A(IQueue<T> source)
        {
            _data = new T[source.Length];
            foreach (T item in (IEnumerable)source)
            {
                _data[_endIndex] = item;
                _endIndex++;
            }
        }

        public void Enqueue(T t)
        {
            _data[_endIndex] = t;
            _endIndex++;

            if (_endIndex == _data.Length)
                Resize(_data.Length * 2);
        }

        public T Dequeue()
        {
            if (_startIndex >= _endIndex)
                throw new Exception("No more element");
            else if (Length == _data.Length / 4)
                Resize(_data.Length / 2);
            T value = _data[_startIndex];
            _startIndex++;
            return value;
        }

        public int Length => _endIndex - _startIndex;

        private void Resize(int newLength)
        {
            T[] newData = new T[newLength];
            Array.Copy(_data, _startIndex, newData, 0, Length);
            _data = newData;
            _endIndex = Length;
            _startIndex = 0;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = _startIndex; i < _endIndex; i++)
                yield return _data[i];
        }
    }
}
