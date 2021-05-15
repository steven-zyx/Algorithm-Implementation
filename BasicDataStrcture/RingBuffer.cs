using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

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
            OutputDebug($"E: {t}");
            //Check if full
            //Block the Enqueue request if full
            if (_endIndex - _size == _startIndex)
            {
                OutputDebug("E: C");
                _eventHasVacancy.Reset();
                OutputDebug("E: R");
                _eventHasVacancy.Wait();
                OutputDebug("E: W");
                if (_endIndex - _size == _startIndex)
                {
                    OutputDebug("E: C2");
                    _eventHasVacancy.Reset();
                    OutputDebug("E: R2");
                    _eventHasVacancy.Wait();
                    OutputDebug("E: W2");
                }
            }
            //Increment the pointer and add the item
            lock (_enqueueLock)
            {
                _data[_endIndex % _size] = t;
                OutputDebug("E: S");
                _endIndex++;
                OutputDebug("E: I");
            }
            //To show the collection is not empty, allow the Dequeue request to proceed
            if (!_eventHasElement.IsSet)
                _eventHasElement.Set();
            OutputDebug("E: RL");
        }

        public T Dequeue()
        {
            OutputDebug("D:   ");
            //Check if empty
            //Block the Dequeue request if empty
            if (_endIndex == _startIndex)
            {
                OutputDebug("D: C");
                _eventHasElement.Reset();
                OutputDebug("D: R");
                _eventHasElement.Wait();
                OutputDebug("D: W");
                if (_endIndex == _startIndex)
                {
                    OutputDebug("D: C2");
                    _eventHasElement.Reset();
                    OutputDebug("D: R2");
                    _eventHasElement.Wait();
                    OutputDebug("D: W2");
                }
            }
            //Remove an item and increment the pointer
            T value = default(T);
            lock (_dequeueLock)
            {
                value = _data[_startIndex % _size];
                OutputDebug($"D: {value}");
                _startIndex++;
                OutputDebug("D: I");
            }
            // To show there is vacancy, allow the Enqueue request to proceed
            if (!_eventHasVacancy.IsSet)
                _eventHasVacancy.Set();
            OutputDebug("D: RL");
            return value;
        }

        private void OutputDebug(string message)
        {
            //Trace.WriteLine($"{message}\t_s:{_startIndex}\t_e:{_endIndex}\t_eV:{_eventHasVacancy.IsSet}\t_eE:{_eventHasElement.IsSet}");
            //Trace.WriteLine(string.Join(" ", _data));
        }
    }
}
