using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Searching;

namespace String
{
    public partial class HybridST<V> : IStringSymbolTable<V>
    {
        protected TrieNode_TST<V> _root;
        protected Alphabet _alphabet;

        public HybridST(Alphabet a)
        {
            _alphabet = a;
        }

        public void Put(string key, V value)
        {
            _root = PutToTrie(_root, key, 0, value);
        }

        protected TrieNode_TST<V> PutToTrie(TrieNode_TST<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TrieNode_TST<V>(_alphabet.R);
            if (digit == key.Length)
            {
                node.Value = value;
                return node;
            }

            int c = _alphabet.ToIndex(key[digit]);
            if (digit == 0)
                node.Next[c] = PutToTrie(node.Next[c] as TrieNode_TST<V>, key, digit + 1, value);
            else
                node.Next[c] = PutToTST(node.Next[c] as TSTNode<V>, key, digit, value);
            return node;
        }

        protected TSTNode<V> PutToTST(TSTNode<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TSTNode<V>(key[digit]);

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = PutToTST(node.Left, key, digit, value);
            else if (result > 0)
                node.Right = PutToTST(node.Right, key, digit, value);
            else if (digit + 1 == key.Length)
                node.Value = value;
            else
                node.Mid = PutToTST(node.Mid, key, digit + 1, value);
            return node;
        }

        public V Get(string key)
        {
            object result = GetFromTrie(_root, key, 0);
            if (result is TrieNode_TST<V> r)
                return r.Value;
            else if (result is TSTNode<V> t)
                return t.Value;
            else
                return default(V);
        }

        protected object GetFromTrie(TrieNode_TST<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                return node;
            int c = _alphabet.ToIndex(key[digit]);
            if (digit == 0)
                return GetFromTrie(node.Next[c] as TrieNode_TST<V>, key, digit + 1);
            else
                return GetFromTST(node.Next[c] as TSTNode<V>, key, digit);
        }

        protected TSTNode<V> GetFromTST(TSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                return GetFromTST(node.Left, key, digit);
            else if (result > 0)
                return GetFromTST(node.Right, key, digit);
            else if (digit + 1 == key.Length)
                return node;
            else
                return GetFromTST(node.Mid, key, digit + 1);
        }

        public bool Delete(string key)
        {
            _root = DeleteFromTrie(_root, key, 0);
            return false;
        }

        protected TrieNode_TST<V> DeleteFromTrie(TrieNode_TST<V> node, string key, int digit)
        {
            if (node == null)
                return null;
            if (digit == key.Length)
                node.Value = default(V);
            else
            {
                int i = _alphabet.ToIndex(key[digit]);
                if (digit == 0)
                    node.Next[i] = DeleteFromTrie(node.Next[i] as TrieNode_TST<V>, key, digit + 1);
                else
                    node.Next[i] = DeleteFromTST(node.Next[i] as TSTNode<V>, key, digit);
            }

            if (!node.Value.Equals(default(V)))
                return node;
            for (int i = 0; i < _alphabet.R; i++)
                if (node.Next[i] != null)
                    return node;
            return null;
        }

        protected TSTNode<V> DeleteFromTST(TSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = DeleteFromTST(node.Left, key, digit);
            else if (result > 0)
                node.Right = DeleteFromTST(node.Right, key, digit);
            else if (digit + 1 == key.Length)
                node.Value = default(V);
            else
                node.Mid = DeleteFromTST(node.Mid, key, digit + 1);

            if (node.Value.Equals(default(V)) && node.Mid == null)
                return DeleteNode(node);
            else
                return node;
        }

        protected virtual TSTNode<V> DeleteNode(TSTNode<V> node)
        {
            if (node.Right == null)
                return node.Left;
            else if (node.Left == null)
                return node.Right;
            else
            {
                TSTNode<V> substitute = Min(node.Right);
                substitute.Right = DeleteMin(node.Right);
                substitute.Left = node.Left;
                return substitute;
            }
        }

        protected virtual TSTNode<V> DeleteMin(TSTNode<V> node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = DeleteMin(node.Left);
            return node;
        }

