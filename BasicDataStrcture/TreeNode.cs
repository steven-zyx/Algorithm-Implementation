using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TreeNode<K, V> where K : IComparable
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public TreeNode<K, V> Left { get; set; }
        public TreeNode<K, V> Right { get; set; }
        public int N { get; set; }

        public TreeNode(K key, V value, int n)
        {
            Key = key;
            Value = value;
            N = n;
        }
    }
}
