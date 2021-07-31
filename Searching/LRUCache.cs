using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class LRUCache<K>
    {
        private Node_D<K> _first;
        private Node_D<K> _last;
        private ISymbolTable<K, Node_D<K>> _st;

        public LRUCache()
        {
            _st = new SeperateChainingHashST<K, Node_D<K>>();
        }

        public void Access(K key)
        {
            Node_D<K> n = _st.Get(key);
            if (n == null)
            {
                n = new Node_D<K>(key);
                AddToFirst(n);
                _st.Put(key, n);
            }
            else
            {
                Remove(n);
                AddToFirst(n);
            }
        }

        public void Remove()
        {
            if (_st.IsEmpty) return;
            _st.Delete(_last.Value);
            Remove(_last);
        }

        private void Remove(Node_D<K> n)
        {
            if (n.Previous == null)
                _first = n.Next;
            else
                n.Previous.Next = n.Next;

            if (n.Next == null)
                _last = n.Previous;
            else
                n.Next.Previous = n.Previous;
        }

        private void AddToFirst(Node_D<K> n)
        {
            n.Next = _first;
            n.Previous = null;
            if (_first == null)
                _last = n;
            else
                _first.Previous = n;
            _first = n;
        }

        public IEnumerable<K> Keys()
        {
            for (Node_D<K> node = _first; node != null; node = node.Next)
                yield return node.Value;
        }
    }
}
