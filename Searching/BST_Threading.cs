using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Searching
{
    public class BST_Threading<K, V> : BST<K, V> where K : IComparable
    {
        protected TreeNode_T<K, V> Root_T
        {
            get => _root as TreeNode_T<K, V>;
            set => _root = value;
        }
    }
}
