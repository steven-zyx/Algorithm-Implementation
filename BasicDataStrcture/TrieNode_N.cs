using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_N<V> : TrieNode<V>
    {
        public int N { get; set; }

        public TrieNode_N(int R) : base(R) { }
    }
}
