using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class BestCase
    {
        private bool _isBest = true;

        public int[] Generate(int[] input)
        {
            return null;
        }





        public bool SortAndValidate(int[] source)
        {
            Sort(source, 0, source.Length - 1);
            return _isBest;
        }

        private void Sort(int[] _source, int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = Partition(_source, lo, hi);
            Validate(lo, mid, hi);
            Sort(_source, lo, mid - 1);
            Sort(_source, mid + 1, hi);
        }

        private int Partition(int[] _source, int lo, int hi)
        {
            int leftIndex = lo;
            int rightIndex = hi + 1;
            int midE = _source[lo];

            while (true)
            {
                while (_source[++leftIndex] < midE)
                    if (leftIndex >= hi)
                        break;
                while (midE < _source[--rightIndex])
                    if (rightIndex <= lo)
                        break;
                if (leftIndex >= rightIndex)
                    break;
                Exchange(_source, leftIndex, rightIndex);
            }

            Exchange(_source, lo, rightIndex);
            return rightIndex;
        }

        private void Exchange(int[] _source, int i, int j)
        {
            int item = _source[i];
            _source[i] = _source[j];
            _source[j] = item;
        }

        private void Validate(int lo, int mid, int hi)
        {
            int leftCount = mid - lo;
            int rightCount = hi - mid;
            if (Math.Abs(leftCount - rightCount) > 2)
                _isBest = false;
        }
    }
}


