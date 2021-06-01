using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class SampleSort
    {
        private int[] _source;
        private int[] _sample;
        private QuickSort _client = new QuickSort();

        public void Sort(int[] source, int[] sample)
        {
            _source = source;
            _client.Sort(sample);
            _sample = sample;
            Sort(0, source.Length - 1, 0, sample.Length - 1);
        }

        private void Sort(int lo, int hi, int sL, int sH)
        {
            if (hi <= lo) return;

            if (sH < sL)
            {
                Sort(lo, hi);
            }
            else
            {
                int sM = sL + (sH - sL) / 2;
                int mid = Partition(lo, hi, _sample[sM]);
                Sort(lo, mid, sL, sM - 1);
                Sort(mid + 1, hi, sM + 1, sH);
            }
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
                while (_source[++leftIndex] < midE)
                    if (leftIndex >= hi)
                        break;
                while (midE < _source[--rightIndex])
                    if (rightIndex <= lo)
                        break;
                if (leftIndex >= rightIndex)
                    break;
                Exchange(leftIndex, rightIndex);
            }

            Exchange(lo, rightIndex);
            return rightIndex;
        }

        private int Partition(int lo, int hi, int pivot)
        {
            int leftIndex = lo - 1;
            int rightIndex = hi + 1;

            while (true)
            {
                while (_source[++leftIndex] < pivot)
                    if (leftIndex >= hi)
                        break;
                while (pivot < _source[--rightIndex])
                    if (rightIndex <= lo)
                        break;
                if (leftIndex >= rightIndex)
                    break;
                Exchange(leftIndex, rightIndex);
            }

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
