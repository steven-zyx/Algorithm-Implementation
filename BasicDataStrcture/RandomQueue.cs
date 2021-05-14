﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class RandomQueue<T> : IEnumerable<T>
    {
        private T[] _data;
        private int _count;
        private Random ran;

        public RandomQueue()
        {
            _data = new T[4];
            _count = 0;
            ran = new Random(DateTime.Now.Second);
        }

        public bool IsEmpty => _count == 0;

        public void Enqueue(T t)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);
            _data[_count] = t;
            _count++;
        }

        public T Dequeue()
        {
            int randomIndex = ran.Next(0, _count);
            T value = _data[randomIndex];
            _data[randomIndex] = _data[_count - 1];
            _count--;
            if (_count < _data.Length / 4)
                Resize(_data.Length / 2);
            return value;
        }

        public T Sample()
        {
            int randomIndex = ran.Next(0, _count);
            return _data[randomIndex];
        }

        private void Resize(int newSize)
        {
            T[] newData = new T[newSize];
            Array.Copy(_data, newData, _count);
            _data = newData;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RandomBag<T>.RandomBagEnumerator(_data, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
