using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class SequentialSearchMultiST<K, V> : SequentialSearchST<K, V>, IMultiSymbolTable<K, V>
    {
        public override void Put(K key, V value)
        {
            Node_P<K, V> newNode = new Node_P<K, V>(key, value, _start);
            _start = newNode;
            _count++;
        }

        public IEnumerable<V> GetAll(K key)
        {
            for (var c = _start; c != null; c = c.Next)
                if (key.Equals(c.Key))
                    yield return c.Value;
        }

        public int DeleteAll(K key)
        {
            int result = 0;
            Node_P<K, V> beforeStart = new Node_P<K, V>(default(K), default(V), _start);
            for (var p = beforeStart; p != null; p = p.Next)
                while (p.Next != null && p.Next.Key.Equals(key))
                {
                    result++;
                    p.Next = p.Next.Next;
                }
            _start = beforeStart.Next;
            _count -= result;
            return result;
        }
    }
}
