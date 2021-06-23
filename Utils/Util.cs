using System;
using System.Collections;
using System.Linq;
using BasicDataStrcture;

namespace Utils
{
    public class Util
    {
        public static int[] GenerateRandomArray(int start, int count)
        {
            int[] source = Enumerable.Range(start, count).ToArray();
            Random ran = new Random(DateTime.Now.Second);

            int temp;
            int randomIndex;
            for (int i = 0; i < count; i++)
            {
                randomIndex = ran.Next(0, count);
                temp = source[i];
                source[i] = source[randomIndex];
                source[randomIndex] = temp;
            }
            return source;
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
    }
}
