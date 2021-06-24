using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class BinarySearchST_C<K, V> : BinarySearchST<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            base.Put(key, value);
            Certificate();
        }

        public override bool Delete(K key)
        {
            bool result = base.Delete(key);
            Certificate();
            return result;
        }

        public override void DeleteMax()
        {
            base.DeleteMax();
            Certificate();
        }

        public override void DeleteMin()
        {
            base.DeleteMin();
            Certificate();
        }

        private void Certificate()
        {
            K current = _keys[0];
            for (int i = 1; i < _count; i++)
            {
                K key = _keys[i];
                if (current.CompareTo(key) >= 0 ||
                    i != Rank(Select(i)) ||
                    key.CompareTo(Select(Rank(key))) != 0)
                {
                    throw new Exception("Inconsistant");
                }
                current = key;
            }
        }
    }
}
