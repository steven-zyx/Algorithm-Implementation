using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //Override common symbel table functions
    public partial class BST_23_Cache<K, V> : BST_23<K, V> where K : IComparable
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

        protected override TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;

            //Reach bottom, attach new node
            if (h == null)
            {
                _cache = new TreeNode_C<K, V>(key, value, 1, RED);
                return _cache;
            }

            //Check go left or right
            int result = key.CompareTo(h.Key);
            if (result < 0)
                h.Left = Put(h.Left, key, value);
            else if (result > 0)
                h.Right = Put(h.Right, key, value);
            else
            {
                h.Value = value;
                _cache = h;
            }

            return Balance(h);
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

        public override bool Delete(K key)
        {
            bool result = base.Delete(key);
            if (result && CacheHit(key))
                _cache = null;
            return result;
        }
    }

    //Override ordered symbol table function
    public partial class BST_23_Cache<K, V>
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

        protected override TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (h.Left == null)
            {
                if (CacheHit(h.Key))
                    _cache = null;
                return null;
            }

            if (IsBlack(h.Left_C) && IsBlack(h.Left_C.Left_C))
                h = MoveRedLeft(h);

            h.Left = DeleteMin(h.Left);
            return Balance(h);
        }

        protected override TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (IsRed(h.Left_C))
                h = RotateRight(h);
            else if (h.Right == null)
            {
                if (CacheHit(h.Key))
                    _cache = null;
                return null;
            }

            if (IsBlack(h.Right_C) && IsBlack(h.Right_C.Left_C))
                h = MoveRedRight(h);

            h.Right = DeleteMax(h.Right);
            return Balance(h);
        }
    }

    //Functions for caching
    public partial class BST_23_Cache<K, V>
    {
        protected bool CacheHit(K key) => _cache != null && _cache.Key.CompareTo(key) == 0;
    }
}
