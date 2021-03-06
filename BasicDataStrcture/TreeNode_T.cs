using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TreeNode_T<K, V> : TreeNode<K, V> where K : IComparable
    {
        public TreeNode_T<K, V> Pred { get; set; }
        public TreeNode_T<K, V> Succ { get; set; }

        public TreeNode_T(K key, V value, int n) : base(key, value, n) { }
    }
}
