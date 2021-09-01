using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_Char<V> : ITrieNode<V>
    {
        protected V _value;
        protected ITrieNode<V>[] _next;

        public TrieNode_Char(int R)
        {
            _next = new ITrieNode<V>[R];
        }

        public V GetValue() => _value;

        public void SetValue(V value) => _value = value;

        public ITrieNode<V> GetNext(int index) => _next[index];

        public ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R)
        {
            _next[index] = node;
            return this;
        }

        public int NextCount()
        {
            int cnt = 0;
            for (int i = 0; i < _next.Length; i++)
                if (_next[i] != null)
                    cnt++;
            return cnt;
        }

        public TrieNode_Str<V> MergeChild()
        {
            int index = -1;
            for (int i = 0; i < _next.Length; i++)
                if (_next[i] != null)
                {
                    index = i;
                    break;
                }

            TrieNode_Str<V> current = new TrieNode_Str<V>();
            current.SetValue(_value);
            current.GetNext(index);
            current.SetNext(index, _next[index], _next.Length);
            return current;
        }
    }
}
