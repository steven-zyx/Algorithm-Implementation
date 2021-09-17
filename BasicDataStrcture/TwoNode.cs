using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TwoNode<K, V> : TreeNodeBase<K, V> where K : IComparable<V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public TreeNodeBase<K, V> Left { get; set; }
        public TreeNodeBase<K, V> Right { get; set; }

        public TwoNode(K key, V value, int n)
        {
            Key = key;
            Value = value;
            N = n;
        }
    }
}