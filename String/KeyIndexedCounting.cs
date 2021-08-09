using System;

namespace String
{
    public class KeyIndexedCounting
    {
        public void Sort(int[] source, int R)
        {
            int count = source.Length;
            int[] frequency = new int[R + 1];
            for (int i = 0; i < count; i++)
                frequency[source[i] + 1]++;
            for (int i = 2; i < frequency.Length; i++)
                frequency[i] = frequency[i] + frequency[i - 1];

            int[] aux = new int[count];
            for (int i = 0; i < count; i++)
                aux[frequency[source[i]]++] = source[i];
            for (int i = 0; i < count; i++)
                source[i] = aux[i];
        }

        public void InplaceSort(int[] source, int R)
        {
            int[] count = new int[R + 1];
            for (int i = 0; i < source.Length; i++)
                count[source[i] + 1]++;
            for (int i = 2; i < R; i++)
                count[i] += count[i - 1];

            int[] startIndex = new int[R];
            Array.Copy(count, startIndex, R);

            for (int i = 0; i < source.Length; i++)
                while (i < startIndex[source[i]] || i >= count[source[i]])
                    Exchange(source, i, count[source[i]]++);
        }

        private void Exchange(int[] _source, int a, int b)
        {
            int temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }
    }
}
