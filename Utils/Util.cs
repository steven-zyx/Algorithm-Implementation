﻿using System;
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

        public static void Shuffle<T>(T[] source)
        {
            int count = source.Length;
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Ran.Next(0, count);
                T temp = source[i];
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

        public static (Node_D<string> start, Node_D<string> end) GenerateDoublyLinkedList(string[] textList)
        {
            int count = textList.Length;
            Node_D<string>[] nodeList = textList.Select(x => new Node_D<string>(x)).ToArray();
            for (int i = 1; i < count - 1; i++)
            {
                nodeList[i].Previous = nodeList[i - 1];
                nodeList[i].Next = nodeList[i + 1];
            }
            nodeList[0].Next = nodeList[1];
            nodeList[count - 1].Previous = nodeList[count - 2];

            return (nodeList[0], nodeList[count - 1]);
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

        public static bool IsSorted(this int[][] source)
        {
            for (int i = 1; i < source.Length; i++)
                if (Compare(source[i - 1], source[i]) > 0)
                    throw new Exception("Not sorted");
            return true;
        }

        public static bool IsSorted<T>(Node_D<T> start) where T : IComparable
        {
            while (start.Next != null)
            {
                if (start.Value.CompareTo(start.Next.Value) > 0)
                    throw new Exception("Not sorted");
                start = start.Next;
            }
            return true;
        }

        public static int Compare(int[] a, int[] b)
        {
            int maxLength = Max(a.Length, b.Length);
            for (int i = 0; i < maxLength; i++)
            {
                int valueA = int.MinValue;
                if (i < a.Length)
                    valueA = a[i];

                int valueB = int.MinValue;
                if (i < b.Length)
                    valueB = b[i];

                int result = valueA.CompareTo(valueB);
                if (result != 0)
                    return result;
            }
            return 0;
        }

        public static HashSet<string> GenerateDynamicLengthString_Distinct(char[] characters, int count, int length)
        {
            HashSet<string> textList = new HashSet<string>();
            while (textList.Count < count)
            {
                char[] text = new char[GenerateRandomLength(length)];
                for (int j = 0; j < text.Length; j++)
                    text[j] = characters[Ran.Next(0, characters.Length)];
                textList.Add(new string(text));
            }
            return textList;
        }

        public static string[] GenerateDynamicLengthString(char[] characters, int count, int length)
        {
            string[] textList = new string[count];
            for (int i = 0; i < count; i++)
            {
                char[] text = new char[GenerateRandomLength(length)];
                for (int j = 0; j < text.Length; j++)
                    text[j] = characters[Ran.Next(0, characters.Length)];
                textList[i] = new string(text);
            }
            return textList;
        }

        public static HashSet<string> GenerateFixedLengthString_Distinct(char[] characters, int count, int length)
        {
            HashSet<string> textList = new HashSet<string>();
            while (textList.Count < count)
            {
                char[] text = new char[length];
                for (int j = 0; j < length; j++)
                    text[j] = characters[Ran.Next(0, characters.Length)];
                textList.Add(new string(text));
            }
            return textList;
        }

        public static string[] GenerateFixedLengthString(char[] characters, int count, int length)
        {
            string[] textList = new string[count];
            for (int i = 0; i < count; i++)
            {
                char[] text = new char[length];
                for (int j = 0; j < length; j++)
                    text[j] = characters[Ran.Next(0, characters.Length)];
                textList[i] = new string(text);
            }
            return textList;
        }

        private static int GenerateRandomLength(int average)
        {
            int offset = Ran.Next(-1, 2);
            while (offset != 0)
            {
                average += offset;
                offset = Ran.Next(-1, 2);
            }
            if (average < 0)
                average = 0;
            return average;
        }

        public static int[][] GenerateIntArray(int count, int length)
        {
            int[][] result = new int[count][];
            for (int i = 0; i < count; i++)
            {
                int[] element = new int[length];
                for (int j = 0; j < length; j++)
                    element[j] = Ran.Next(0, int.MaxValue);
                result[i] = element;
            }
            return result;
        }

        public static double Log2(double number) => Log10(number) / Log10(2);
    }
}
