using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public abstract class RedBlackTree<K, V> : BST<K, V> where K : IComparable
    {
        protected const bool RED = true;
        protected const bool BLACK = false;

        protected TreeNode_C<K, V> Root => _root as TreeNode_C<K, V>;

        protected bool IsRed(TreeNode_C<K, V> x)
        {
            if (x == null)
                return false;

            return x.Color == RED;

        }

        protected bool IsBlack(TreeNode_C<K, V> x)
        {
            if (x == null)
                return true;

            return x.Color == BLACK;
        }
    }
}
