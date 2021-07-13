using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BasicDataStrcture;
using static System.Math;

namespace Utils
{
    public static class Util
    {
        public static int[] GenerateRandomArray(int start, int count)
        {
            int[] source = Enumerable.Range(start, count).ToArray();
            Shuffle(source);
            return source;
        }

        public static int[] GenerateRandomArrayRepeat(int start, int count, int repeat)
        {
            IEnumerable<int> numbers = new int[0];
            for (int i = 0; i < repeat; i++)
            {
                numbers = numbers.Concat(Enumerable.Range(start, count));
            }
            int[] source = numbers.ToArray();
            Shuffle(source);
            return source;
        }

        private static void Shuffle(int[] source)
        {
            int count = source.Length;
            Random ran = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < count; i++)
            {
                int randomIndex = ran.Next(0, count);
                int temp = source[i];
                source[i] = source[randomIndex];
                source[randomIndex] = temp;
            }
        }

        public static Node<int> GenerateLinkedList(int[] numbers)
        {
            Node<int> start = new Node<int>(numbers[0]);
            Node<int> current = start;
            for (int i = 1; i < numbers.Length; i++)
            {
                Node<int> item = new Node<int>(numbers[i]);
                current.Next = item;
                current = item;
            }
            return start;
        }

        public static int PrimeCeiling(int n)
        {
            while (true)
            {
                if (IsPrimeNumber(n))
                    return n;
                else
                    n++;
            }
        }

        private static bool IsPrimeNumber(int n)
        {
            int maxFactor = (int)Floor(Sqrt(n));
            for (int i = 2; i <= maxFactor; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static double StDev(this IEnumerable<int> numbers)
        {
            double avg = numbers.Average();
            double sum = numbers.Sum(x => Pow(x - avg, 2));
            return Sqrt(sum / numbers.Count() - 1);
        }
    }
}
