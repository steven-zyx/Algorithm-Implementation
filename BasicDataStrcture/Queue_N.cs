using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace BasicDataStrcture
{
    public class Queue_N<T> : IQueue<T>, IEnumerable
    {
        private Node<T> _last;
        private Node<T> _first;
        private int _count;

        public Queue_N() { }

        public Queue_N(IEnumerable source)
        {
            foreach (T item in source)
                Enqueue(item);
        }

        public Queue_N(IQueue<T> source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                T item = source.Dequeue();
                source.Enqueue(item);
                Enqueue(item);
            }
        }

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

        public IEnumerator GetEnumerator()
        {
            Node<T> current = _first;
            for (int i = 0; i < _count; i++)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}
