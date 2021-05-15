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

        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[_size];
            _startIndex = 7;
            _endIndex = 7;
            _eventHasElement = new ManualResetEventSlim(false);
            _eventHasVacancy = new ManualResetEventSlim(true);
            _lockObj = new object();
        }

        public int Count
        {
            get
            {
                if (_endIndex < _startIndex)
                    return _endIndex + 12 - _startIndex;
                else
                    return _endIndex - _startIndex; 
            }
        }

        public void Enqueue(T t)
        {
            lock (_lockObj)
            {
                //Block the Enqueue request if full
                _eventHasVacancy.Wait();
                //Increment the pointer and add the item
                _data[_endIndex] = t;
                _endIndex++;
                if (_endIndex == _size)
                    _endIndex = 0;
                //check if full
                if (_endIndex == _startIndex)
                    _eventHasVacancy.Reset();
                //To show the collection is not empty, allow the Dequeue request to proceed
                if (!_eventHasElement.IsSet)
                    _eventHasElement.Set();
            }
        }

        public T Dequeue()
        {
            lock (_lockObj)
            {
                //Block the Dequeue request if empty
                _eventHasElement.Wait();
                //Remove an item and increment the pointer
                T value = _data[_startIndex];
                _startIndex++;
                if (_startIndex == _size)
                    _startIndex = 0;
                //check if empty
                if (_endIndex == _startIndex)
                    _eventHasElement.Reset();
                // To show there is vacancy, allow the Enqueue request to proceed
                if (!_eventHasVacancy.IsSet)
                    _eventHasVacancy.Set();
                return value;
            }
        }
    }
}
