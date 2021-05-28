using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class _3WayMergeSort
    {
        private int[] _aux;
        private int[] _source;

        public void Sort(int[] source)
        {
            _aux = new int[source.Length];
            _source = source;
            Sort(0, source.Length - 1);
        }

        private void Sort(int lo, int hi)
        {
            if (lo >= hi) return;
            int mid1 = lo + (hi - lo) / 3;
            int mid2 = lo + (hi - lo) * 2 / 3;
            Sort(lo, mid1);
            Sort(mid1 + 1, mid2);
            Sort(mid2 + 1, hi);
            Merge(lo, mid1, mid2, hi);
        }

        private void Merge(int lo, int mid, int hi)
        {
            int leftIndex = lo;
            int rightIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
                _aux[i] = _source[i];

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid + 1) _source[i] = _aux[rightIndex++];
                else if (rightIndex >= hi + 1) _source[i] = _aux[leftIndex++];
                else if (_aux[leftIndex] < _aux[rightIndex]) _source[i] = _aux[leftIndex++];
                else _source[i] = _aux[rightIndex++];
            }
        }

        private void Merge(int lo, int mid1, int mid2, int hi)
        {
            int leftIndex = lo;
            int midIndex = mid1 + 1;
            int rightIndex = mid2 + 1;

            for (int i = lo; i <= hi; i++)
                _aux[i] = _source[i];

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid1 + 1)   //left exhausted
                {
                    if (midIndex >= mid2 + 1) Set(i, rightIndex++);
                    else if (rightIndex >= hi + 1) Set(i, midIndex++);
                    else if (_aux[midIndex] < _aux[rightIndex]) Set(i, midIndex++);
                    else Set(i, rightIndex++);
                }
                else if (midIndex >= mid2 + 1)    //mid exhausted
                {
                    if (leftIndex >= mid1 + 1) Set(i, rightIndex++);
                    else if (rightIndex >= hi + 1) Set(i, leftIndex++);
                    else if (_aux[leftIndex] < _aux[rightIndex]) Set(i, leftIndex++);
                    else Set(i, rightIndex++);
                }
                else if (rightIndex >= hi + 1)      //right exhausted
                {
                    if (leftIndex >= mid1 + 1) Set(i, midIndex++);
                    else if (midIndex >= mid2 + 1) Set(i, leftIndex++);
                    else if (_aux[leftIndex] < _aux[midIndex]) Set(i, leftIndex++);
                    else Set(i, midIndex++);
                }
                else if (_aux[leftIndex] < _aux[midIndex] && _aux[leftIndex] < _aux[rightIndex]) Set(i, leftIndex++);    //left
                else if (_aux[midIndex] < _aux[leftIndex] && _aux[midIndex] < _aux[rightIndex]) Set(i, midIndex++);   //mid
                else Set(i, rightIndex++);   //right
            }
        }

        private void Set(int sourceIndex, int auxIndex)
        {
            _source[sourceIndex] = _aux[auxIndex];
        }
    }
}
