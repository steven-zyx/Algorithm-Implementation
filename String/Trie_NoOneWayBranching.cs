using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Searching;

namespace String
{
    public partial class Trie_NoOneWayBranching<V> : IStringSymbolTable<V>
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
                node = new TrieNode_Str<V>();
            if (key.Length == digit)
            {
                node.Value = value;
                return node;
            }

            int index = _alphabet.ToIndex(key[digit]);
            if (node is TrieNode_Str<V> s)
            {
                ITrieNode<V> next = s.GetNext(index);
                s.Digit++;
                ITrieNode<V> result = Put(next, key, digit + 1, value);
                s.Digit--;
                return s.SetNext(index, result, _alphabet.R);
            }
            else
            {
                TrieNode_Char<V> c = node as TrieNode_Char<V>;
                c.Next[index] = Put(c.Next[index], key, digit + 1, value);
                return c;
            }
        }

        public V Get(string key)
        {
            var result = Get(_root, key, 0);
            if (result.node is TrieNode_Char<V> c)
                return c.Value;
            else if (result.node is TrieNode_Str<V> s)
                return s._values[result.digit];
            else
                return default(V);
        }

        protected (ITrieNode<V> node, int digit) Get(ITrieNode<V> node, string key, int digit)
        {
            if (node == null)
                return (null, -1);
            if (digit == key.Length)
            {
                if (node is TrieNode_Str<V> sNode)
                    return (sNode, sNode.Digit);
                else
                    return (node, -1);
            }

            int index = _alphabet.ToIndex(key[digit]);
            if (node is TrieNode_Str<V> s)
            {
                ITrieNode<V> next = s.GetNext(index);
                s.Digit++;
                var result = Get(next, key, digit + 1);
                s.Digit--;
                return result;
            }
            else
            {
                TrieNode_Char<V> c = node as TrieNode_Char<V>;
                return Get(c.Next[index], key, digit + 1);
            }
        }

        public bool Delete(string key)
        {
            _root = Delete(_root, key, 0);
            if (_root is TrieNode_Str<V> s && s.IsFinalEmpty())
                _root = null;
            return true;
        }

        protected ITrieNode<V> Delete(ITrieNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                node.Value = default(V);
            else
            {
                int index = _alphabet.ToIndex(key[digit]);
                if (node is TrieNode_Str<V> s)
                {
                    ITrieNode<V> next = s.GetNext(index);
                    s.Digit++;
                    ITrieNode<V> result = Delete(next, key, digit + 1);
                    s.Digit--;
                    s.SetNext(index, result, _alphabet.R);

                    if (!s.Value.Equals(default(V)) && s.IsFinalEmpty())
                        s.Shrink();
                }
                else
                {
                    TrieNode_Char<V> c = node as TrieNode_Char<V>;
                    c.Next[index] = Delete(c.Next[index], key, digit + 1);

                    if (c.Next[index] is TrieNode_Str<V> sSub && sSub.IsFinalEmpty())
                        c.Next[index] = null;

                    List<int> childIndex = new List<int>();
                    for (int i = 0; i < _alphabet.R; i++)
                        if (c.Next[i] != null)
                            childIndex.Add(i);
                    if (childIndex.Count == 1)
                    {
                        TrieNode_Str<V> current = new TrieNode_Str<V>();
                        current.Value = c.Value;
                        current.SetNext(childIndex[0], c.Next[childIndex[0]], _alphabet.R);
                        node = current;
                    }
                }
            }
            return node;
        }

        public bool IsEmpty => _root is null;

        public bool Contains(string key)
        {
            var result = Get(_root, key, 0);
            return result.node is TrieNode_Char<V> c && !c.Value.Equals(default(V)) ||
                result.node is TrieNode_Str<V> s && !s._values[result.digit].Equals(default(V));
        }

        public IEnumerable<string> Keys()
        {
            Queue<string> keys = new Queue<string>();
            Keys(_root, "", keys);
            return keys;
        }

        protected void Keys(ITrieNode<V> node, string text, Queue<string> keys)
        {
            if (node == null)
                return;
            if (!node.Value.Equals(default(V)))
                keys.Enqueue(text);

            if (node is TrieNode_Str<V> s)
            {
                int index = s.Characters[s.Digit];
                if (index >= 0)
                {
                    ITrieNode<V> next = s.GetNext(index);
                    s.Digit++;
                    Keys(next, text + _alphabet.ToChar(index), keys);
                    s.Digit--;
                }
            }
            else if (node is TrieNode_Char<V> c)
                for (int i = 0; i < _alphabet.R; i++)
                    if (c.Next[i] != null)
                        Keys(c.Next[i], text + _alphabet.ToChar(i), keys);
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            Queue<string> keys = new Queue<string>();
            KeysThatMatch(_root, "", s, keys);
            return keys;
        }

        protected void KeysThatMatch(ITrieNode<V> node, string text, string pat, Queue<string> keys)
        {
            if (node == null)
                return;
            if (text.Length == pat.Length)
            {
                if (!node.Value.Equals(default(V)))
                    keys.Enqueue(text);
                return;
            }

            char ch = pat[text.Length];
            if (node is TrieNode_Char<V> c)
            {
                if (!ch.Equals('.'))
                    KeysThatMatch(c.Next[_alphabet.ToIndex(ch)], text + ch, pat, keys);
                else
                    for (int i = 0; i < _alphabet.R; i++)
                        if (c.Next[i] != null)
                            KeysThatMatch(c.Next[i], text + _alphabet.ToChar(i), pat, keys);
            }
            else if (node is TrieNode_Str<V> s)
            {
                if (ch.Equals('.'))
                {
                    int index = s.Characters[s.Digit];
                    if (index >= 0)
                    {
                        ITrieNode<V> next = s.GetNext(index);
                        s.Digit++;
                        KeysThatMatch(next, text + _alphabet.ToChar(index), pat, keys);
                        s.Digit--;
                    }
                }
                else
                {
                    ITrieNode<V> next = s.GetNext(_alphabet.ToIndex(ch));
                    s.Digit++;
                    KeysThatMatch(next, text + ch, pat, keys);
                    s.Digit--;
                }
            }
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            Queue<string> keys = new Queue<string>();

            var result = Get(_root, prefix, 0);
            if (result.node is TrieNode_Char<V> c)
                Keys(c, prefix, keys);
            else if (result.node is TrieNode_Str<V> s)
            {
                s.Digit = result.digit;
                Keys(s, prefix, keys);
                s.Digit = 0;
            }

            return keys;
        }

        public string LongestPrefixOf(string s)
        {
            int length = LongestPrefixOf(_root, s, 0, 0);
            return s.Substring(0, length);
        }

        protected int LongestPrefixOf(ITrieNode<V> node, string text, int digit, int length)
        {
            if (node == null)
                return length;
            if (!node.Value.Equals(default(V)))
                length = digit;
            if (digit == text.Length)
                return length;

            int index = _alphabet.ToIndex(text[digit]);
            if (node is TrieNode_Char<V> c)
                return LongestPrefixOf(c.Next[index], text, digit + 1, length);
            else
            {
                TrieNode_Str<V> s = node as TrieNode_Str<V>;
                ITrieNode<V> next = s.GetNext(index);
                s.Digit++;
                int result = LongestPrefixOf(next, text, digit + 1, length);
                s.Digit--;
                return result;
            }
        }

        public int Size() => Size(_root);

        protected int Size(ITrieNode<V> node)
        {
            if (node == null)
                return 0;
            int cnt = 0;
            if (!node.Value.Equals(default(V)))
                cnt++;

            if (node is TrieNode_Char<V> c)
            {
                for (int i = 0; i < _alphabet.R; i++)
                    if (c.Next[i] != null)
                        cnt += Size(c.Next[i]);
            }
            else if (node is TrieNode_Str<V> s)
            {
                int index = s.Characters[s.Digit];
                if (index >= 0)
                {
                    ITrieNode<V> next = s.GetNext(index);
                    s.Digit++;
                    cnt += Size(next);
                    s.Digit--;
                }
            }
            return cnt;
        }
    }

    public partial class Trie_NoOneWayBranching<V> : ICertificate
    {
        public void Certificate()
        {
        }
    }
}
