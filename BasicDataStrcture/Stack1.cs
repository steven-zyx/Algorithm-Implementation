using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Stack1<T>
    {
        private Node<T> _top;
        private int _count = 0;

        public int Length => _count;

        public void Push(T t)
        {
            _count++;
            Node<T> data = new Node<T>(t);
            data.Next = _top;
            _top = data;
        }

        public T Pop()
        {
            if (_count == 0)
                throw new Exception();
            _count--;
            T data = _top.Value;
            _top = _top.Next;
            return data;
        }
    }
}
