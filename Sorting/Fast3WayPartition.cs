using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class Fast3WayPartition
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

            int sample = _source[lo];
            int le = lo + 1, lt = lo + 1;
            int re = hi, gt = hi;


            while (true)
            {
                while (lt < re + 1 && _source[lt] <= sample)
                {
                    if (_source[lt] == sample)
                        Exchange(le++, lt);
                    lt++;
                }
                while (gt > le - 1 && _source[gt] >= sample)
                {
                    if (_source[gt] == sample)
                        Exchange(re--, gt);
                    gt--;
                }
                if (gt < lt)
                    break;
                Exchange(lt++, gt--);
            }

            if (lt == le && lt == hi + 1)
                return;

            int leftEnd = lo;
            while (leftEnd < le && gt >= le)
                Exchange(gt--, leftEnd++);
            int rightEnd = hi;
            while (rightEnd > re && lt <= re)
                Exchange(lt++, rightEnd--);

            Sort(lo, gt);
            Sort(lt, hi);
        }

        private void Exchange(int l, int r)
        {
            int temp = _source[l];
            _source[l] = _source[r];
            _source[r] = temp;
        }
    }
}
