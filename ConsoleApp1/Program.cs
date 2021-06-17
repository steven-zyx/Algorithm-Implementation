﻿using System;
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
            //ShuffleLinkedList();
            //ShowMultiwayHeap();
            //ShowComputaionalNumberTheory();
            //ShowLoadBalancing();
            //ShowSortByReverseDomain();
            ShowSpamCampaign();
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

        private static int[] GenerateRandomArray(int start, int count)
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

        public static void ShowMultiwayHeap()
        {
            int count = 1_000_000;
            int[] source = GenerateRandomArray(0, count);

            for (int i = 2; i <= 9; i++)
            {
                MultiwayHeap pq = new MultiwayHeap(count, i);
                foreach (int n in source)
                    pq.Insert(n);
                for (int n = 0; n < count; n++)
                    pq.DeleteMax();

                Console.WriteLine($"{i}-ary heap did {pq.CompareCount.ToString("N0")} compares.");
            }

            for (int i = 2; i <= 9; i++)
            {
                MultiwayHeap_Floyds pq = new MultiwayHeap_Floyds(count, i);
                foreach (int n in source)
                    pq.Insert(n);
                for (int n = 0; n < count; n++)
                    pq.DeleteMax();

                Console.WriteLine($"{i}-ary heap using floyd's method did {pq.CompareCount.ToString("N0")} compares.");
            }
        }

        public static void ShowComputaionalNumberTheory()
        {
            ComputaionalNumberTheory client = new ComputaionalNumberTheory(100);
            client.Calculate();
            client.OutputPairs();
        }

        public static void ShowLoadBalancing()
        {
            int count = 100;
            int[] task = GenerateRandomArray(0, count);

            LoadBalancing client = new LoadBalancing(task, 20);
            client.Arrange();
            var minMax = client.ShowMinMax();

            Console.WriteLine($"Minimum processing time:\t{minMax.Item1}");
            Console.WriteLine($"Maximum processing time:\t{minMax.Item2}");
        }

        public static void ShowSortByReverseDomain()
        {
            string[] domainList = new string[]
            {
                "baidu.com.cn",
                "apple.com.cn",
                "pricetoen.com.edu",
            };
            SortByReverseDomain client = new SortByReverseDomain();
            foreach (string domain in client.Sort(domainList))
            {
                Console.WriteLine(domain);
            }
        }

        public static void ShowSpamCampaign()
        {
            string[] emailList = new string[]
            {
                "zhuyuxuan@beyondsoft.com",
                "v-yuzhu@microsoft.com",
                "steven_chuh@foxmail.com",
                "xiedongxidong@beyondsoft.com",
                "v-dox@microsoft.com",
            };
            new SpamCampaign(emailList);
        }
    }
}
