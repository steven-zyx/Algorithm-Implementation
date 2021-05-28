using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class QuickSort
    {
        private int[] _source;

        public void Sort(int[] source)
        {
            _source = source;
            Sort(0, source.Length - 1);
        }

        private void Sort(int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = Partition(lo, hi);
            Sort(lo, mid - 1);
            Sort(mid + 1, hi);
        }

        private int Partition(int lo, int hi)
        {
            int leftIndex = lo;
            int rightIndex = hi + 1;
            int midE = _source[lo];

            while (true)
            {
                while (_source[++leftIndex] <= midE)
                    if (leftIndex >= hi)
                        break;
                while (_source[--rightIndex] >= midE)
                    if (rightIndex <= lo)
                        break;
                if (leftIndex >= rightIndex)
                    break;
                Exchange(leftIndex, rightIndex);
            }

            Exchange(lo, rightIndex);
            return rightIndex;
        }

        private void Exchange(int i, int j)
        {
            int item = _source[i];
            _source[i] = _source[j];
            _source[j] = item;
        }
    }
}
