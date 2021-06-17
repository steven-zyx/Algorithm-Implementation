using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TriplyNode
    {
        public TriplyNode Parent { get; set; }
        public TriplyNode leftSub { get; set; }
        public TriplyNode RightSub { get; set; }
        public int Value { get; set; }


        public TriplyNode(int item)
        {
            Value = item;
        }
    }
}
