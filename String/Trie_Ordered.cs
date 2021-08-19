using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Searching;

namespace String
{
    public partial class Trie_Ordered<V> : TrieST<V>
    {
        protected TrieNode_N<V> Root_N => _root as TrieNode_N<V>;

        protected bool _isToAdd;
        protected bool _isToDelete;

        public Trie_Ordered(Alphabet a) : base(a) { }

        protected override TrieNode<V> Put(TrieNode<V> node, string key, V value, int digit)
        {
            if (node == null)
                node = new TrieNode_N<V>(_a.R);
            if (digit == key.Length)
            {
                _isToAdd = node.Value.Equals(default(V));
                if (_isToAdd)
                    (node as TrieNode_N<int>).N++;
                node.Value = value;
                return node;
            }
            int c = _a.ToIndex(key[digit]);
            node.Next[c] = Put(node.Next[c] as TrieNode_N<V>, key, value, digit + 1);
            if (_isToAdd)
                (node as TrieNode_N<V>).N++;
            return node;
        }

        public override int Size() => Size(Root_N);

        protected override int Size(TrieNode<V> x)
        {
            TrieNode_N<V> node = x as TrieNode_N<V>;
            if (node == null)
                return 0;
            else
                return node.N;
        }

        public override bool Delete(string key)
        {
            _root = Delete(_root, key, 0);
            return _isToDelete;
        }

        protected override TrieNode<V> Delete(TrieNode<V> node, string key, int digit)
        {
            if (node == null)
            {
                _isToDelete = false;
                return null;
            }
            if (digit == key.Length)
            {
                _isToDelete = true;
                node.Value = default(V);
            }
            else
            {
                int i = _a.ToIndex(key[digit]);
                node.Next[i] = Delete(node.Next[i], key, digit + 1);
            }

            if (_isToDelete)
                (node as TrieNode_N<V>).N--;

            if (!node.Value.Equals(default(V)))
                return node;
            for (int i = 0; i < _a.R; i++)
                if (node.Next[i] != null)
                    return node;
            return null;
        }
    }

    public partial class Trie_Ordered<V> : ICertificate
    {
        public void Certificate()
        {
            RecrusiveCert(Root_N);
        }

        public void RecrusiveCert(TrieNode_N<V> node)
        {
            if (node == null)
                return;

            int size = 0;
            if (!node.Value.Equals(default(V)))
                size++;
            for (int i = 0; i < _a.R; i++)
            {
                RecrusiveCert(node.Next[i] as TrieNode_N<V>);
                size += Size(node.Next[i]);
            }

            if (node.N != size)
                throw new Exception("Incorrect N");
        }
    }
}
