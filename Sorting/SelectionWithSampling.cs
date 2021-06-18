using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class SelectionWithSampling
    {
        private int[] _source;
        private int _sampleIndex;
        private InsertionSort _sortClient = new InsertionSort();

        public int FindXth(int[] source, int k)
        {
            _source = source;
            _sampleIndex = (int)((float)k / source.Length * 5);

            int lo = 0, hi = _source.Length - 1;
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
            //Sampling
            if (hi - lo >= 20)
            {
                _sortClient.Sort(_source, lo, lo + 4);
                Exchange(lo, lo + _sampleIndex);
            }

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
