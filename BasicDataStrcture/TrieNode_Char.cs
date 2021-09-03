using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_Char<V> : ITrieNode<V>
    {
        public V Value { get; set; }

        public ITrieNode<V>[] Next { get; set; }

        public TrieNode_Char(int R)
        {
            Next = new ITrieNode<V>[R];
        }
    }
}
