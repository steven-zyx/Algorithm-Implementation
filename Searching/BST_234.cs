﻿using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public partial class BST_234<K, V> : BST_23<K, V> where K : IComparable
    {
        protected override TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;

            if (h == null)
                return new TreeNode_C<K, V>(key, value, 1, RED);

            if (IsRed(h.Left_C) && IsRed(h.Right_C))
                FlipColor(h);

            int result = key.CompareTo(h.Key);
            if (result < 0)
                h.Left = Put(h.Left, key, value);
            else if (result > 0)
                h.Right = Put(h.Right, key, value);
            else
                h.Value = value;

            return Balance(h);
        }

        protected override TreeNode_C<K, V> Balance(TreeNode_C<K, V> h)
        {
            h.N = Size(h.Left) + Size(h.Right) + 1;

            if (IsRed(h.Right_C) && IsBlack(h.Left_C))
                h = RotateLeft(h);
            if (IsRed(h.Left_C) && IsRed(h.Left_C.Left_C))
                h = RotateRight(h);

            return h;
        }
    }

    public partial class BST_234<K, V>
    {
        protected override TreeNode_C<K, V> MoveRedLeft(TreeNode_C<K, V> h)
        {
            FlipColor(h);
            if (IsRed(h.Right_C.Left_C))
            {
                h.Right = RotateRight(h.Right_C);
                h = RotateLeft(h);
                if (IsRed(h.Right_C.Right_C))
                    h.Right = RotateLeft(h.Right_C);
                FlipColor(h);
            }
            return h;
        }

        protected override TreeNode_C<K, V> MoveRedRight(TreeNode_C<K, V> h)
        {
            FlipColor(h);
            if (IsRed(h.Left_C.Left_C))
            {
                if (IsRed(h.Left_C.Right_C))
                    h.Left = RotateLeft(h.Left_C);
                h = RotateRight(h);
                FlipColor(h);
            }
            return h;
        }
    }
}
