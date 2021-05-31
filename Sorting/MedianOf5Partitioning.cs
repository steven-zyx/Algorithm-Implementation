using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MedianOf5Partitioning
    {
        private int[] _source;
        private int[] _temp5 = new int[5];
        private InsertionSort _ISClient = new InsertionSort();

        public void Sort(int[] source)
        {
            _source = source;
            Sort(0, source.Length - 1);
        }

        private void Sort(int lo, int hi)
        {
            if (hi <= lo + 15)
            {
                _ISClient.Sort(_source, lo, hi);
                return;
            }
            int mid = Partition(lo, hi);
            Sort(lo, mid - 1);
            Sort(mid + 1, hi);
        }

        private int Partition(int lo, int hi)
        {
            _temp5[0] = _source[lo];
            _temp5[1] = _source[lo + 1];
            _temp5[2] = _source[lo + 2];
            _temp5[3] = _source[hi - 1];
            _temp5[4] = _source[hi];
            _ISClient.Sort(_temp5);
            _source[lo] = _temp5[0];
            _source[lo + 1] = _temp5[1];
            _source[lo + 2] = _temp5[2];
            _source[hi - 1] = _temp5[3];
            _source[hi] = _temp5[4];

            int le = lo + 2;
            int ge = hi - 1;
            int s = _source[lo + 2];
            while (true)
            {
                while (_source[++le] < s) { }
                while (_source[--ge] > s) { }
                if (le > ge) break;
                Exchange(le, ge);
            }
            Exchange(lo + 2, ge);
            return ge;
        }

        private void Exchange(int i, int j)
        {
            int item = _source[i];
            _source[i] = _source[j];
            _source[j] = item;
        }
    }
}
