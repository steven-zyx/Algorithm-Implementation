using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using static BasicDataStrcture.TreeNode_C<int, int>;

namespace Searching
{
    public class BST_234<K, V> : BST_23<K, V> where K : IComparable
    {
        protected override TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;

            if (h == null)
                return new TreeNode_C<K, V>(key, value, 1, RED);

            //When going down the tree, seperate a 4-node.
            //Put the smaller and larger node to lower-left and lower-right, attach the middle node to the parent.
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

        protected override TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (IsRed(h.Left_C) && IsRed(h.Right_C))
                FlipColor(h);
            return base.DeleteMax(h);
        }

        protected override TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (IsRed(h.Left_C) && IsRed(h.Right_C))
                FlipColor(h);
            return base.DeleteMin(h);
        }

        protected override TreeNode_C<K, V> Balance(TreeNode_C<K, V> h)
        {
            //Update N for every node in the route
            h.N = Size(h.Left) + Size(h.Right) + 1;

            //For a 3 node, put the smaller node to lower-left
            //For a 4 node, put the larger node to lower-right, the middle to the top and attach to the parent.
            if (IsRed(h.Right_C) && IsBlack(h.Left_C))
                h = RotateLeft(h);
            if (IsRed(h.Left_C) && IsRed(h.Left_C.Left_C))
                h = RotateRight(h);

            return h;
        }
    }
}