        protected TSTNode<V> Min(TSTNode<V> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public bool Contains(string key)
        {
            object result = GetFromTrie(_root, key, 0);
            if (result is TrieNode_TST<V> r)
                return !r.Value.Equals(default(V));
            else if (result is TSTNode<V> t)
                return !t.Value.Equals(default(V));
            else
                return false;
        }

        public bool IsEmpty => _root == null;

        public IEnumerable<string> Keys()
        {
            Queue<string> keys = new Queue<string>();
            TrieKeys(_root, "", keys);
            return keys;
        }

        protected void TrieKeys(TrieNode_TST<V> node, string text, Queue<string> keys)
        {
            if (node == null)
                return;
            if (!node.Value.Equals(default(V)))
                keys.Enqueue(text);
            for (int i = 0; i < _alphabet.R; i++)
                if (node.Next[i] != null)
                    if (node.Next[i] is TrieNode_TST<V> r)
                        TrieKeys(r, text + _alphabet.ToChar(i), keys);
                    else if (node.Next[i] is TSTNode<V> t)
                        TSTKeys(t, text, keys);
        }

        protected void TSTKeys(TSTNode<V> node, string text, Queue<string> keys)
        {
            if (node == null)
                return;

            if (!node.Value.Equals(default(V)))
                keys.Enqueue(text + node.C);

            TSTKeys(node.Left, text, keys);
            TSTKeys(node.Mid, text + node.C, keys);
            TSTKeys(node.Right, text, keys);
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            Queue<string> keys = new Queue<string>();
            KeysThatMatchFromTrie(_root, "", s, keys);
            return keys;
        }

        protected void KeysThatMatchFromTrie(TrieNode_TST<V> node, string text, string pat, Queue<string> keys)
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
            {
                for (int i = 0; i < _alphabet.R; i++)
                    if (node.Next[i] != null)
                        if (text.Length == 0)
                            KeysThatMatchFromTrie(node.Next[i] as TrieNode_TST<V>, text + _alphabet.ToChar(i), pat, keys);
                        else
                            KeysThatMatchFromTST(node.Next[i] as TSTNode<V>, pat, text, keys);
            }
            else
            {
                var next = node.Next[_alphabet.ToIndex(c)];
                if (text.Length == 0)
                    KeysThatMatchFromTrie(next as TrieNode_TST<V>, text + c, pat, keys);
                else
                    KeysThatMatchFromTST(next as TSTNode<V>, pat, text, keys);
            }
        }

        protected void KeysThatMatchFromTST(TSTNode<V> node, string pat, string text, Queue<string> keys)
        {
            if (node == null)
                return;

            if (pat[text.Length].Equals('.'))
            {
                KeysThatMatchFromTST(node.Left, pat, text, keys);
                KeysThatMatchFromTST(node.Mid, pat, text + node.C, keys);
                KeysThatMatchFromTST(node.Right, pat, text, keys);
            }
            else
            {
                int result = pat[text.Length].CompareTo(node.C);
                if (result < 0)
                    KeysThatMatchFromTST(node.Left, pat, text, keys);
                else if (result > 0)
                    KeysThatMatchFromTST(node.Right, pat, text, keys);
                else if (text.Length == pat.Length - 1)
                    keys.Enqueue(text + node.C);
                else
                    KeysThatMatchFromTST(node.Mid, pat, text + node.C, keys);
            }
        }

        public IEnumerable<string> KeysWithPrefix(string s)
        {
            Queue<string> keys = new Queue<string>();
            var node = GetFromTrie(_root, s, 0);
            if (node is TrieNode_TST<V> r)
                TrieKeys(r, s, keys);
            else if (node is TSTNode<V> t)
                TSTKeys(t.Mid, s, keys);
            return keys;
        }

        public string LongestPrefixOf(string s)
        {
            int length = LongestPrefixOfFromTrie(_root, s, 0, 0);
            return s.Substring(0, length);
        }

        protected int LongestPrefixOfFromTrie(TrieNode_TST<V> node, string text, int digit, int length)
        {
            if (node == null)
                return length;
            if (!node.Value.Equals(default(V)))
                length = digit;
            if (digit == text.Length)
                return length;

            int i = _alphabet.ToIndex(text[digit]);
            if (digit == 0)
                return LongestPrefixOfFromTrie(node.Next[i] as TrieNode_TST<V>, text, digit + 1, length);
            else
                return LongestPrefixOfFromTST(node.Next[i] as TSTNode<V>, text, digit, length);
        }

        public int LongestPrefixOfFromTST(TSTNode<V> node, string text, int digit, int length)
        {
            if (node == null || digit == text.Length)
                return length;

            int result = text[digit].CompareTo(node.C);
            if (result < 0)
                return LongestPrefixOfFromTST(node.Left, text, digit, length);
            else if (result > 0)
                return LongestPrefixOfFromTST(node.Right, text, digit, length);
            else
            {
                if (!node.Value.Equals(default(V)))
                    length = digit + 1;
                return LongestPrefixOfFromTST(node.Mid, text, digit + 1, length);
            }
        }

        public int Size() => SizeOfTrie(_root);

        protected virtual int SizeOfTrie(TrieNode_TST<V> node)
        {
            if (node == null)
                return 0;
            int cnt = 0;
            if (!node.Value.Equals(default(V)))
                cnt++;
            for (int i = 0; i < _alphabet.R; i++)
                if (node.Next[i] is TrieNode_TST<V> r)
                    cnt += SizeOfTrie(r);
                else if (node.Next[i] is TSTNode<V> t)
                    cnt += SizeOfTST(t);
            return cnt;
        }

        protected virtual int SizeOfTST(TSTNode<V> node)
        {
            if (node == null)
                return 0;

            int cnt = 0;
            if (!node.Value.Equals(default(V)))
                cnt++;

            return cnt + SizeOfTST(node.Left) + SizeOfTST(node.Right) + SizeOfTST(node.Mid);
        }
    }

    public partial class HybridST<V> : ICertificate
    {
        public void Certificate()
        {
        }
    }
}
