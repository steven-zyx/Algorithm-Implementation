using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_Base<V>
    {
        public V Value { get; set; }

        public virtual TrieNode_Base<V> NextNode(int index)
        {
            throw new NotImplementedException();
        }
    }
}
