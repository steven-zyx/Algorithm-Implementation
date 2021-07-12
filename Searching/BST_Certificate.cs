using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_Certificate<K, V> : BST<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            base.Put(key, value);
            IsBST();
        }

        public override bool Delete(K key)
        {
            bool result = base.Delete(key);
            IsBST();
            return result;
        }

        public override void DeleteMin()
        {
            base.DeleteMin();
            IsBST();
        }

        public override void DeleteMax()
        {
            base.DeleteMax();
            IsBST();
        }
    }
}
