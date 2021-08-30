using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public class Trie_NoOneWayBranching<V> : IStringSymbolTable<V>
    {
        protected ITrieNode<V> _root;
        protected Alphabet _alphabet;

        public Trie_NoOneWayBranching(Alphabet a)
        {
            _alphabet = a;
        }

        public void Put(string key, V value)
        {
            _root = Put(_root, key, 0, value);
        }

        protected ITrieNode<V> Put(ITrieNode<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TrieNode_Str<V>(key, digit);
            if (key.Length == digit)
            {
                node.SetValue(value);
                return node;
            }

            int index = _alphabet.ToIndex(key[digit]);
            var result = Put(node.GetNext(index), key, digit + 1, value);
            return node.SetNext(index, result, _alphabet.R);
        }

        public bool IsEmpty => throw new NotImplementedException();

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public V Get(string key)
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
