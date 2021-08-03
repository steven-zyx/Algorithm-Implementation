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
        public static Random Ran = new Random((int)DateTime.Now.Ticks);

        public static readonly int[] Primes = {
            3, 7, 13, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191, 16381, 32749, 65521, 131071, 262139, 524287, 1048573, 2097143, 4194301,
            8388593, 16777213, 33554393, 67108859, 134217689, 268435399, 536870909, 1073741789, 2147483647 };

        public static int[] GenerateRandomArray(int start, int count)
        {
            int[] source = Enumerable.Range(start, count).ToArray();
            Shuffle(source);
            return source;
        }

        public static int[] GenerateRandomArrayRepeat(int start, int count, int repeat)
        {
            int[] source = new int[count * repeat];
            int index = 0;
            for (int i = 0; i < repeat; i++)
                for (int j = 0; j < count; j++)
                    source[index++] = start + j;
            Shuffle(source);
            return source;
        }

        private static void Shuffle(int[] source)
        {
            int count = source.Length;
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Ran.Next(0, count);
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

        public static int AVG(params int[] numbers)
        {
            long sum = 0;
            foreach (int n in numbers)
                sum += n;
            return (int)(sum / numbers.Length);
        }

        public static bool IsSorted<T>(this T[] source) where T : IComparable
        {
            for (int i = 1; i < source.Length; i++)
                if (source[i].CompareTo(source[i - 1]) < 0)
                    throw new Exception("Not sorted");
            return true;
        }

        public static string[] GenerateString(int count, int length)
        {
            Func<char> generateRandomChar = () =>
            {
                int number;
                do
                    number = Ran.Next(48, 91);
                while (number >= 58 && number <= 64);
                return (char)number;
            };

            string[] textList = new string[count];
            for (int i = 0; i < count; i++)
            {
                char[] text = new char[length];
                for (int j = 0; j < length; j++)
                    text[j] = generateRandomChar();
                textList[i] = new string(text);
            }
            return textList;
        }
    }
}
