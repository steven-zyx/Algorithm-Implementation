using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TreeNode_C<K, V> : TreeNode<K, V> where K : IComparable
    {

        public bool Color { get; set; }

        public TreeNode_C<K, V> Left_C => Left as TreeNode_C<K, V>;

        public TreeNode_C<K, V> Right_C => Right as TreeNode_C<K, V>;

        public TreeNode_C(K key, V value, int n, bool color) : base(key, value, n)
        {
            Color = color;
        }
    }
}
