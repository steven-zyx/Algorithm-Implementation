using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class GeneralizedQueue_L<T>
    {
        private Node<T> _exit;
        private Node<T> _entry;
        private int _count;

        public GeneralizedQueue_L() { }

        public bool IsEmpty => _count == 0;

        public void Insert(T t)
        {
            _count++;
            Node<T> data = new Node<T>(t);
            if (_exit == null)
            {
                _exit = data;
                _entry = data;
            }
            else
            {
                _entry.Next = data;
                _entry = data;
            }
        }

        public T Delete(int k)
        {
            if (k > _count)
                throw new Exception("Invalid opeartion");
            if (k == 1)
            {
                T value = _exit.Value;
                _exit = null;
                _entry = null;
                _count = 0;
                return value;
            }
            else
            {
                Node<T> previous = _exit;
                for (int i = 2; i < k; i++)
                {
                    previous = previous.Next;
                }
                T value = previous.Next.Value;
                previous.Next = previous.Next.Next;
                _count--;
                return value;
            }
        }
    }
}
