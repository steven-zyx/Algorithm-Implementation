using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MedianOf3Partitioning
    {
        private int[] _source;
        private int[] _temp3 = new int[3];
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
            _temp3[0] = _source[lo];
            _temp3[1] = _source[lo + 1];
            _temp3[2] = _source[hi];
            _ISClient.Sort(_temp3);
            _source[lo] = _temp3[0];
            _source[lo + 1] = _temp3[1];
            _source[hi] = _temp3[2];

            int le = lo + 1;
            int ge = hi;
            int s = _source[lo + 1];
            while (true)
            {
                while (_source[++le] < s) { }
                while (_source[--ge] > s) { }
                if (le > ge) break;
                Exchange(le, ge);
            }
            Exchange(lo + 1, ge);
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
