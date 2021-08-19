using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TSTNode_N<V> : TSTNode<V>
    {
        public TSTNode_N(char c) : base(c) { }

        public int N { get; set; }
    }
}
