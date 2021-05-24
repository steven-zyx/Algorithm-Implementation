using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class BottomUpMergeSort
    {
        private int[] _aux;

        public void Sort(int[] source)
        {
            int n = source.Length;
            _aux = new int[n];

            int size = 1;
            while (size < n)
            {
                for (int lo = 0; lo < n - size; lo = lo + size * 2)
                {
                    Merge(source, lo, lo + size - 1, Math.Min(lo + size * 2 - 1, n - 1));
                }
                size *= 2;
            }
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
