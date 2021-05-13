using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Steque<T>
    {
        private Node<T> _first;
        private Node<T> _last;

        public void Push(T t)
        {
            Node<T> data = new Node<T>(t);
            if (_first == null)
                _last = data;
            else
                data.Next = _first;
            _first = data;
        }

        public T Pop()
        {
            if (_first == null)
                throw new Exception("No more element");
            T value = _first.Value;
            _first = _first.Next;
            return value;
        }

        public void Enqueue(T t)
        {
            Node<T> data = new Node<T>(t);
            if (_first == null)
                _first = data;
            else
                _last.Next = data;
            _last = data;
        }
    }
}
