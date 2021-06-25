using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SelfOrganizingSearch<K, V> : SequentialSearchST<K, V> where K : IComparable
    {
        public override V Get(K key)
        {
            if (_start == null)
                return default(V);
            else if (_start.Key.CompareTo(key) == 0)
                return _start.Value;

            for (var c = _start; c.Next != null; c = c.Next)
            {
                if (c.Next.Key.CompareTo(key) == 0)
                {
                    var item = c.Next;
                    c.Next = item.Next;
                    item.Next = _start;
                    _start = item;
                    return item.Value;
                }
            }
            return default(V);
        }
    }
}
