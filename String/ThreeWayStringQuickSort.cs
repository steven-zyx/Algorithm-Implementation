using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class ThreeWayStringQuickSort : ThreeWayQuickSort<string>
    {
        protected override bool Less(int a, int b, int d) => _source[a].Substring(d).CompareTo(_source[b].Substring(d)) < 0;

        protected override int ValueAt(string element, int i)
        {
            if (i >= element.Length)
                return -1;
            else
                return element[i];
        }
    }
}
