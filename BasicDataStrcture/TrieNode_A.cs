using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_A<V> : TrieNode_Base<V>
    {
        public TrieNode_Base<V>[] Next { get; set; }

        public override TrieNode_Base<V> NextNode(int index)
        {
            if (Next == null)
                return null;
            else
                return Next[index];
        }
    }
}
