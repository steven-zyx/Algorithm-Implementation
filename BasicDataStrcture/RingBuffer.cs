using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BasicDataStrcture
{
    public class RingBuffer<T>
    {
        private T[] _data;
        private bool[] _state;
        private int _size;
        private int _virtualStartIndex;
        private int _virtualEndIndex;
        private bool _finishWrite = false;

        public StringBuilder _log = new StringBuilder();
        private static readonly object _logLock = new object();



        public RingBuffer(int size)
        {
            _size = size;
            _data = new T[size];
            _state = new bool[size];

            _virtualStartIndex = -1;
            _virtualEndIndex = -1;
        }

        public void Enqueue(T t)
        {
            Log($"Start:{t}");
            int localIndex = Interlocked.Increment(ref _virtualEndIndex) % _size;
            Log($"LIndex:{localIndex}");
            while (_state[localIndex] == true) { }
            Log($"Pass");
            _data[localIndex] = t;
            Log($"Set:{t}");
            _state[localIndex] = true;
            Log("tag true");
        }

        public T Dequeue(out bool isFinished)
        {
            Log($"Start");
            int localIndex = Interlocked.Increment(ref _virtualStartIndex) % _size;
            Log($"LIndex:{localIndex}");
            while (_state[localIndex] == false)
            {
                if (_finishWrite)
                {
                    isFinished = true;
                    return default(T);
                }
            }
            Log($"Pass");

            T value = _data[localIndex];
            Log($"Get:{value}");
            _state[localIndex] = false;
            Log("tag false");

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

        private void Log(string message, [CallerMemberName] string method = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method[0]);
            sb.Append(" ");
            sb.Append(Thread.CurrentThread.ManagedThreadId);
            for (int i = sb.Length; i < 5; i++)
            {
                sb.Append(" ");
            }
            sb.Append(message);
            for (int i = sb.Length; i < 15; i++)
            {
                sb.Append(" ");
            }
            sb.Append($" s:{_virtualStartIndex} e:{_virtualEndIndex}");
            for (int i = sb.Length; i < 35; i++)
            {
                sb.Append(" ");
            }
            for (int i = 0; i < _data.Length; i++)
            {
                sb.Append(_data[i]);
                sb.Append(" ");
                sb.Append(_state[i] ? "1" : "0");
                sb.Append(",");
            }
            sb.Append("\r\n");
            lock (_logLock)
            {
                _log.Append(sb);
            }
        }
    }
    public class RingBuffer2<T>
    {
        private T[] _data;
        private bool[] _state;
        private int _size;
        private int _virtualStartIndex;
        private int _virtualEndIndex;
        private bool _finishWrite = false;

        public RingBuffer2(int size)
        {
            _size = size;
            _data = new T[size];
            _state = new bool[size];

            _virtualStartIndex = -1;
            _virtualEndIndex = -1;
        }

        public void Enqueue(T t)
        {
            int localIndex = Interlocked.Increment(ref _virtualEndIndex) % _size;
            while (_state[localIndex] == true) { }
            _data[localIndex] = t;
            _state[localIndex] = true;
        }

        public T Dequeue(out bool isFinished)
        {
            int localIndex = Interlocked.Increment(ref _virtualStartIndex) % _size;
            while (_state[localIndex] == false)
            {
                if (_finishWrite)
                {
                    isFinished = true;
                    return default(T);
                }
            }

            T value = _data[localIndex];
            _state[localIndex] = false;

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
