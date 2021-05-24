using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MergeSort
    {
        private int[] _aux;

        public void Sort(int[] source)
        {
            _aux = new int[source.Length];
            Sort(source, 0, source.Length - 1);
        }

        private void Sort(int[] source, int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = lo + (hi - lo) / 2;
            Sort(source, lo, mid);
            Sort(source, mid + 1, hi);
            Merge(source, lo, mid, hi);
        }

        private void Merge(int[] source, int lo, int mid, int hi)
        {
            int leftIndex = lo;
            int rightIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
                _aux[i] = source[i];

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid + 1) source[i] = _aux[rightIndex++];
                else if (rightIndex >= hi + 1) source[i] = _aux[leftIndex++];
                else if (_aux[leftIndex] < _aux[rightIndex]) source[i] = _aux[leftIndex++];
                else source[i] = _aux[rightIndex++];
            }
        }
    }
}
