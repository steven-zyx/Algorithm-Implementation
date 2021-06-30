using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_CountGet<K, V> : BST<K, V> where K : IComparable
    {
        private int _countGet = 0;
        public int CountGet => _countGet;

        public override V Get(K key)
        {
            _countGet = 0;
            return base.Get(key);
        }

        protected override TreeNode<K, V> Get(TreeNode<K, V> x, K key)
        {
            _countGet++;
            return base.Get(x, key);
        }
    }
}
