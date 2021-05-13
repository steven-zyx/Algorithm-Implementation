using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Queue2<T>
    {
        private T[] _data;
        private int _startIndex;
        private int _endIndex;

        public Queue2()
        {
            _data = new T[4];
            _startIndex = 0;
            _endIndex = 0;
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
    }
}
