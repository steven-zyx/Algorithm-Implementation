using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Searching;
using Utils;

namespace String
{
    //Symbol table dedicated operation
    public partial class TST<V> : IStringSymbolTable<V>
    {
        protected TSTNode<V> _root;

        public virtual V Get(string key)
        {
            TSTNode<V> node = Get(_root, key, 0);
            if (node == null)
                return default(V);
            else
                return node.Value;
        }

        protected TSTNode<V> Get(TSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                return Get(node.Left, key, digit);
            else if (result > 0)
                return Get(node.Right, key, digit);
            else if (digit + 1 == key.Length)
                return node;
            else
                return Get(node.Mid, key, digit + 1);
        }

        public virtual void Put(string key, V value)
        {
            _root = Put(_root, key, 0, value);
        }

        protected virtual TSTNode<V> Put(TSTNode<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TSTNode<V>(key[digit]);

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = Put(node.Left, key, digit, value);
            else if (result > 0)
                node.Right = Put(node.Right, key, digit, value);
            else if (digit + 1 == key.Length)
                node.Value = value;
            else
                node.Mid = Put(node.Mid, key, digit + 1, value);
            return node;
        }

        public virtual bool IsEmpty => _root == null;

        public virtual bool Contains(string key)
        {
            TSTNode<V> node = Get(_root, key, 0);
            return node != null && !node.Value.IsNullOrDefault();
        }

        public virtual bool Delete(string key)
        {
            _root = Delete(_root, key, 0);
            return true;
        }

        protected virtual TSTNode<V> Delete(TSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = Delete(node.Left, key, digit);
            else if (result > 0)
                node.Right = Delete(node.Right, key, digit);
            else if (digit + 1 == key.Length)
                node.Value = default(V);
            else
                node.Mid = Delete(node.Mid, key, digit + 1);

            if (node.Value.IsNullOrDefault() && node.Mid == null)
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

        public virtual IEnumerable<string> Keys()
        {
            Queue<string> keys = new Queue<string>();
            Keys(_root, "", keys);
            return keys;
        }

        protected void Keys(TSTNode<V> node, string text, Queue<string> keys)
        {
            if (node == null)
                return;

            if (!node.Value.IsNullOrDefault())
                keys.Enqueue(text + node.C);

            Keys(node.Left, text, keys);
            Keys(node.Mid, text + node.C, keys);
            Keys(node.Right, text, keys);
        }

        public virtual int Size() => Size(_root);

        protected virtual int Size(TSTNode<V> node)
        {
            if (node == null)
                return 0;

            int cnt = 0;
            if (!node.Value.IsNullOrDefault())
                cnt++;

            return cnt + Size(node.Left) + Size(node.Right) + Size(node.Mid);
        }
    }

    //string dedicated operation
    public partial class TST<V>
    {
        public virtual IEnumerable<string> KeysThatMatch(string pat)
        {
            Queue<string> keys = new Queue<string>();
            KeysThatMatch(_root, pat, "", keys);
            return keys;
        }

        protected void KeysThatMatch(TSTNode<V> node, string pat, string text, Queue<string> keys)
        {
            if (node == null)
                return;

            if (pat[text.Length].Equals('.'))
            {
                KeysThatMatch(node.Left, pat, text, keys);
                KeysThatMatch(node.Mid, pat, text + node.C, keys);
                KeysThatMatch(node.Right, pat, text, keys);
            }
            else
            {
                int result = pat[text.Length].CompareTo(node.C);
                if (result < 0)
                    KeysThatMatch(node.Left, pat, text, keys);
                else if (result > 0)
                    KeysThatMatch(node.Right, pat, text, keys);
                else if (text.Length == pat.Length - 1)
                    keys.Enqueue(text + node.C);
                else
                    KeysThatMatch(node.Mid, pat, text + node.C, keys);
            }
        }

        public virtual IEnumerable<string> KeysWithPrefix(string prefix)
        {
            Queue<string> keys = new Queue<string>();
            TSTNode<V> node = Get(_root, prefix, 0);
            if (node != null)
                Keys(node.Mid, prefix, keys);
            return keys;
        }

        public string LongestPrefixOf(string text)
        {
            int length = LongestPrefixOf(_root, text, 0, 0);
            return text.Substring(0, length);
        }

        public int LongestPrefixOf(TSTNode<V> node, string text, int digit, int length)
        {
            if (node == null || digit == text.Length)
                return length;

            int result = text[digit].CompareTo(node.C);
            if (result < 0)
                return LongestPrefixOf(node.Left, text, digit, length);
            else if (result > 0)
                return LongestPrefixOf(node.Right, text, digit, length);
            else
            {
                if (!node.Value.IsNullOrDefault())
                    length = digit + 1;
                return LongestPrefixOf(node.Mid, text, digit + 1, length);
            }
        }
    }

    //Functions for certificate
    public partial class TST<V> : ICertificate
    {
        public void Certificate()
        {
            RecrusiveCert(_root);
        }

        protected virtual void RecrusiveCert(TSTNode<V> node)
        {
            if (node == null)
                return;
            if (node.Left != null && node.Left.C.CompareTo(node.C) >= 0)
                throw new Exception("left greater than or equal to parent");
            else if (node.Right != null && node.Right.C.CompareTo(node.C) <= 0)
                throw new Exception("right less than or equal to parent");

            RecrusiveCert(node.Left);
            RecrusiveCert(node.Mid);
            RecrusiveCert(node.Right);
        }
    }
}
