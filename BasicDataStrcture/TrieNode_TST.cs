using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_TST<V>
    {
        public V Value { get; set; }

        public object[] Next { get; }

        public TrieNode_TST(int R)
        {
            Next = new object[R];
        }
    }
}
