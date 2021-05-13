using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Deque1<T>
    {
        private int _count;
        private Node2<T> _mostLeft;
        private Node2<T> _mostRight;

        public Deque1()
        {
            _count = 0;
        }

        public bool IsEmpty => _count == 0;

        public int Size => _count;

        public void PushLeft(T t)
        {
            _count++;
            Node2<T> data = new Node2<T>(t);
            if (_mostLeft == null)
            {
                _mostLeft = data;
                _mostRight = data;
            }
            else
            {
                data.Next = _mostLeft;
                _mostLeft.Previous = data;
                _mostLeft = data;
            }
        }

        public void PushRight(T t)
        {
            _count++;
            Node2<T> data = new Node2<T>(t);
            if (_mostRight == null)
            {
                _mostRight = data;
                _mostLeft = data;
            }
            else
            {
                data.Previous = _mostRight;
                _mostRight.Next = data;
                _mostRight = data;
            }
        }

        public T PopLeft()
        {
            if (_mostLeft == null)
            {
                throw new Exception("No more element");
            }
            _count--;
            T value = _mostLeft.Value;
            _mostLeft = _mostLeft.Next;
            if (_mostLeft == null)
            {
                _mostRight = null;
            }
            return value;
        }

        public T PopRight()
        {
            if (_mostRight == null)
            {
                throw new Exception("No more element");
            }
            _count--;
            T value = _mostRight.Value;
            _mostRight = _mostRight.Previous;
            if (_mostRight == null)
            {
                _mostLeft = null;
            }
            return value;
        }
    }
}
