using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class QuickSort<T> where T : IComparable
    {
        private T[] _source;

        public void Sort(T[] source)
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
            T midE = _source[lo];

            while (true)
            {
                while (_source[++leftIndex].CompareTo(midE) < 0)
                    if (leftIndex >= hi)
                        break;
                while (_source[--rightIndex].CompareTo(midE) > 0)
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
            T item = _source[i];
            _source[i] = _source[j];
            _source[j] = item;
        }
    }
}
