using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Linq;

namespace Searching
{
    public class BST_Iterator<K, V> : BST<K, V> where K : IComparable
    {
        public override IEnumerable<K> Keys()
        {
            Stack<TreeNode<K, V>> route = new Stack<TreeNode<K, V>>();
            route.Push(_root);

            while (route.Peek().Left != null)
                route.Push(route.Peek().Left);

            while (route.Count > 0)
            {
                TreeNode<K, V> node = route.Pop();
                yield return node.Key;

                if (node.Right != null)
                {
                    route.Push(node.Right);
                    while (route.Peek().Left != null)
                        route.Push(route.Peek().Left);
                }
            }
        }

        public override IEnumerable<K> Keys(K lo, K hi)
        {
            Stack<TreeNode<K, V>> route = new Stack<TreeNode<K, V>>();

            TreeNode<K, V> startN = Ceiling(_root, lo);
            TreeNode<K, V> currentN = _root;
            while (true)
            {
                route.Push(currentN);
                int c = startN.Key.CompareTo(currentN.Key);
                if (c < 0) currentN = currentN.Left;
                else if (c > 0) currentN = currentN.Right;
                else break;
            }

            while (route.Count > 0)
            {
                TreeNode<K, V> node = route.Pop();
                if (node.Key.CompareTo(hi) <= 0)
                    yield return node.Key;
                else
                    yield break;

                if (node.Right != null)
                {
                    route.Push(node.Right);
                    while (route.Peek().Left != null)
                        route.Push(route.Peek().Left);
                }
            }
        }

        public override int Size(K lo, K hi) => Keys(lo, hi).Count();
    }
}
