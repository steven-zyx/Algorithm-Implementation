using System;

namespace Sorting
{
    public class SelectionSort
    {
        public void Sort<T>(T[] source)where T : IComparable
        {
            int count = source.Length;
            for (int i = 0; i < count; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < count; j++)
                {
                    if (source[j].CompareTo(source[minIndex]) < 0)
                        minIndex = j;
                }
                Exchange(source, minIndex, i);
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
