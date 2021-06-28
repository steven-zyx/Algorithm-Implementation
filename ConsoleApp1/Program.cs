using BasicDataStrcture;
using Sorting;
using System;
using System.Diagnostics;
using System.Linq;
using Utils;
using System.Collections;
using Searching;

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
            //ShowSpamCampaign();
            //ShowCalifornia();
            //ShowCheckStability();
            //ShowKendallTauDistance();
            //ShowIdleTime();
            //ShowFrequencyCountFromDictionary();
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
            Node<int> start = Util.GenerateLinkedList(Enumerable.Range(0, 100).ToArray());
            ShuffleLinkedList client = new ShuffleLinkedList();
            Node<int> result = client.Shuffle(start);

            for (Node<int> current = result; current != null; current = current.Next)
            {
                Console.Write(current.Value);
                Console.Write(" ");
            }
        }

        public static void ShowMultiwayHeap()
        {
            int count = 1_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

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
            int[] task = Util.GenerateRandomArray(0, count);

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

        public static void ShowCalifornia()
        {
            string[] nameList = new string[]
            {
                "steven",
                "john",
                "amy",
                "ted"
            };
            California client = new California();
            foreach (string item in client.Sort(nameList))
                Console.WriteLine(item);
        }

        public static void ShowCheckStability()
        {
            CheckStability client = new CheckStability();
            Console.WriteLine($"Is merge sort stable? {client.IsMergeSortStable()}");
            Console.WriteLine($"Is quick sort stable? {client.IsQuickSortStable()}");
            Console.WriteLine($"Is quick sort stable by force? {client.IsQuickSortStableByForce()}");
            Console.WriteLine($"Is adjusted priority queue stable? {client.IsAdjustedPriorityQueueStable()}");
        }

        public static void ShowKendallTauDistance()
        {
            int[] identityPermutation = { 0, 3, 1, 6, 2, 5, 4 };
            int[] source = { 1, 0, 3, 6, 4, 2, 5 };

            KendallTauDistance client = new KendallTauDistance();
            long ktDistinct = client.CalculateByMergeSort(source, identityPermutation);
            Console.WriteLine($"Kendall tau distance is: {ktDistinct}");
        }

        public static void ShowIdleTime()
        {
            IdleTime client = new IdleTime();
            Tuple<int, int>[] tasks = client.GenerateJob(1000, 1_000, 3);
            var result = client.MaxIdleAndBusyDuration(tasks);
            Console.WriteLine($"Maximun idle duration: {result.Item1}");
            Console.WriteLine($"Maximun busy duration: {result.Item2}");
        }

        public static void ShowFrequencyCountFromDictionary()
        {
            BinarySearch_Cache<string, int> dict = new BinarySearch_Cache<string, int>();
            dict.Put("hahaha!", 0);
            dict.Put("a", 0);
            dict.Put("can", 0);
            dict.Put("hohhoho!", 0);

            string[] words = { "can", "you", "can", "a", "can", "like", "a", "canner", "can", "can", "a", "can", "?" };
            foreach (string word in words)
            {
                if (dict.Contains(word))
                    dict.Put(word, dict.Get(word) + 1);
            }

            Console.WriteLine("The words occur in dictionary, ordered by their postion in the dictionary:");
            foreach (string key in dict.Keys())
            {
                int frequency = dict.Get(key);
                if (frequency > 0)
                    Console.WriteLine($"{key}\t{frequency}");
            }

            OrderedInsertion<int, string> existWords = new OrderedInsertion<int, string>();
            foreach (string key in dict.Keys())
            {
                int frequency = dict.Get(key);
                if (frequency > 0)
                    existWords.Put(frequency, key);
            }

            Console.WriteLine("The words occur in dictionary, ordered by their frequency:");
            while (!existWords.IsEmpty)
            {
                int frquency = existWords.Max();
                Console.WriteLine($"{existWords.Get(frquency)}\t{frquency}");
                existWords.DeleteMax();
            }
        }
    }
}
