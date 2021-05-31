using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class NutsAndBolts
    {
        public void Sort(int[] nuts, int[] bolts)
        {
            Sort(nuts, bolts, 0, bolts.Length - 1);
        }



        private void Sort(int[] scA, int[] scB, int lo, int hi)
        {
            if (hi <= lo)
                return;
            int sample = scA[lo];
            int mid = Partition(scB, lo, hi, sample);
            Partition(scA, lo, hi, scB[mid]);
            Sort(scA, scB, lo, mid - 1);
            Sort(scA, scB, mid + 1, hi);
        }


        private int Partition(int[] source, int lo, int hi, int sample)
        {
            int lt = lo - 1;
            int gt = hi + 1;
            int fix = -1;

            while (true)
            {
                while (source[++lt] < sample)
                    if (lt >= hi)
                        break;
                while (source[--gt] > sample)
                    if (gt <= lo)
                        break;
                if (lt >= gt)
                {
                    if (fix == -1)
                        fix = gt;
                    break;
                }
                else if (source[lt] == sample)
                {
                    gt++;
                    fix = lt;
                }
                else if (source[gt] == sample)
                {
                    lt--;
                    fix = gt;
                }
                else
                {
                    Exchange(source, lt, gt);
                }
            }
            if (fix >= lt)
            {
                Exchange(source, fix, lt);
                return lt;
            }
            else
            {
                Exchange(source, fix, gt);
                return gt;
            }
        }

        private void Exchange(int[] source, int a, int b)
        {
            int temp = source[a];
            source[a] = source[b];
            source[b] = temp;
        }
    }
}
