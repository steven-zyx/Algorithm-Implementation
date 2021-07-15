using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Node_P<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node_P<K, V> Next { get; set; }

        public Node_P(K key, V value, Node_P<K, V> next)
        {
            Key = key;
            Value = value;
            Next = next;
        }
    }
}
