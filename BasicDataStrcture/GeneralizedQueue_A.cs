using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class GeneralizedQueue_A<T>
    {
        private T[] _data;
        private int _count;

        public GeneralizedQueue_A()
        {
            _data = new T[4];
            _count = 0;
        }

        public bool IsEmpty => _count == 0;

        public void Insert(T t)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);
            _data[_count] = t;
            _count++;
        }

        public T Delete(int k)
        {
            if (k > _count)
                throw new Exception("Invalid operation");
            T value = _data[k - 1];
            if (_count < _data.Length / 4)
            {
                MoveWithinResize(_data.Length / 2, k - 1);
            }
            else
            {
                for (int i = k - 1; i < _count; i++)
                {
                    _data[i] = _data[i + 1];
                }
                _count--;
            }
            return value;
        }

        private void Resize(int newSize)
        {
            T[] newData = new T[newSize];
            Array.Copy(_data, newData, _count);
            _data = newData;
        }

        private void MoveWithinResize(int newSize, int vacancy)
        {
            T[] newData = new T[newSize];
            Array.Copy(_data, newData, vacancy);
            Array.Copy(_data, vacancy + 1, newData, vacancy, _count - vacancy - 1);
            _data = newData;
            _count--;
        }
    }
}
