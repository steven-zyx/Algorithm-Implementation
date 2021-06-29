using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //Override common symbel table functions
    public partial class BST_Cache<K, V> : BST<K, V> where K : IComparable
    {
        protected TreeNode<K, V> _cache;

        public override V Get(K key)
        {
            if (_cache != null && key.CompareTo(_cache.Key) == 0)
                return _cache.Value;


            var node = Get(_root, key);
            if (node == null)
                return default(V);
            else
            {
                _cache = node;
                return node.Value;
            }
        }

        public override void Put(K key, V value)
        {
            if (_cache != null && key.CompareTo(_cache.Key) == 0)
            {
                _cache.Value = value;
                return;
            }

            _root = Put(_root, key, value);
        }

        public override TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            if (x == null)
            {
                _cache = new TreeNode<K, V>(key, value, 1); ;
                return _cache;
            }

            int result = key.CompareTo(x.Key);
            if (result < 0)
                x.Left = Put(x.Left, key, value);
            else if (result > 0)
                x.Right = Put(x.Right, key, value);
            else
            {
                x.Value = value;
                _cache = x;
            }

            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public override bool Contains(K key)
        {
            var node = Get(_root, key);
            if (node != null)
            {
                _cache = node;
                return true;
            }
            else
                return false;
        }
    }

    //Override ordered symbol table function
    public partial class BST_Cache<K, V>
    {
        public override K Floor(K key)
        {
            var node = Floor(_root, key);
            if (node == null)
                return default(K);
            else
            {
                _cache = node;
                return node.Key;
            }
        }

        public override K Ceiling(K key)
        {
            var node = Ceiling(_root, key);
            if (node == null)
                return default(K);
            else
            {
                _cache = node;
                return node.Key;
            }
        }

        protected override int Rank(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return 0;

            int result = key.CompareTo(x.Key);
            if (result == 0)
            {
                _cache = x;
                return Size(x.Left);
            }
            else if (result < 0)
                return Rank(x.Left, key);
            else
                return Size(x.Left) + 1 + Rank(x.Right, key);
        }

        public override K Select(int k)
        {
            var node = Select(_root, k);
            if (node == null)
                return default(K);
            else
            {
                _cache = node;
                return node.Key;
            }
        }
    }
}
