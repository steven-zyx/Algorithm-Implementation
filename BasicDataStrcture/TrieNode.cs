using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode<V>
    {
        public V Value { get; set; }
        public TrieNode<V>[] Next { get; }

        public TrieNode(int R)
        {
            Next = new TrieNode<V>[R];
        }
    }
}
