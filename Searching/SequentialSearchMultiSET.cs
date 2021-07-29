using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class SequentialSearchMultiSET<K> : SequentialSearchSET<K>, IMultiSet<K>
    {
        public override void Put(K key)
        {
            Node<K> newStart = new Node<K>(key, _start);
            _start = newStart;
            _count++;
        }

        public int Count(K key)
        {
            int result = 0;
            for (var c = _start; c != null; c = c.Next)
                if (c.Value.Equals(key))
                    result++;
            return result;
        }

        public int DeleteAll(K key)
        {
            int result = 0;
            Node<K> beforeStart = new Node<K>(default(K), _start);
            for (var p = beforeStart; p != null; p = p.Next)
                while (p.Next != null && p.Next.Value.Equals(key))
                {
                    p = p.Next.Next;
                    result++;
                }
            _start = beforeStart.Next;
            _count -= result;
            return result;
        }
    }
}
