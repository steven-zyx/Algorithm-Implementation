using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_23_WithoutBalance<K, V> : RedBlackTree<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            if (_root == null)
                _root = new TreeNode_C<K, V>(key, value, 1, BLACK);
            else
                _root = Put(Root, key, value, null);
            Root.Color = BLACK;
        }

        protected TreeNode<K, V> Put(TreeNode_C<K, V> h, K key, V value, TreeNode_C<K, V> parent)
        {
            if (h == null)
            {
                var newNode = new TreeNode_C<K, V>(key, value, 1, RED);
                if (IsRed(parent) || IsRed(parent.Left_C) || IsRed(parent.Right_C))
                    newNode.Color = BLACK;
                return newNode;
            }

            int result = key.CompareTo(h.Key);
            if (result < 0)
                h.Left = Put(h.Left_C, key, value, h);
            else if (result > 0)
                h.Right = Put(h.Right_C, key, value, h);
            else
                h.Value = value;

            h.N = Size(h.Left) + Size(h.Right) + 1;
            return h;
        }

        protected override TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            if (x.Left == null)
                return SetToBlack(x.Right);
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected override TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            if (x.Right == null)
                return SetToBlack(x.Left);
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected override TreeNode<K, V> Delete(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;
            int result = key.CompareTo(x.Key);
            if (result < 0)
                x.Left = Delete(x.Left, key);
            else if (result > 0)
                x.Right = Delete(x.Right, key);
            else
            {
                if (x.Right == null)
                    return SetToBlack(x.Left);
                else if (x.Left == null)
                    return SetToBlack(x.Right);
                else
                {
                    TreeNode<K, V> succ = Min(x.Right);
                    x.Key = succ.Key;
                    x.Value = succ.Value;
                    x.Right = DeleteMin(x.Right);
                }
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected TreeNode<K, V> SetToBlack(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (h != null)
                h.Color = BLACK;
            return h;
        }

        public override void Certificate()
        {
            base.Certificate();
            SingleRedLink(Root);
        }
    }
}
