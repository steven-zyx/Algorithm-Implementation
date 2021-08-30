using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public interface ITrieNode<V>
    {
        void SetValue(V value);

        ITrieNode<V> GetNext(int index);

        ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R);
    }
}
