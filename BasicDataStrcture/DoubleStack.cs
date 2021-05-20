using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class DoubleStack<T, QueueT> where QueueT : IDeque<T>, new()
    {
        private IDeque<T> _deque = new QueueT();
        private int _count1 = 0;
        private int _count2 = 0;

        public void Push1(T t)
        {
            _deque.PushLeft(t);
            _count1++;
        }

        public void Push2(T t)
        {
            _deque.PushRight(t);
            _count2++;
        }

        public T Pop1()
        {
            if (_count1 == 0)
                throw new Exception("No more element");
            T item = _deque.PopLeft();
            _count1--;
            return item;
        }

        public T Pop2()
        {
            if (_count2 == 0)
                throw new Exception("No more element");
            T item = _deque.PopRight();
            _count2--;
            return item;
        }
    }
}
