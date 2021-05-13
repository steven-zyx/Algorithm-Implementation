using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Stack2<T>
    {
        private T[] _data;
        private int _count;

        public Stack2()
        {
            _data = new T[4];
            _count = 0;
        }

        public void Push(T t)
        {
            _data[_count] = t;
            _count++;
            if (_data.Length == _count)
                Resize(_count * 2);
        }

        public T Pop()
        {
            if (_count == 0)
                throw new Exception("Empty");
            else if (_count == _data.Length / 4)
                Resize(_count * 2);
            _count--;
            return _data[_count];
        }

        public int Length => _count;

        private void Resize(int newLength)
        {
            T[] newData = new T[newLength];
            Array.Copy(_data, newData, _count);
            _data = newData;
        }
    }
}
