using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class KendallTauDistance
    {
        private int[] _aux;
        private long _linearithmicInv = 0;
        private IComparer<int> _comparer;

        public long CalculateByMergeSort(int[] source, int[] identityPermutation)
        {
            _comparer = new KendallTauComparer(identityPermutation);
            _aux = new int[source.Length];
            MergeSort(source, 0, source.Length - 1);
            return _linearithmicInv;
        }

        private void MergeSort(int[] source, int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = lo + (hi - lo) / 2;
            MergeSort(source, lo, mid);
            MergeSort(source, mid + 1, hi);
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
                else if (LessThanOrEqualTo(_aux[leftIndex], _aux[rightIndex])) source[i] = _aux[leftIndex++];
                else
                {
                    source[i] = _aux[rightIndex++];
                    _linearithmicInv += (mid + 1 - leftIndex);
                }
            }
        }

        private bool LessThanOrEqualTo(int left, int right)
        {
            return _comparer.Compare(left, right) <= 0;
        }

        private void Exchange(int[] source, int i, int j)
        {
            int item = source[i];
            source[i] = source[j];
            source[j] = item;
        }
    }

    public class KendallTauComparer : IComparer<int>
    {
        private Dictionary<int, int> _order;

        public KendallTauComparer(int[] identityPermutation)
        {
            _order = new Dictionary<int, int>();
            for (int i = 0; i < identityPermutation.Length; i++)
            {
                _order[identityPermutation[i]] = i;
            };
        }

        public int Compare(int x, int y)
        {
            return _order[x] - _order[y];
        }
    }
}
