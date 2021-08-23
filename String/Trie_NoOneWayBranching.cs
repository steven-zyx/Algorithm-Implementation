using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public class Trie_NoOneWayBranching<V> : IStringSymbolTable<V>
    {
        protected TrieNode_Base<V> _root;
        protected Alphabet _alphabet;

        public Trie_NoOneWayBranching(Alphabet _a)
        {
            _alphabet = _a;
        }

        public void Put(string key, V value)
        {
            _root = Put(_root, key, 0, value);
        }

        public TrieNode_Base<V> Put(TrieNode_Base<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TrieNode_S<V>();
            if (digit == key.Length)
            {
                node.Value = value;
                return node;
            }

            int index = _alphabet.ToIndex(key[digit]);
            if (node is TrieNode_S<V> s)
                if (s.Index == index)
                {
                    s.Next = Put(s.Next, key, digit + 1, value);
                    return node;
                }
                else
                    node = s.ToNodeA(_alphabet.R);

            TrieNode_A<V> a = node as TrieNode_A<V>;
            a.Next[index] = Put(a.Next[index], key, digit + 1, value);
            return node;
        }

        public V Get(string key)
        {
            TrieNode_Base<V> node = Get(_root, key, 0);
            if (node == null)
                return default(V);
            return node.Value;
        }

        protected TrieNode_Base<V> Get(TrieNode_Base<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                return node;

            int index = _alphabet.ToIndex(key[digit]);
            return Get(node.NextNode(index), key, digit + 1);
        }

        public bool IsEmpty => _root == null;

        public bool Contains(string key)
        {
            TrieNode_Base<V> node = Get(_root, key, 0);
            return node != null && !node.Value.Equals(default(V));
        }

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Keys()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysWithPrefix(string s)
        {
            throw new NotImplementedException();
        }

        public string LongestPrefixOf(string s)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }
}
