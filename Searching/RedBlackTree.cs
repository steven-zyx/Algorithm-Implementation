﻿using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //Basic operation
    public abstract partial class RedBlackTree<K, V> : BST<K, V> where K : IComparable
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

    //For certification
    public abstract partial class RedBlackTree<K, V>
    {
        protected void SingleRedLink(TreeNode_C<K, V> h)
        {
            if (h == null) return;

            if (IsRed(h) && IsRed(h.Left_C))
                throw new Exception("A node connected with 2 red links");

            SingleRedLink(h.Left_C);
            SingleRedLink(h.Right_C);
        }
    }
}
