using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Stack_N<T> : IStack<T>, IEnumerable<T>
    {
        private Node<T> _top;
        private int _count = 0;

        public Stack_N() { }

        public Stack_N(IStack<T> source)
        {
            T[] data = new T[source.Length];
            for (int i = data.Length - 1; i >= 0; i--)
                data[i] = source.Pop();
            foreach (T item in data)
            {
                source.Push(item);
                Push(item);
            }
        }

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

        public T Peek() => _top.Value;

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = _top;
            for (int i = 0; i < _count; i++)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
