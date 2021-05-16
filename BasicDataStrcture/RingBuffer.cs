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
        private int _virtualStartIndex = 0;
        private int _endIndex = 0;      //Pointing to the postion right behine the last element in the ring
        private static readonly object _enqueueLock = new object();
        private static readonly object _dequeueLock = new object();
        private bool _finishWrite = false;

        public StringBuilder _log = new StringBuilder();
        private static readonly object _logLock = new object();

        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[size];
        }

        public int Count => _endIndex - _startIndex;

        public void Enqueue(T t)
        {
            Log___E("Start");
            while (_endIndex - _size == _startIndex) { }
            Log___E("Passed");
            _data[_endIndex % _size] = t;
            Log___E($"Set {t}");
            _endIndex++;
            Log___E($"Increment");
        }

        public T Dequeue(out bool isFinished)
        {
            Log___D("Start");
            int localIndex;
            lock (_dequeueLock)
            {
                Log___D("Lock");
                while (_endIndex == _startIndex || _virtualStartIndex == _endIndex)
                {
                    if (_finishWrite)
                    {
                        isFinished = true;
                        return default(T);
                    }
                }
                localIndex = _virtualStartIndex;
                _virtualStartIndex++;
                Log___D($"Set local {localIndex}:");
            }

            T value = _data[localIndex % _size];
            Log___D($"Get {value}");

            //int initialValue;
            //while (_startIndex < localIndex + 1)
            //{
            //    initialValue = _startIndex;
            //    if (Interlocked.CompareExchange(ref _startIndex, localIndex + 1, initialValue) == initialValue)
            //        break;
            //}
            //Log___D($"CAS");

            while (_startIndex < localIndex) { }
            _startIndex++;

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

        private void Log___D(string message)
        {
            //Log("Dequeue_" + message);
        }

        private void Log___E(string message)
        {
            //Log("Eequeue_" + message);
        }


        private void Log(string message)
        {
            StringBuilder sb = new StringBuilder(message);
            for (int i = message.Length; i < 20; i++)
            {
                sb.Append(" ");
            }
            sb.Append($"\t_s:{_startIndex}\t_vS:{_virtualStartIndex}\t_e:{_endIndex}\r\n");
            sb.Append(string.Join(" ", _data) + "\r\n");

            lock (_logLock)
            {
                _log.Append(sb);
            }
        }
    }

    public class RingBuffer2<T>
    {
        private T[] _data;
        private int _size;
        private int _startIndex = 0;    //Pointing to the first element in the ring
        private int _virtualStartIndex = 0;
        private int _endIndex = 0;      //Pointing to the postion right behine the last element in the ring
        private static readonly object _enqueueLock = new object();
        private static readonly object _dequeueLock = new object();
        private bool _finishWrite = false;

        public StringBuilder _log = new StringBuilder();
        private static readonly object _logLock = new object();

        public RingBuffer2(int size)
        {
            _size = size;
            _data = new T[size];
        }

        public int Count => _endIndex - _startIndex;

        public void Enqueue(T t)
        {
            while (_endIndex - _size == _startIndex) { }
            _data[_endIndex % _size] = t;
            _endIndex++;
        }

        public T Dequeue(out bool isFinished)
        {
            int localIndex;
            lock (_dequeueLock)
            {
                while (_endIndex == _startIndex || _virtualStartIndex == _endIndex)
                {
                    if (_finishWrite)
                    {
                        isFinished = true;
                        return default(T);
                    }
                }
                localIndex = _virtualStartIndex;
                _virtualStartIndex++;
            }

            T value = _data[localIndex % _size];

            //int initialValue;
            //while (_startIndex < localIndex + 1)
            //{
            //    initialValue = _startIndex;
            //    if (Interlocked.CompareExchange(ref _startIndex, localIndex + 1, initialValue) == initialValue)
            //        break;
            //}

            while (_startIndex < localIndex) { }
            _startIndex++;

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
