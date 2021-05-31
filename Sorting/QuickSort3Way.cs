using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class QuickSort3Way
    {
        private int[] _source;

        public void Sort(int[] source)
        {
            _source = source;
            Sort(0, source.Length - 1);
        }
            
        private void Sort(int lo, int hi)
        {
            if (hi <= lo) return;

            int lt = lo;
            int i = lo + 1;
            int gt = hi;
            int v = _source[lo];

            while (i <= gt)
            {
                int result = _source[i] - v;
                if (result < 0) Exchange(lt++, i++);
                else if (result > 0) Exchange(gt--, i);
                else i++;
            }

            Sort(lo, lt - 1);
            Sort(gt + 1, hi);
        }

        private void Exchange(int l, int r)
        {
            int temp = _source[l];
            _source[l] = _source[r];
            _source[r] = temp;
        }
    }
}
