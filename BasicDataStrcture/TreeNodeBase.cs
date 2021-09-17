using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TreeNodeBase<K, V> where K : IComparable<V>
    {
        public int N { get; set; }
    }
}
