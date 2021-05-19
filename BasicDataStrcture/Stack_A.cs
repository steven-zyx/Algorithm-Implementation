using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Stack_A<T> : IStack<T>, IEnumerable
    {
        private T[] _data;
        private int _count;

        public Stack_A()
        {
            _data = new T[4];
            _count = 0;
        }

        public Stack_A(IStack<T> source) : this()
        {
            _count = source.Length;
            _data = new T[_count];
            for (int i = _count - 1; i >= 0; i--)
                _data[i] = source.Pop();
            foreach (T item in _data)
                source.Push(item);
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

        public IEnumerator GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
                yield return _data[i];
        }

        public static bool IsGenerable(int?[] input)
        {
            int index = 0;
            foreach (int? value in input)
            {
                if (value.HasValue)
                    index++;
                else
                    index--;
                if (index < 0)
                    return false;
            }
            return true;
        }
    }
}
