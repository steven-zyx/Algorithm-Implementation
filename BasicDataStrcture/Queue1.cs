using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Queue1<T>
    {
        private Node<T> _last;
        private Node<T> _first;
        private int _count;

        public void Enqueue(T t)
        {
            _count++;
            Node<T> data = new Node<T>(t);
            if (_count == 1)
                _first = data;
            else
                _last.Next = data;
            _last = data;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new Exception("No more element");
            _count--;
            T data = _first.Value;
            _first = _first.Next;
            return data;
        }

        public int Length => _count;
    }
}
