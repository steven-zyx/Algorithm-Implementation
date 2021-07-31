using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class UniQueue<V> : IQueue<V>, IEnumerable<V>
    {
        private Queue_N<V> _queue;
        private ISet<V> _set;

        public UniQueue()
        {
            _queue = new Queue_N<V>();
            _set = new SeperateChainingSET<V>();
        }

        public V Dequeue() => _queue.Dequeue();

        public void Enqueue(V t)
        {
            if (!_set.Contains(t))
            {
                _queue.Enqueue(t);
                _set.Put(t);
            }
        }

        public int Length => _queue.Length;

        public IEnumerator<V> GetEnumerator() => _queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
