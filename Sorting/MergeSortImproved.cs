using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MergeSortImproved
    {
        public int[] Sort(int[] source)
        {
            int[] aux = new int[source.Length];
            Sort(aux, source, 0, source.Length - 1);
            return aux;
        }

        private void Sort(int[] aux, int[] source, int lo, int hi)
        {
            if (hi - 15 <= lo)
            {
                InsertionSort(aux, lo, hi);
                return;
            }
            int mid = lo + (hi - lo) / 2;
            Sort(source, aux, lo, mid);
            Sort(source, aux, mid + 1, hi);
            if (source[mid] <= source[mid + 1])
                Array.Copy(source, lo, aux, lo, hi - lo + 1);
            else
                Merge(aux, source, lo, mid, hi);
        }

        private void InsertionSort(int[] source, int lo, int hi)
        {
            int temp;
            for (int i = lo + 1; i <= hi; i++)
            {
                for (int j = i; j > lo && source[j - 1] > source[j]; j--)
                {
                    temp = source[j];
                    source[j] = source[j - 1];
                    source[j - 1] = temp;
                }
            }
        }

        private void Merge(int[] source, int[] aux, int lo, int mid, int hi)
        {
            int leftIndex = lo;
            int rightIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
            {
                if (leftIndex >= mid + 1) source[i] = aux[rightIndex++];
                else if (rightIndex >= hi + 1) source[i] = aux[leftIndex++];
                else if (aux[leftIndex] < aux[rightIndex]) source[i] = aux[leftIndex++];
                else source[i] = aux[rightIndex++];
            }
        }
    }
}
