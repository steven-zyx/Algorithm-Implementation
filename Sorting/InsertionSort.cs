using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class InsertionSort
    {
        public void Sort<T>(T[] source) where T : IComparable
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

        private void Exchange<T>(T[] source, int i, int j)
        {
            T item = source[i];
            source[i] = source[j];
            source[j] = item;
        }
    }
}
