using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Deque2<T>
    {
        private T[] _data;
        private int _mostLeft;
        private int _mostRight;

        public Deque2()
        {
            _data = new T[4];
            _mostLeft = 2;
            _mostRight = 2;
        }

        public int Size => _mostRight - _mostLeft;

        public bool IsEmpty => Size == 0;

        public void PushLeft(T t)
        {
            if (_mostLeft == 0)
            {
                if (Size < _data.Length / 4)
                    Rearrange();
                else
                    Resize(_data.Length * 2);
            }
            _mostLeft--;
            _data[_mostLeft] = t;
        }

        public void PushRight(T t)
        {
            if (_mostRight == _data.Length)
            {
                if (Size < _data.Length / 4)
                    Rearrange();
                else
                    Resize(_data.Length * 2);
            }
            _data[_mostRight] = t;
            _mostRight++;
        }

        public T PopLeft()
        {
            if (IsEmpty)
                throw new Exception("No more element");
            else if (Size < _data.Length / 4)
                Resize(_data.Length / 2);
            T value = _data[_mostLeft];
            _mostLeft++;
            return value;
        }

        public T PopRight()
        {
            if (IsEmpty)
                throw new Exception("No more element");
            else if (Size < _data.Length / 4)
                Resize(_data.Length / 2);
            _mostRight--;
            T value = _data[_mostRight];
            return value;
        }

        private void Resize(int newSize)
        {
            int newStart = newSize / 2 - Size / 2;

            T[] newData = new T[newSize];
            Array.Copy(_data, _mostLeft, newData, newStart, Size);
            _data = newData;

            _mostRight = newStart + Size;
            _mostLeft = newStart;
        }

        private void Rearrange()
        {
            int newStart = _data.Length / 2 - Size / 2;
            int copyOfnewStart = newStart;
            for (int i = _mostLeft; i < _mostRight; i++)
            {
                _data[newStart] = _data[i];
                newStart++;
            }
            _mostLeft = copyOfnewStart;
            _mostRight = newStart + 1;
        }
    }
}
