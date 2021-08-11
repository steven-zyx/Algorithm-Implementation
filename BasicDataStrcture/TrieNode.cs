using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode<V>
    {
        public V Value;
        public TrieNode<V>[] Next;
    }
}
