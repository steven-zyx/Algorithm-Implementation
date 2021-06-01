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
            int le = lo + 1;
            int lt = lo + 1;
            int re = hi;
            int gt = hi;

            while (true)
            {
                //if (_source[lt] == sample) Exchange(le++, lt++);
                //else if (_source[gt] == sample) Exchange(re--, gt--);
                //else if (_source[lt] < sample) lt++;
                //else Exchange(lt, gt--);


                while (_source[gt] >= sample)
                {
                    if (_source[gt] == sample)
                        Exchange(re--, gt);
                    gt--;
                    if (gt == le - 1)
                        break;
                }
                while (_source[lt] <= sample)
                {
                    if (_source[lt] == sample)
                        Exchange(le++, lt);
                    lt++;
                    if (lt == re + 1)
                        break;
                }
                if (gt < lt)
                    break;
                Exchange(lt++, gt--);
            }

            int leftEnd = lo;
            while (gt >= le) Exchange(gt--, leftEnd++);
            int rightEnd = hi;
            while (lt <= re) Exchange(lt++, rightEnd--);


            Sort(lo, leftEnd - 1);
            Sort(rightEnd + 1, hi);
        }

        private void Exchange(int l, int r)
        {
            int temp = _source[l];
            _source[l] = _source[r];
            _source[r] = temp;
        }
    }
}
