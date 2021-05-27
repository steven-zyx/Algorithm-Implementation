using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class Inversions
    {
        private long _qudraticInv = 0;
        private long _linearithmicInv = 0;
        private int[] _aux;


        public long Calculate(int[] source)
        {
            MergeSort(source);
            return _linearithmicInv;
        }

        public long QudraticCalc(int[] source)
        {
            InsertionSort(source);
            return _qudraticInv;
        }

        private void InsertionSort(int[] source)
        {
            int count = source.Length;
            for (int i = 1; i < count; i++)
            {
                for (int j = i; j >= 1 && source[j].CompareTo(source[j - 1]) < 0; j--)
                {
                    Exchange(source, j, j - 1);
                }
            }
        }

        private void Exchange(int[] source, int i, int j)
        {
            int item = source[i];
            source[i] = source[j];
            source[j] = item;

            _qudraticInv++;
        }

        public void MergeSort(int[] source)
        {
            _aux = new int[source.Length];
            MergeSort(source, 0, source.Length - 1);
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
                else if (_aux[leftIndex] < _aux[rightIndex]) source[i] = _aux[leftIndex++];
                else
                {
                    source[i] = _aux[rightIndex++];
                    _linearithmicInv += (mid + 1 - leftIndex);
                }
            }
        }
    }
}
