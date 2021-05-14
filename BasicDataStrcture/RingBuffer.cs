using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BasicDataStrcture
{
    public class RingBuffer<T>
    {
        private T[] _data;
        private int _startIndex;    //Pointing to the first element to read
        private int _endIndex;      //Pointing to the last element to read
        private int _size;
        private ManualResetEventSlim _eventHasVacancy;
        private ManualResetEventSlim _eventHasElement;
        private static object _lockObj;
        private int _count = 0;

        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[_size];
            _startIndex = 9;
            _endIndex = 9;
            _eventHasElement = new ManualResetEventSlim(false);
            _eventHasVacancy = new ManualResetEventSlim(true);
            _lockObj = new object();
        }

        public void Enqueue(T t)
        {
            lock (_lockObj)
            {
                //Check if the collection is full, block the Enqueue request if so
                if (_count == _size)
                {
                    _eventHasVacancy.Reset();
                    _eventHasVacancy.Wait();
                }
                //Add the item and increment the pointer
                _data[_endIndex] = t;
                _endIndex++;
                if (_endIndex == _size)
                    _endIndex = 0;
                //To show the collection is not empty, allow the Dequeue request to proceed
                if (!_eventHasElement.IsSet)
                    _eventHasElement.Set();
                _count++;
            }
        }

        public T Dequeue()
        {
            lock (_lockObj)
            {
                //Check if the collection is empty, block the Dequeue reqeust if so
                if (_count == 0)
                {
                    _eventHasElement.Reset();
                    _eventHasElement.Wait();
                }
                //Remove an item and increment the pointer
                T value = _data[_startIndex];
                _startIndex++;
                if (_startIndex == _size)
                    _startIndex = 0;
                //To show there is vacancy, allow the Enqueue request to proceed
                if (!_eventHasVacancy.IsSet)
                    _eventHasVacancy.Set();
                _count--;
                return value;
            }
        }
    }
}
