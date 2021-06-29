using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_Certificate<K, V> : BST<K, V> where K : IComparable
    {
        protected bool IsBinaryTree(TreeNode<K, V> x)
        {
            if (x == null)
                return true;

            int n = Size(x.Left) + Size(x.Right) + 1;
            if (x.N == n)
                return IsBinaryTree(x.Left) && IsBinaryTree(x.Right);
            else
                return false;
        }

        protected bool IsOrdered(TreeNode<K, V> x, K min, K max)
        {
            if (x == null)
                return true;
            if (x.Key.CompareTo(min) >= 0 && x.Key.CompareTo(max) <= 0)
            {
                if (x.Left == null || x.Key.CompareTo(x.Left.Key) > 0)
                {
                    if (x.Right == null || x.Key.CompareTo(x.Right.Key) < 0)
                        return IsOrdered(x.Left, min, max) && IsOrdered(x.Right, min, max);
                }
            }
            return false;
        }

        protected bool HasNoDuplicates()
        {
            IEnumerator<K> iterator = Keys().GetEnumerator();
            if (iterator.MoveNext())
            {
                K previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    if (iterator.Current.CompareTo(previous) <= 0)
                        return false;
                    else
                        previous = iterator.Current;
                }
            }
            return true;
        }

        protected bool SelectRankCheck()
        {
            if (_root == null)
                return true;

            for (int i = 0; i < _root.N; i++)
                if (i != Rank(Select(i)))
                    return false;
            foreach (K key in Keys())
                if (Select(Rank(key)).CompareTo(key) != 0)
                    return false;
            return true;
        }

        protected bool IsBST(TreeNode<K, V> x) => IsBinaryTree(x) && IsOrdered(x, Min(), Max()) && HasNoDuplicates() && SelectRankCheck();

        public override void Put(K key, V value)
        {
            base.Put(key, value);
            if (!IsBST(_root))
                throw new Exception("Inconsistant binary search tree");
        }

        public override bool Delete(K key)
        {
            bool result = base.Delete(key);
            if (!IsBST(_root))
                throw new Exception("Inconsistant binary search tree");
            return result;
        }

        public override void DeleteMin()
        {
            base.DeleteMin();
            if (!IsBST(_root))
                throw new Exception("Inconsistant binary search tree");
        }

        public override void DeleteMax()
        {
            base.DeleteMax();
            if (!IsBST(_root))
                throw new Exception("Inconsistant binary search tree");
        }
    }
}
