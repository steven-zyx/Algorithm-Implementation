﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //Common symbol table functions
    public partial class BST<K, V> : IOrderedSymbolTable<K, V> where K : IComparable
    {
        protected TreeNode<K, V> _root;

        public BST() => Init();

        public void Init()
        {
            _root = null;
        }

        public V this[K key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public V Get(K key) => Get(_root, key);

        protected V Get(TreeNode<K, V> root, K key)
        {
            if (root == null)
                return default(V);

            int result = root.Key.CompareTo(key);
            if (result < 0)
                return Get(root.Left, key);
            else if (result > 0)
                return Get(root.Right, key);
            else
                return root.Value;
        }

        public void Put(K key, V value) => Put(_root, key, value);

        public TreeNode<K, V> Put(TreeNode<K, V> root, K key, V value)
        {
            if (root == null)
                return new TreeNode<K, V>(key, value, 1);

            int result = root.Key.CompareTo(key);
            if (result < 0)
                root.Left = Put(root.Left, key, value);
            else if (result > 0)
                root.Right = Put(root.Right, key, value);
            else
                root.Value = value;

            root.N = root.N + root.N + 1;
            return root;
        }

        public bool IsEmpty => _root == null;

        public int Size() => Size(_root);

        protected int Size(TreeNode<K, V> node)
        {
            if (node == null)
                return 0;
            else
                return node.N;
        }

        public bool Contains(K key)
        {
            throw new NotImplementedException();
        }

        public bool Delete(K key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Keys()
        {
            throw new NotImplementedException();
        }
    }

    //Ordered symbol table functions
    public partial class BST<K, V>
    {
        public K Min() => Min(_root).Key;

        protected TreeNode<K, V> Min(TreeNode<K, V> x)
        {
            if (x.Left == null)
                return x;
            else
                return Min(x.Left);
        }

        public K Max() => Max(_root).Key;

        protected TreeNode<K, V> Max(TreeNode<K, V> x)
        {
            if (x.Right == null)
                return x;
            else
                return Max(x.Right);
        }

        public void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public K Floor(K key)
        {
            var node = Floor(_root, key);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Floor(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;

            int result = x.Key.CompareTo(key);
            if (result == 0)
                return x;
            else if (result < 0)
                return Floor(x.Left, key);
            else
            {
                var node = Floor(x.Right, key);
                if (node == null)
                    return x;
                else
                    return node;
            }
        }

        public K Ceiling(K key)
        {
            var node = Ceiling(_root, key);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Ceiling(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;

            int result = x.Key.CompareTo(key);
            if (result == 0)
                return x;
            else if (result > 0)
                return Ceiling(x.Right, key);
            else
            {
                var node = Ceiling(x.Left, key);
                if (node == null)
                    return x;
                else
                    return node;
            }
        }

        public IEnumerable Keys(K lo, K hi)
        {
            throw new NotImplementedException();
        }

        public int Rank(K key) => Rank(_root, key);

        protected int Rank(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return 0;

            int result = key.CompareTo(x.Key);
            if (result == 0)
                return Size(x.Left);
            else if (result < 0)
                return Rank(x.Left, key);
            else
                return Size(x.Left) + 1 + Rank(x.Right, key);
        }

        public K Select(int k)
        {
            var node = Select(_root, k);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Select(TreeNode<K, V> x, int k)
        {
            if (x == null)
                return null;

            int t = Size(x.Left);
            if (t > k)
                return Select(x.Left, k);
            else if (t < k)
                return Select(x.Right, k - t - 1);
            else
                return x;
        }

        public int Size(K lo, K hi)
        {
            throw new NotImplementedException();
        }
    }
}
