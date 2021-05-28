using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Sorting
{
    public class IndirectSort
    {
        private int[] _aux;
        private int[] _source;
        private int[] _perm;

        public int[] Sort(int[] source)
        {
            _aux = new int[source.Length];
            _source = source;
            _perm = Enumerable.Range(0, source.Length).ToArray();
            Sort(0, source.Length - 1);
            return _perm;
        }

        private void Sort(int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = lo + (hi - lo) / 2;
            Sort(lo, mid);
            Sort(mid + 1, hi);
            Merge(lo, mid, hi);
        }

        private void Merge(int lo, int mid, int hi)
        {
            int leftIndex = lo;
            int rightIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
                _aux[i] = _perm[i];

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid + 1) _perm[i] = _aux[rightIndex++];
                else if (rightIndex >= hi + 1) _perm[i] = _aux[leftIndex++];
                else if (_source[_aux[leftIndex]] < _source[_aux[rightIndex]]) _perm[i] = _aux[leftIndex++];
                else _perm[i] = _aux[rightIndex++];
            }
        }
    }

    public static class IndirectSortUtil
    {
        public static IEnumerable OrderBy(this IEnumerable<int> source)
        {
            IndirectSort client = new IndirectSort();
            int[] arraySource = source.ToArray();
            int[] perm = client.Sort(arraySource);
            foreach (int index in perm)
            {
                yield return arraySource[index];
            }
        }
    }
}
