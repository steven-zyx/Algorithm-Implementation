using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    //For overwriting BST functions
    public partial class BST_23<K, V> : RedBlackTree<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            _root = Put(_root, key, value);
            Root.Color = BLACK;
        }

        protected override TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;

            //Reach bottom, attach new node
            if (h == null)
                return new TreeNode_C<K, V>(key, value, 1, RED);

            //Check go left or right
            int result = key.CompareTo(h.Key);
            if (result < 0)
                h.Left = Put(h.Left, key, value);
            else if (result > 0)
                h.Right = Put(h.Right, key, value);
            else
                h.Value = value;

            return Balance(h);
        }

        public override void DeleteMin()
        {
            if (IsBlack(Root.Left_C) && IsBlack(Root.Right_C))
                Root.Color = RED;
            _root = DeleteMin(Root);
            if (Root != null)
                Root.Color = BLACK;
        }

        protected override TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (h.Left == null)
                return null;

            if (IsBlack(h.Left_C) && IsBlack(h.Left_C.Left_C))
                h = MoveRedLeft(h);

            h.Left = DeleteMin(h.Left);
            return Balance(h);
        }

        public override void DeleteMax()
        {
            if (IsBlack(Root.Left_C) && IsBlack(Root.Right_C))
                Root.Color = RED;
            _root = DeleteMax(Root);
            if (Root != null)
                Root.Color = BLACK;
        }

        protected override TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (IsRed(h.Left_C))
                h = RotateRight(h);
            else if (h.Right == null)
                return null;

            if (IsBlack(h.Right_C) && IsBlack(h.Right_C.Left_C))
                h = MoveRedRight(h);

            h.Right = DeleteMax(h.Right);
            return Balance(h);
        }

        public override bool Delete(K key)
        {
            if (_root == null)
                return false;
            int oldN = _root.N;

            if (IsBlack(Root.Left_C) && IsBlack(Root.Right_C))
                Root.Color = RED;
            _root = Delete(Root, key);
            if (Root != null)
            {
                Root.Color = BLACK;
                return Root.N < oldN;
            }
            else
                return true;
        }

        protected override TreeNode<K, V> Delete(TreeNode<K, V> x, K key)
        {
            TreeNode_C<K, V> h = x as TreeNode_C<K, V>;
            if (h == null)
                return null;

            if (key.CompareTo(h.Key) < 0)
            {
                if (h.Left_C != null && IsBlack(h.Left_C) && IsBlack(h.Left_C.Left_C))
                    h = MoveRedLeft(h);
                h.Left = Delete(h.Left, key);
            }
            else
            {
                if (IsRed(h.Left_C))
                    h = RotateRight(h);
                else if (key.CompareTo(h.Key) == 0 && h.Right_C == null)
                    return null;
                if (h.Right_C != null && IsBlack(h.Right_C) && IsBlack(h.Right_C.Left_C))
                    h = MoveRedRight(h);
                if (key.CompareTo(h.Key) == 0)
                {
                    var succ = Min(h.Right);
                    h.Key = succ.Key;
                    h.Value = succ.Value;
                    h.Right = DeleteMin(h.Right);
                }
                else
                    h.Right = Delete(h.Right, key);
            }
            return Balance(h);
        }
    }

    //For red-black tree functions
    public partial class BST_23<K, V>
    {
        protected void FlipColor(TreeNode_C<K, V> h)
        {
            h.Left_C.Color = !h.Left_C.Color;
            h.Right_C.Color = !h.Right_C.Color;
            h.Color = !h.Color;
        }

        protected TreeNode_C<K, V> RotateLeft(TreeNode_C<K, V> h)
        {
            TreeNode_C<K, V> x = h.Right_C;
            h.Right = x.Left;
            x.Left = h;
            x.N = h.N;
            h.N = Size(h.Left) + Size(h.Right) + 1;
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }

        protected TreeNode_C<K, V> RotateRight(TreeNode_C<K, V> h)
        {
            TreeNode_C<K, V> x = h.Left_C;
            h.Left = x.Right;
            x.Right = h;
            x.N = h.N;
            h.N = Size(h.Left) + Size(h.Right) + 1;
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }

        protected virtual TreeNode_C<K, V> MoveRedLeft(TreeNode_C<K, V> h)
        {
            FlipColor(h);
            if (IsRed(h.Right_C.Left_C))
            {
                h.Right = RotateRight(h.Right_C);
                h = RotateLeft(h);
                FlipColor(h);
            }
            return h;
        }

        protected virtual TreeNode_C<K, V> MoveRedRight(TreeNode_C<K, V> h)
        {
            FlipColor(h);
            if (IsRed(h.Left_C.Left_C))
            {
                h = RotateRight(h);
                FlipColor(h);
            }
            return h;
        }

        protected virtual TreeNode_C<K, V> Balance(TreeNode_C<K, V> h)
        {
            //Update N for every node in the route
            h.N = Size(h.Left) + Size(h.Right) + 1;

            //For a 3 node, put the smaller node to lower-left
            //For a 4 node, put the larger node to lower-right, the middle to the top and attach to the parent.
            if (IsRed(h.Right_C) && IsBlack(h.Left_C))
                h = RotateLeft(h);
            if (IsRed(h.Left_C) && IsRed(h.Left_C.Left_C))
                h = RotateRight(h);
            if (IsRed(h.Left_C) && IsRed(h.Right_C))
                FlipColor(h);

            return h;
        }
    }

    //For Certification
    public partial class BST_23<K, V>
    {
        private int _blackLevel;

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

            if (IsRed(h.Right_C))
                IsBalanced(h.Right_C, level);
            else
                IsBalanced(h.Right_C, level + 1);
        }

        private void CheckBlackLevel(int level)
        {
            if (_blackLevel == -1)
                _blackLevel = level;
            else if (level != _blackLevel)
                throw new Exception("Different black level");
        }

        public override void Certificate()
        {
            base.Certificate();
            SingleRedLink(Root);
            RedLinkLeanLeft(Root);
            IsBalanced(Root);
        }
    }
}
