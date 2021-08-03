using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class RandomQueue<T> : IQueue<T>, IEnumerable<T>
    {
        private T[] _data;
        private int _count;
        private Random _ran;

        public RandomQueue()
        {
            _data = new T[4];
            _count = 0;
            _ran = new Random(DateTime.Now.Millisecond);
        }


        public void Enqueue(T t)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);
            _data[_count] = t;
            _count++;
        }

        public T Dequeue()
        {
            int randomIndex = _ran.Next(0, _count);
            T value = _data[randomIndex];
            _data[randomIndex] = _data[_count - 1];
            _count--;
            if (_count < _data.Length / 4)
                Resize(_data.Length / 2);
            return value;
        }

        public T Sample()
        {
            int randomIndex = _ran.Next(0, _count);
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
            T[] output = new T[_count];
            Array.Copy(_data, output, _count);
            for (int i = 0; i < _count; i++)
            {
                T temp = output[i];
                int randomIndex = _ran.Next(0, _count);
                output[i] = output[randomIndex];
                output[randomIndex] = temp;
            }

            for (int i = 0; i < _count; i++)
                yield return output[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsEmpty => _count == 0;

        public int Length => _count;
    }
}
