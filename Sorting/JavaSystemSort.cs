using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class JavaSystemSort
    {
        private int[] _source;
        private InsertionSort _client = new InsertionSort();

        public void Sort(int[] source)
        {
            _source = source;
            Sort(0, source.Length - 1);
        }


        private void Sort(int lo, int hi)
        {
            //cutoff to insertion sort
            if (lo + 15 > hi)
            {
                _client.Sort(_source, lo, hi);
                return;
            }

            //turkey ninth
            int medianIndex = CalcMedian(MedianOf3(lo), MedianOf3(lo + 3), MedianOf3(hi - 2));
            Exchange(medianIndex, lo);

            //initial sample, left and right boundaries
            int sample = _source[lo];
            int le = lo + 1, lt = lo + 1;
            int re = hi, gt = hi;


            while (true)
            {
                //proceed left boundary
                while (lt < re + 1 && _source[lt] <= sample)
                {
                    if (_source[lt] == sample)
                        Exchange(le++, lt);
                    lt++;
                }
                //proceed right boundary
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

            //return if elements are all the same
            if (lt == le && lt == hi + 1)
                return;

            //move items with equal key to middle
            int leftEnd = lo;
            while (leftEnd < le && gt >= le)
                Exchange(gt--, leftEnd++);
            int rightEnd = hi;
            while (rightEnd > re && lt <= re)
                Exchange(lt++, rightEnd--);

            //sort the subarrays
            Sort(lo, gt);
            Sort(lt, hi);
        }

        private void Exchange(int l, int r)
        {
            int temp = _source[l];
            _source[l] = _source[r];
            _source[r] = temp;
        }

        private int MedianOf3(int lo) => CalcMedian(lo, lo + 1, lo + 2);


        private int CalcMedian(int fi, int se, int th)
        {
            int a = _source[fi];
            int b = _source[se];
            int c = _source[th];

            if (a >= b && a >= c)
                return fi;
            else if (b >= a && b >= c)
                return se;
            else
                return th;
        }
    }
}
