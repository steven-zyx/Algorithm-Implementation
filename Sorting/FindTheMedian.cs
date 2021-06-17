using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class FindTheMedian
    {
        private int[] _source;

        public int FindMedian(int[] source)
        {
            _source = source;
            int lo = 0, hi = _source.Length - 1;
            int k = source.Length / 2;

            while (true)
            {
                int mid = Partition(lo, hi);
                if (mid == k)
                    return _source[k];
                else if (mid > k)
                    hi = mid - 1;
                else
                    lo = mid + 1;
            }
        }

        private int Partition(int lo, int hi)
        {
            int left = lo;
            int right = hi + 1;
            int pivot = _source[lo];

            while (true)
            {
                while (_source[++left] < pivot)
                    if (left >= hi)
                        break;
                while (_source[--right] > pivot) { }

                if (left >= right)
                    break;
                Exchange(left, right);
            }
            Exchange(lo, right);
            return right;
        }

        private void Exchange(int a, int b)
        {
            int temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }
    }
}
