using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class BestCase
    {
        private bool _isBest;
        private Queue<int> midNumbers = new Queue<int>();


        public void Generate(int[] input)
        {
            int hi = input.Length - 1;
            Sort(input, 0, hi);
            Reverse(input, 0, hi);
        }

        private void Reverse(int[] source, int lo, int hi)
        {
            if (lo >= hi)
                return;
            int mid = lo + (hi - lo) / 2;
            Reverse(source, lo, mid - 1);
            Reverse(source, mid + 1, hi);
            Exchange(source, lo, mid);
        }

        public bool SortAndValidate(int[] source)
        {
            _isBest = true;
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


