using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TSTNode<V>
    {
        public char C { get; set; }

        public TSTNode<V> Left { get; set; }

        public TSTNode<V> Mid { get; set; }

        public TSTNode<V> Right { get; set; }

        public V Value { get; set; }
    }
}
