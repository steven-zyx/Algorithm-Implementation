using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Deque_N<T> : IDeque<T>
    {
        private int _count;
        private Node_D<T> _mostLeft;
        private Node_D<T> _mostRight;

        public Deque_N()
        {
            _count = 0;
        }

        public bool IsEmpty => _count == 0;

        public int Size => _count;

        public void PushLeft(T t)
        {
            _count++;
            Node_D<T> data = new Node_D<T>(t);
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
            Node_D<T> data = new Node_D<T>(t);
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
                _mostRight = null;
            else
                _mostLeft.Previous = null;
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
                _mostLeft = null;
            else
                _mostRight.Next = null;
            return value;
        }

        public IEnumerable<T> FromLeftToRight()
        {
            for (Node_D<T> current = _mostLeft; current != null; current = current.Next)
                yield return current.Value;
        }

        public IEnumerable<T> FromRightToLeft()
        {
            for (Node_D<T> current = _mostRight; current != null; current = current.Previous)
                yield return current.Value;
        }
    }
}
