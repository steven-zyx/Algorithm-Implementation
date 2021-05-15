using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BasicDataStrcture
{
    public class RingBuffer<T>
    {
        private T[] _data;
        private int _size;
        private int _startIndex = 0;    //Pointing to the first element in the ring
        private int _endIndex = 0;      //Pointing to the postion right behine the last element in the ring
        private ManualResetEventSlim _eventHasVacancy = new ManualResetEventSlim(true);
        private ManualResetEventSlim _eventHasElement = new ManualResetEventSlim(false);
        private static readonly object _lockObj = new object();
        private static readonly object _enqueueLock = new object();
        private static readonly object _dequeueLock = new object();


        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[size];
        }

        public int Count => _endIndex - _startIndex;

        public void Enqueue(T t)
        {
            //Block the Enqueue request if full
            _eventHasVacancy.Wait();
            //Increment the pointer and add the item
            lock (_enqueueLock)
            {
                _data[_endIndex % _size] = t;
                _endIndex++;
            }
            //Check if full
            if (_endIndex - _size == _startIndex)
            {
                _eventHasVacancy.Reset();
            }
            //To show the collection is not empty, allow the Dequeue request to proceed
            if (!_eventHasElement.IsSet)
                _eventHasElement.Set();
        }

        public T Dequeue()
        {
            //Block the Dequeue request if empty
            _eventHasElement.Wait();
            //Remove an item and increment the pointer
            T value = default(T);
            lock (_dequeueLock)
            {
                value = _data[_startIndex % _size];
                _startIndex++;
            }
            //Check if empty
            if (_endIndex == _startIndex)
            {
                _eventHasElement.Reset();
            }
            // To show there is vacancy, allow the Enqueue request to proceed
            if (!_eventHasVacancy.IsSet)
                _eventHasVacancy.Set();
            return value;
        }
    }
}
