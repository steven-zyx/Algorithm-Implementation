using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class ThreeWayIntArrayQuickSort : ThreeWayQuickSort<int[]>
    {
        protected override bool Less(int a, int b, int d)
        {
            int maxLength = Math.Max(_source[a].Length, _source[b].Length);
            for (; d < maxLength; d++)
            {
                int valueA = int.MinValue;
                if (d < _source[a].Length)
                    valueA = _source[a][d];

                int valueB = int.MinValue;
                if (d < _source[b].Length)
                    valueB = _source[b][d];

                int result = valueA.CompareTo(valueB);
                if (result != 0)
                    return result < 0;
            }
            return false;
        }

        protected override int ValueAt(int[] element, int i)
        {
            if (i >= element.Length)
                return -1;
            else
                return element[i];
        }
    }
}
