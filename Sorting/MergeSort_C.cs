using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MergeSort_C<T>
    {
        private T[] _aux;
        private IComparer<T> _comparer;

        public void Sort(T[] source, IComparer<T> comparer)
        {
            _aux = new T[source.Length];
            _comparer = comparer;
            Sort(source, 0, source.Length - 1);
        }

        private void Sort(T[] source, int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = lo + (hi - lo) / 2;
            Sort(source, lo, mid);
            Sort(source, mid + 1, hi);
            Merge(source, lo, mid, hi);
        }

        private void Merge(T[] source, int lo, int mid, int hi)
        {
            int leftIndex = lo;
            int rightIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
                _aux[i] = source[i];

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid + 1) source[i] = _aux[rightIndex++];
                else if (rightIndex >= hi + 1) source[i] = _aux[leftIndex++];
                else if (_comparer.Compare(_aux[leftIndex], _aux[rightIndex]) <= 0) source[i] = _aux[leftIndex++];
                else source[i] = _aux[rightIndex++];
            }
        }
    }
}
