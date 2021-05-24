using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting
{
    public class ShellSortCases
    {
        private byte _count = 100;
        private int _recordHigh = 0;
        private byte[] _recordSource = null;
        private static readonly object _recordLock = new object();

        public void Enumerate()
        {
            byte[] numberBag = Enumerable.Range(1, _count).Select(x => (byte)x).ToArray();
            byte[] permutation = new byte[_count];
            byte[] source = new byte[_count];
            GenerateElement(ref numberBag, ref permutation, -1, ref source);
            Console.WriteLine("finish");
        }

        public void EnumerateConcurrently ()
        {
            byte threadCount = 8;
            Action<byte> generateAndSort = n =>
            {
                for (byte j = n; j < _count; j += threadCount)
                {
                    byte[] numberBag = Enumerable.Range(1, _count).Select(x => (byte)x).ToArray();
                    byte[] permutation = new byte[_count];
                    byte[] source = new byte[_count];

                    permutation[0] = numberBag[j];
                    numberBag[j] = 0;
                    GenerateElement(ref numberBag, ref permutation, 0, ref source);
                }
            };

            Task[] tasks = new Task[threadCount];
            for (byte i = 0; i < threadCount; i++)
            {
                byte taskNo = i;
                Task t = new Task(() => generateAndSort(taskNo));
                t.Start();
                tasks[i] = t;
            }
            Task.WaitAll(tasks);
            Console.WriteLine("finish");
        }

        private void GenerateElement(ref byte[] numberBag, ref byte[] permutation, sbyte level, ref byte[] source)
        {
            level++;
            for (byte i = 0; i < _count; i++)
            {
                if (numberBag[i] == 0)
                    continue;
                permutation[level] = numberBag[i];
                if (level == _count - 1)
                {
                    Array.Copy(permutation, source, _count);
                    UpdateRecord(Sort(source), permutation);
                    return;
                }
                numberBag[i] = 0;
                GenerateElement(ref numberBag, ref permutation, level, ref source);
                numberBag[i] = permutation[level];
            }
        }

        private int Sort(byte[] source)
        {
            int length = source.Length;
            int compareCount = 0;

            byte h = 40;
            while (h >= 1)
            {
                for (byte i = h; i < length; i++)
                {
                    for (byte j = i; j >= h; j -= h)
                    {
                        compareCount++;
                        if (source[j - h] > source[j])
                            Exchange(source, j - h, j);
                        else
                            break;
                    }
                }
                h /= 3;
            }
            return compareCount;
        }

        private void Exchange<T>(T[] source, int i, int j)
        {
            T item = source[i];
            source[i] = source[j];
            source[j] = item;
        }

        private void UpdateRecord(int compareCount, byte[] original)
        {
            if (compareCount >= _recordHigh)
            {
                lock (_recordLock)
                {
                    if (compareCount >= _recordHigh)
                    {
                        _recordHigh = compareCount;
                        _recordSource = original;
                        Console.WriteLine(_recordHigh);
                        Console.WriteLine(string.Join(",", original));
                    }
                }
            }
        }
    }
}
