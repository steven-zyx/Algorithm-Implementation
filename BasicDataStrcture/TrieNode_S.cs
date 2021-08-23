using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_S<V> : TrieNode_Base<V>
    {
        public int Index { get; set; }
        public TrieNode_Base<V> Next { get; set; }

        public override TrieNode_Base<V> NextNode(int index)
        {
            if (Index == index)
                return Next;
            else
                return null;
        }

        public TrieNode_A<V> ToNodeA(int R)
        {
            TrieNode_A<V> node = new TrieNode_A<V>();
            node.Value = Value;
            node.Next = new TrieNode_Base<V>[R];
            node.Next[Index] = Next;
            return node;
        }
    }
}
