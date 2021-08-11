using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public class TrieST<V> : IStringSymbolTable<V>
    {
        protected TrieNode<V> _root;
        protected Alphabet _a;

        public TrieST(Alphabet a)
        {
            _a = a;
        }

        public void Put(string key, V value)
        {
            _root = Put(_root, key, value, 0);
        }

        protected TrieNode<V> Put(TrieNode<V> node, string key, V value, int digit)
        {
            if (node == null)
                node = new TrieNode<V>();
            if (digit == key.Length)
            {
                node.Value = value;
                return node;
            }
            int c = _a.ToIndex(key[digit]);
            node.Next[c] = Put(node.Next[c], key, value, digit + 1);
            return node;
        }

        public V Get(string key)
        {
            TrieNode<V> node = Get(_root, key, 0);
            if (node == null)
                return default(V);
            return node.Value;
        }

        protected TrieNode<V> Get(TrieNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                return node;
            int c = _a.ToIndex(key[digit]);
            return Get(node.Next[c], key, digit + 1);
        }

        public int Size() => Size(_root);

        protected int Size(TrieNode<V> node)
        {
            if (node == null)
                return 0;
            int cnt = 0;
            if (!node.Value.Equals(default(V)))
                cnt++;
            for (int i = 0; i < _a.R; i++)
                cnt += Size(node.Next[i]);
            return cnt;
        }

        public IEnumerable<string> Keys()
        {
            Queue<string> keys = new Queue<string>();
            Collect(_root, "", keys);
            return keys;
        }

        public IEnumerable<string> KeysWithPrefix(string s)
        {
            Queue<string> keys = new Queue<string>();
            Collect(Get(_root, s, 0), s, keys);
            return keys;
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            Queue<string> keys = new Queue<string>();
            Collect(_root, "", s, keys);
            return keys;
        }

        protected void Collect(TrieNode<V> node, string text, Queue<string> keys)
        {
            if (node == null)
                return;
            if (!node.Value.Equals(default(V)))
                keys.Enqueue(text);
            for (int i = 0; i < _a.R; i++)
                Collect(node.Next[i], text + _a.ToChar(i), keys);
        }

        protected void Collect(TrieNode<V> node, string text, string pat, Queue<string> keys)
        {
            if (node == null)
                return;
            if (text.Length == pat.Length)
            {
                if (!node.Value.Equals(default(V)))
                    keys.Enqueue(text);
                return;
            }

            char c = pat[text.Length];
            if (c == '.')
                for (int i = 0; i < _a.R; i++)
                    Collect(node.Next[i], text + _a.ToChar(i), pat, keys);
            else
                Collect(node.Next[_a.ToIndex(c)], text + c, pat, keys);
        }
        public string LongestPrefixOf(string s)
        {
            int length = LongestPrefixOf(_root, s, 0, 0);
            return s.Substring(0, length);
        }

        protected int LongestPrefixOf(TrieNode<V> node, string text, int digit, int length)
        {
            if (node == null)
                return length;
            if (!node.Value.Equals(default(V)))
                length = digit;
            if (digit == text.Length)
                return length;

            int i = _a.ToIndex(text[digit]);
            return LongestPrefixOf(node.Next[i], text, digit + 1, length);
        }

        public bool IsEmpty => _root == null;

        public bool Contains(string key)
        {
            TrieNode<V> node = Get(_root, key, 0);
            return node == null || node.Value.Equals(default(V));
        }

        public bool Delete(string key)
        {
            _root = Delete(_root, key, 0);
            return true;
        }

        protected TrieNode<V> Delete(TrieNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                node.Value = default(V);
            else
            {
                int i = _a.ToChar(key[digit]);
                node.Next[i] = Delete(node.Next[i], key, digit + 1);
            }

            if (!node.Value.Equals(default(V)))
                return node;
            for (int i = 0; i < _a.R; i++)
                if (node.Next[i] != null)
                    return node;
            return null;
        }
    }
}
