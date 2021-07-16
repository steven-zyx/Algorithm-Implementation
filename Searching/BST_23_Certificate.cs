using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //Add certification for Put and Delete functions
    public partial class BST_23_Certificate<K, V> : BST_23<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            base.Put(key, value);
            IsRedBlackBST();
        }

        public override bool Delete(K key)
        {
            bool result = base.Delete(key);
            return result;
        }

        public override void DeleteMin()
        {
            base.DeleteMin();
            IsRedBlackBST();
        }

        public override void DeleteMax()
        {
            base.DeleteMax();
            IsRedBlackBST();
        }
    }

    //Functions for certificating
    public partial class BST_23_Certificate<K, V>
    {
        private int _blackLevel;

        protected void SingleRedLink(TreeNode_C<K, V> h)
        {
            if (h == null) return;

            if (IsRed(h) && IsRed(h.Left_C))
                throw new Exception("A node connected with 2 red links");

            SingleRedLink(h.Left_C);
            SingleRedLink(h.Right_C);
        }

        protected void RedLinkLeanLeft(TreeNode_C<K, V> h)
        {
            if (h == null) return;

            if (IsRed(h.Right_C))
                throw new Exception("A right-leaing red link");

            RedLinkLeanLeft(h.Left_C);
            RedLinkLeanLeft(h.Right_C);
        }

        protected void IsBalanced(TreeNode_C<K, V> h)
        {
            _blackLevel = -1;
            IsBalanced(h, 0);
        }

        private void IsBalanced(TreeNode_C<K, V> h, int level)
        {
            if (h == null)
            {
                CheckBlackLevel(level);
                return;
            }

            if (IsRed(h.Left_C))
                IsBalanced(h.Left_C, level);
            else
                IsBalanced(h.Left_C, level + 1);
            IsBalanced(h.Right_C, level + 1);
        }

        private void CheckBlackLevel(int level)
        {
            if (_blackLevel == -1)
                _blackLevel = level;
            else if (level != _blackLevel)
                throw new Exception("Different black level");
        }

        protected void IsRedBlackBST()
        {
            Certificate();
            SingleRedLink(Root);
            RedLinkLeanLeft(Root);
            IsBalanced(Root);
        }
    }
}
