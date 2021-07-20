using BasicDataStrcture;
using Sorting;
using System;
using System.Diagnostics;
using System.Linq;
using Utils;
using System.Collections;
using System.Collections.Generic;
using Searching;
using System.IO;

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
            //ShowPerfectbalance();
            //LevelOrderTraversal();
            //ExactProbabilities();
            //TreeDrawing();
            //ShowAvgPathLength();
            //DrawBST();
            All23Trees();
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

        private static void PopulateBSTwithPerfectBalance(IOrderedSymbolTable<int, int> BST, List<int> data)
        {
            Queue<List<int>> sourceList = new Queue<List<int>>();
            sourceList.Enqueue(data);

            while (sourceList.Count > 0)
            {
                List<int> source = sourceList.Dequeue();
                int mid = source.Count() / 2;

                BST.Put(source[mid], mid);

                var left = source.GetRange(0, mid);
                if (left.Count() > 0)
                    sourceList.Enqueue(left);

                var right = source.GetRange(mid + 1, source.Count - mid - 1);
                if (right.Count() > 0)
                    sourceList.Enqueue(right);
            }
        }

        public static void ShowPerfectbalance()
        {
            BST_CountGet<int, int> bst = new BST_CountGet<int, int>();
            int count = 1_000;
            List<int> source = Enumerable.Range(0, count).ToList();
            PopulateBSTwithPerfectBalance(bst, source);

            for (int i = 0; i < count; i++)
            {
                bst.Get(i);
                Console.WriteLine($"{i}\t{bst.CountGet}");
            }
            DrawBST(bst);
        }

        public static void LevelOrderTraversal()
        {
            BST_CountGet<int, int> bst = new BST_CountGet<int, int>();
            int count = 1_000;
            List<int> source = Enumerable.Range(0, count).ToList();
            PopulateBSTwithPerfectBalance(bst, source);

            foreach (int key in bst.PrintLevel())
                Console.WriteLine(key);
        }

        public static void ExactProbabilities()
        {
            Dictionary<BST<int, int>, int> treeCount = new Dictionary<BST<int, int>, int>();
            for (int i = 0; i < 10_000_000; i++)
            {
                BST<int, int> bst = new BST<int, int>();

                int[] source = Util.GenerateRandomArray(2, 5);
                foreach (int x in source)
                    bst.Put(x, x);

                if (treeCount.ContainsKey(bst))
                    treeCount[bst]++;
                else
                    treeCount[bst] = 1;

                if (i % 200000 == 0)
                {
                    Console.WriteLine("Shape number:");
                    for (int j = 1; j <= treeCount.Count; j++)
                        Console.Write($"{j}\t");
                    Console.WriteLine();


                    Console.WriteLine("Probability:");
                    foreach (var pair in treeCount)
                    {
                        double prob = (double)pair.Value / i * 100;
                        prob = Math.Round(prob, 2);
                        Console.Write($"{prob}\t");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        public static void TreeDrawing()
        {
            BST<int, int> bst = new BST<int, int>();

            int count = 1_00;
            int[] source = Util.GenerateRandomArray(0, count);
            foreach (int n in source)
                bst.Put(n, n);

            DrawBST(bst);
        }

        public static void DrawBST(BST<int, int> bst)
        {
            int level = 1;
            foreach (var nl in bst.TraverseByLevel())
            {
                if (nl.Level != level)
                {
                    Console.WriteLine();
                    level = nl.Level;
                }
                Console.Write($"{nl.Node.Key} ");
            }
        }

        public static void ShowAvgPathLength()
        {
            int count = 1_000_000;
            Console.WriteLine($"Generating BST with {count.ToString("N0")} keys. Check average path length.");

            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\r\nRound: {i}");
                int[] source = Util.GenerateRandomArray(0, count);
                List<int> pathLengths;

                pathLengths = PopulateIntoBST<BST<int, bool>>(source).PathLengths();
                Console.WriteLine($"BST Avg: {pathLengths.Average()}");
                Console.WriteLine($"BST StDev: {pathLengths.StDev()}");

                pathLengths = PopulateIntoBST<BST_23<int, bool>>(source).PathLengths();
                Console.WriteLine($"BST_23 Avg: {pathLengths.Average()}");
                Console.WriteLine($"BST_23 StDev: {pathLengths.StDev()}");

                pathLengths = PopulateIntoBST<BST_23_WithoutBalance<int, bool>>(source).PathLengths();
                Console.WriteLine($"BST_23_WithoutBalance Avg: {pathLengths.Average()}");
                Console.WriteLine($"BST_23_WithoutBalance StDev: {pathLengths.StDev()}");
            }
        }

        private static BST<int, bool> PopulateIntoBST<T>(int[] source) where T : BST<int, bool>, new()
        {
            BST<int, bool> bst = new T();
            foreach (int n in source)
                bst.Put(n, false);
            return bst;
        }

        public static void DrawBST()
        {
            TestDrawTree<BST<int, bool>>("bst");
            TestDrawTree<BST_23<int, bool>>("BST_23");
            TestDrawTree<BST_234<int, bool>>("BST_234");
            Console.WriteLine("Finish drawing.");
        }

        private static void TestDrawTree<Tree>(string name) where Tree : BST<int, bool>, new()
        {
            Util.Ran = new Random(3);

            int count = 15;
            int[] source = Util.GenerateRandomArray(0, count);
            Tree tree = new Tree();
            foreach (int x in source)
                tree.Put(x, false);


            string diagram = tree.DrawTree().ToString();
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + $@"\{name}.txt";
            File.WriteAllText(fileName, diagram);
        }

        public static void All23Trees()
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"/{0}.txt";
            //new BST_23_GenerateAll(2, string.Format(fileName, "23Tree_height2"));
            //new BST_23_GenerateAll(3, string.Format(fileName, "23Tree_height3"));
            new BST_23_GenerateAll(4, string.Format(fileName, "23Tree_height4"));
            Console.WriteLine("Finish drawing");
        }
    }
}
