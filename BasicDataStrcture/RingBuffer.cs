using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BasicDataStrcture
{
    public class RingBuffer<T>
    {
        private T[] _data;
        private int _startIndex = 7;    //Pointing to the first element in the ring
        private int _endIndex = 7;      //Pointing to the postion right behine the last element in the ring
        private ManualResetEventSlim _eventHasVacancy = new ManualResetEventSlim(true);
        private ManualResetEventSlim _eventHasElement = new ManualResetEventSlim(false);
        private static object _lockObj = new object();
        private static object _enqueueLock = new object();
        private static object _dequeueLock = new object();


        public RingBuffer(int size)
        {
            _data = new T[size];
        }

        public int Count
        {
            get
            {
                if (!_eventHasVacancy.IsSet)
                    return _data.Length;
                if (_endIndex < _startIndex)
                    return _endIndex + _data.Length - _startIndex;
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
                lock (_enqueueLock)
                {
                    _data[_endIndex] = t;
                    _endIndex++;
                    if (_endIndex == _data.Length)
                        _endIndex = 0;
                }
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
                T value = default(T);
                lock (_dequeueLock)
                {
                    value = _data[_startIndex];
                    _startIndex++;
                    if (_startIndex == _data.Length)
                        _startIndex = 0;
                }
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
