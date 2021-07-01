using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_Threading<K, V> : BST<K, V> where K : IComparable
    {
        public override void Put(K key, V value)
        {
            //check if the first element
            TreeNode_T<K, V> node = new TreeNode_T<K, V>(key, value, 1);
            if (_root == null)
            {
                _root = node;
                return;
            }

            //One way go down the tree, put the route in the stack
            Stack<TreeNode<K, V>> route = new Stack<TreeNode<K, V>>();
            TreeNode<K, V> currentN = _root;
            while (currentN != null)
            {
                route.Push(currentN);
                int c = key.CompareTo(currentN.Key);
                if (c < 0)
                    currentN = currentN.Left;
                else if (c > 0)
                    currentN = currentN.Right;
                else
                    break;
            }

            //Attach the element, maintain the related Pred and Succ properties;
            TreeNode_T<K, V> last = route.Peek() as TreeNode_T<K, V>;
            int c2 = key.CompareTo(last.Key);
            if (c2 < 0)
            {
                last.Left = node;
                SetPredSucc(node, last.Pred, last);
            }
            else if (c2 > 0)
            {
                last.Right = node;
                SetPredSucc(node, last, last.Succ);
            }
            else
            {
                last.Value = value;
                return;
            }

            //if element added, go one way up the tree by the route. Update N, set root
            while (route.Count > 1)
                route.Pop().N++;
            _root = route.Pop();
            _root.N++;
        }

        protected void SetPredSucc(TreeNode_T<K, V> node, TreeNode_T<K, V> pred, TreeNode_T<K, V> succ)
        {
            node.Pred = pred;
            node.Succ = succ;
            if (pred != null)
                pred.Succ = node;
            if (succ != null)
                succ.Pred = node;
        }

        public K Next(K key)
        {
            TreeNode_T<K, V> node = FindNode(key);
            if (node == null || node.Succ == null)
                return default(K);
            else
                return node.Succ.Key;
        }

        public K Prev(K key)
        {
            TreeNode_T<K, V> node = FindNode(key);
            if (node == null || node.Pred == null)
                return default(K);
            else
                return node.Pred.Key;
        }

        protected TreeNode_T<K, V> FindNode(K key)
        {
            TreeNode<K, V> currentN = _root;
            while (currentN != null)
            {
                int c = key.CompareTo(currentN.Key);
                if (c < 0)
                    currentN = currentN.Left;
                else if (c > 0)
                    currentN = currentN.Right;
                else
                    return currentN as TreeNode_T<K, V>;
            }
            return null;
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
                DeletePredSucc(x);

                if (x.Right == null)
                    return x.Left;
                else if (x.Left == null)
                    return x.Right;
                else
                {
                    TreeNode<K, V> t = x;
                    x = Min(x.Right);
                    x.Right = DeleteMin(t.Right);
                    x.Left = t.Left;
                }
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected override TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            if (x.Left == null)
            {
                DeletePredSucc(x);
                return x.Right;
            }
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected override TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            if (x.Right == null)
            {
                DeletePredSucc(x);
                return x.Left;
            }
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        protected void DeletePredSucc(TreeNode<K, V> node)
        {
            TreeNode_T<K, V> nodeT = node as TreeNode_T<K, V>;
            if (nodeT.Pred != null)
                nodeT.Pred.Succ = nodeT.Succ;
            if (nodeT.Succ != null)
                nodeT.Succ.Pred = nodeT.Pred;
        }
    }
}
