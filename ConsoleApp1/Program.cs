using System;
using System.Threading;
using System.Diagnostics;
using BasicDataStrcture;
using System.IO;
using System.Collections;
using Sorting;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ShowListResize();
            //ShowRandomBag();
            //ShowRandomQueue();
            //ListingFiles.List();
            ShuffleLinkedList();
            Console.ReadLine();
        }

        public static void ShowListResize()
        {
            List1<int> numbers = new List1<int>();
            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < 1000; i++)
            {
                watch.Restart();
                numbers.Add(i);
                Console.WriteLine(watch.ElapsedTicks);
            }
        }

        public static void ShowRandomBag()
        {
            RandomBag<int> rBag = new RandomBag<int>();
            for (int i = 0; i < 100; i++)
                rBag.Add(i);
            foreach (int n in rBag)
            {
                Console.Write(n);
                Console.Write(" ");
            }
        }

        public static void ShowRandomQueue()
        {
            RandomQueue<int> rq = new RandomQueue<int>();
            for (int i = 1; i <= 13; i++)
                rq.Enqueue(i);

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Sample #No {i}:\t{rq.Sample()}");
            }

            Console.WriteLine("Enumerate all:");
            foreach (int item in rq)
            {
                Console.Write($"{item}\t");
            }

            Console.WriteLine("\nDequeue all:");
            for (int i = 0; i < 13; i++)
            {
                Console.Write($"{rq.Dequeue()}\t");
            }
        }

        public static void ShellSortCases()
        {
            ShellSortCases ssc = new ShellSortCases();
            ssc.EnumerateConcurrently();
        }

        public static void ShuffleLinkedList()
        {
            Node<int> start = GenerateLinkedList(Enumerable.Range(0, 100).ToArray());
            ShuffleLinkedList client = new ShuffleLinkedList();
            Node<int> result = client.Shuffle(start);

            for (Node<int> current = result; current != null; current = current.Next)
            {
                Console.Write(current.Value);
                Console.Write(" ");
            }
        }

        private static Node<int> GenerateLinkedList(int[] numbers)
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
