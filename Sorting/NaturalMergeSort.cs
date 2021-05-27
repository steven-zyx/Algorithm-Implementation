using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class NaturalMergeSort
    {
        private int[] _aux;

        public void Sort(int[] source)
        {
            _aux = new int[source.Length];


            int mergeCount;
            int lo;
            int mid;
            int hi;
            do
            {
                mergeCount = 0;
                lo = 0;
                while (lo < source.Length)
                {
                    mergeCount++;

                    mid = FindSortedSequence(source, lo);
                    if (mid + 1 >= source.Length)
                    {
                        break;
                    }
                    else
                    {
                        hi = FindSortedSequence(source, mid + 1);
                        Merge(source, lo, mid, hi);
                        lo = hi + 1;
                    }
                }
            } while (mergeCount > 1);
        }


        private int FindSortedSequence(int[] source, int start)
        {
            int i = start + 1;
            while (i < source.Length)
            {
                if (source[i] < source[i - 1])
                    break;
                i++;
            }
            return i - 1;
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
