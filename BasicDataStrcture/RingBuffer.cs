using System.Threading;

namespace BasicDataStrcture
{
    public class RingBuffer2<T>
    {
        private T[] _data;
        private int[] _state;
        private int _size;
        private int _getRequestNo = -1;
        private int _setRequestNo = -1;
        private bool _finishWrite = false;

        public RingBuffer2(int size)
        {
            _size = size;
            _data = new T[size];
            _state = new int[size];
        }

        public void Enqueue(T t)
        {
        Start:
            int localIndex = Interlocked.Increment(ref _setRequestNo) % _size;
            while (_state[localIndex] != 0) { }

            if (Interlocked.CompareExchange(ref _state[localIndex], 1, 0) != 0)
                goto Start;

            _data[localIndex] = t;
            _state[localIndex]++;
        }

        public bool Dequeue(out T value)
        {
        Start:
            int localIndex = Interlocked.Increment(ref _getRequestNo) % _size;
            while (_state[localIndex] != 2)
            {
                if (_finishWrite)
                {
                    if (localIndex == 0)
                    {
                        value = default(T);
                        return false;
                    }
                    else
                        break;
                }
            }

            if (Interlocked.CompareExchange(ref _state[localIndex], -1, 2) != 2)
                goto Start;
            value = _data[localIndex];
            _state[localIndex]++;

            return true;
        }

        public void FinishWrite()
        {
            _finishWrite = true;
        }


        //public StringBuilder _log = new StringBuilder();
        //private static readonly object _logLock = new object();
        //private void Log(string message, [CallerMemberName] string method = "")
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(method[0]);
        //    sb.Append(" ");
        //    sb.Append(Thread.CurrentThread.ManagedThreadId);
        //    for (int i = sb.Length; i < 5; i++)
        //    {
        //        sb.Append(" ");
        //    }
        //    sb.Append(message);
        //    for (int i = sb.Length; i < 15; i++)
        //    {
        //        sb.Append(" ");
        //    }
        //    sb.Append($" s:{_virtualStartIndex} e:{_virtualEndIndex}");
        //    for (int i = sb.Length; i < 35; i++)
        //    {
        //        sb.Append(" ");
        //    }
        //    for (int i = 0; i < _data.Length; i++)
        //    {
        //        sb.Append(_data[i]);
        //        sb.Append(" ");
        //        sb.Append(_state[i]);
        //        sb.Append(",");
        //    }
        //    sb.Append("\r\n");
        //    lock (_logLock)
        //    {
        //        _log.Append(sb);
        //    }
        //}
    }
}
