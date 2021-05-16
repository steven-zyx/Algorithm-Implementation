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
        private static readonly object _enqueueLock = new object();
        private static readonly object _dequeueLock = new object();

        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[size];
        }

        public int Count => _endIndex - _startIndex;


        public void Dnqueue2(T t)
        {

        }


        public void Enqueue(T t)
        {
            OutputDebug($"E: {t}");
            //Check if full
            //Block the Enqueue request if full
            while (_endIndex - _size == _startIndex)
            {
                OutputDebug("E: C");
                _eventHasVacancy.Reset();
                OutputDebug("E: R");
                _eventHasVacancy.Wait(100);
                OutputDebug("E: W");
            }
            _eventHasVacancy.Set();
            OutputDebug("E: S");
            //Increment the pointer and add the item
            lock (_enqueueLock)
            {
                _data[_endIndex % _size] = t;
                OutputDebug("E: WR");
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
            while (_endIndex == _startIndex)
            {
                OutputDebug("D: C");
                _eventHasElement.Reset();
                OutputDebug("D: R");
                _eventHasElement.Wait(100);
                OutputDebug("D: W");
            }
            _eventHasElement.Set();
            OutputDebug("D: S");
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
    public class RingBuffer2<T>
    {
        private T[] _data;
        private int _size;
        private int _startIndex = 0;    //Pointing to the position right behind the first element in the ring
        private int _endIndex = 0;      //Pointing to the last element in the ring
        private static readonly object _enqueueLock = new object();
        private static readonly object _dequeueLock = new object();
        private int _dequeueIndex = -1;
        private int _enqueueIndex = -1;
        private bool _finishWrite = false;

        public RingBuffer2(int size)
        {
            _size = size;
            _data = new T[size];
        }

        public int Count => _endIndex - _startIndex;

        public void Enqueue(T t)
        {
            lock (_enqueueLock)
            {
                while (_enqueueIndex + 1 - _size == _startIndex) { }
                _enqueueIndex++;
            }

            _data[_enqueueIndex % _size] = t;

            int initialValue = _endIndex;
            while (_endIndex < _enqueueIndex + 1)
            {
                if (Interlocked.CompareExchange(ref _endIndex, _enqueueIndex + 1, initialValue) == initialValue)
                    break;
            }
        }

        public T Dequeue(out bool isFinished)
        {
            lock (_dequeueLock)
            {
                while (_endIndex == _dequeueIndex + 1)
                {
                    if (_finishWrite)
                    {
                        isFinished = true;
                        return default(T);
                    }
                }
                _dequeueIndex++;
            }

            T value = default(T);
            value = _data[_dequeueIndex % _size];

            int initialValue = _startIndex;
            while (_startIndex < _dequeueIndex + 1)
            {
                if (Interlocked.CompareExchange(ref _startIndex, _dequeueIndex + 1, initialValue) == initialValue)
                    break;
            }
            isFinished = false;
            return value;
        }

        public T Dequeue()
        {
            bool isFinished;
            return Dequeue(out isFinished);
        }

        public void FinishWrite()
        {
            _finishWrite = true;
        }
    }
}
