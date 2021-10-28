﻿using BasicDataStrcture;
using Sorting;
using System;
using System.Diagnostics;
using System.Linq;
using Utils;
using System.Collections;
using System.Collections.Generic;
using Searching;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using String;
using AlgorithmImplementation.Application;
using AlgorithmImplementation.Graph;

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
            //All23Trees();
            //DisplayChiSquare();
            //HashAttach();
            //HashAttack();
            //ShowConcordance();
            //FullyIndexedCSV();
            //NonOverlappingInterval();
            //RegistrarScheduling();
            //TestKeyIntArray();
            //LongRepeats();
            //SubStringOfLengthL();
            //SubStringOfAnyLength();
            //SpellChecking();
            //WhiteList();
            //ShowRandomPhoneNumbers();
            //SubStringMatches();
            //ShowCyclicRotation();
            //TandemRepeatSearch();
            //TestGoTo();
            //ShowKeyWordInContext();
            //ShowLongestRepeatedSubString();
            //ShowRegexProof();
            ShowArbitrage();
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
            string fileName = Util.DesktopPath + $@"\{name}.txt";
            File.WriteAllText(fileName, diagram);
        }

        public static void All23Trees()
        {
            string fileName = Util.DesktopPath + @"/{0}.txt";
            new BST_23_GenerateAll(2, string.Format(fileName, "23Tree_height2"));
            new BST_23_GenerateAll(3, string.Format(fileName, "23Tree_height3"));
            new BST_23_GenerateAll(4, string.Format(fileName, "23Tree_height4"));
            Console.WriteLine("Finish drawing");
        }

        public static void DisplayChiSquare()
        {
            SeperateChainingHashST<int, bool> st = new SeperateChainingHashST<int, bool>();
            int count = 1_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 1; i < source.Length; i++)
            {
                st.Put(source[i], false);
                if (i % 100_00 == 0)
                {
                    var r = st.ChiSquare();
                    bool isRandom = r.ChiSquare > r.Min && r.ChiSquare < r.Max;
                    Console.WriteLine($"ChiSquare: {r.ChiSquare}\tMin: {r.Min}\tMax: {r.Max}\tProbability: {r.Probability}\tIsRandom: {isRandom}");
                }
            }
        }

        public static void HashAttack()
        {
            Func<string, int> hashCode = x =>
            {
                int hash = 0;
                for (int i = 0; i < x.Length; i++)
                    hash = hash * 31 + x[i];
                return hash;
            };
            Action<string> output = x =>
            {
                Console.WriteLine($"{x}\t{hashCode(x)}");
            };

            HashAttack client = new HashAttack(1000, 100, output);
            foreach (string x in client.GenerateText())
                output(x);
        }

        public static double Divide(long a, long b)
        {
            if (b == 0)
                return -1;
            else
                return (double)a / b;
        }
        public static double Divide(decimal a, long b)
        {
            if (b == 0)
                return -1;
            else
                return (double)a / b;
        }

        public static void ShowConcordance()
        {
            string text = "can you can a can like a canner can can a can";
            Concordance client = new Concordance(text.Split(' '));

            ISymbolTable<string, List<int>> st = client.WordsOccurance();
            foreach (string word in st.Keys())
            {
                Console.WriteLine(word);
                Console.Write("\t");
                foreach (int position in st.Get(word))
                {
                    Console.Write(position);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            string[] sequence = client.Invert(st);
            Console.WriteLine(string.Join(' ', sequence));
        }

        public static void FullyIndexedCSV()
        {
            Action<int> reportProgress = n =>
            {
                if (n % 1000 == 0)
                    Console.WriteLine($"Processing line:{n}");
            };

            string fileName = Util.DesktopPath + "\versafeed_Kroger_bing.txt";
            FullLookupTSV client = new FullLookupTSV(fileName, 33, reportProgress);
            client.BuildHeader();
            client.BuildIndex();
            Console.WriteLine("index built");

            while (true)
            {
                string input = Console.ReadLine();
                int space = input.IndexOf(' ');
                string column = input.Substring(0, space);
                string key = input.Substring(space + 1);

                foreach (string line in client.Search(column, key))
                    Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        public static void NonOverlappingInterval()
        {
            IOrderedSymbolTable<int, int> intervals = new BST_23<int, int>();
            intervals.Put(1643, 2033);
            intervals.Put(5532, 7643);
            intervals.Put(8999, 10332);
            intervals.Put(5666653, 5669321);

            while (true)
            {
                int value = int.Parse(Console.ReadLine());
                int lower = intervals.Floor(value);
                int higher = intervals.Get(lower);
                if (higher >= value)
                    Console.WriteLine($"{value} in {lower}-{higher}");
                else
                    Console.WriteLine($"{value} lies in no interval");
            }
        }

        public static void RegistrarScheduling()
        {
            IOrderedSET<DateTime> oset = new BinarySearchSET<DateTime>();
            Console.WriteLine("Schedule classes for a instructor");

            DateTime dt = new DateTime(2021, 7, 30, 9, 0, 0);
            for (int i = 0; i < 6; i++)
            {
                dt = dt.AddHours(1);
                if (oset.Contains(dt))
                    Console.WriteLine($"class already scheduled at {dt}");
                else
                {
                    oset.Put(dt);
                    Console.WriteLine($"class scheduled at {dt}");
                }
            }

            dt = new DateTime(2021, 7, 30, 11, 0, 0);
            if (oset.Contains(dt))
                Console.WriteLine($"classes already scheduled at {dt}");
            else
                oset.Put(dt);
        }

        public static void LongRepeats()
        {
            Alphabet alphabet = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            string sourceText = Util.GenerateLongString(alphabet.Charcters, 400_000);

            string sourceFile = Util.DesktopPath + "sourceFile.txt";
            File.WriteAllText(sourceFile, sourceText);
            string sDoubleFile = Util.DesktopPath + "sDoubleFile.txt";
            File.WriteAllText(sDoubleFile, sourceText + sourceText);
            string compressedFile = Util.DesktopPath + "compressed.txt";

            ICompression[] clients = new ICompression[] {
                new RunLengthEncoding(),
                new Haffman(),
                new LZW(16, 65536)
            };
            string[] sourceList = new string[]
            {
                sourceFile,
                sDoubleFile
            };

            foreach (ICompression client in clients)
                for (int i = 0; i < sourceList.Length; i++)
                {
                    client.Compress(sourceList[i], compressedFile);
                    double ratio = new FileInfo(compressedFile).Length / (double)new FileInfo(sourceList[i]).Length;
                    Console.WriteLine($"{client.GetType()}: {i + 1} {ratio}");
                    File.Delete(compressedFile);
                }

            File.Delete(sourceFile);
            File.Delete(sDoubleFile);
        }

        public static void SubStringOfLengthL()
        {
            UniqueSubStringOfLength client = new UniqueSubStringOfLength();
            while (true)
            {
                int length = int.Parse(Console.ReadLine());
                string content = Console.ReadLine();
                IEnumerable<string> uniqueString = client.SubStringOfLength(content, length);
                foreach (string str in uniqueString)
                    Console.WriteLine(str);
                Console.WriteLine();
            }
        }

        public static void SubStringOfAnyLength()
        {
            string content = Console.ReadLine();
            UniqueSubString client = new UniqueSubString(content);
            while (true)
            {
                int length = int.Parse(Console.ReadLine());
                IEnumerable<string> subString = client.SubStringOfLength(length);
                foreach (string str in subString)
                    Console.WriteLine(str);
                Console.WriteLine();
            }
        }

        public static void SpellChecking()
        {
            TST<bool> dict = EnglishDictionary.Load();
            while (true)
            {
                string sentence = Console.ReadLine();
                foreach (string word in sentence.Split(' '))
                    if (!dict.Contains(word))
                        Console.WriteLine(word);
                Console.WriteLine();
            }
        }

        public static void WhiteList()
        {
            Console.WriteLine("Enter white list, seperate by space");
            TST<bool> st = new TST<bool>();
            foreach (string word in Console.ReadLine().Split(' '))
                st.Put(word, true);

            while (true)
            {
                foreach (string word in Console.ReadLine().Split(' '))
                {
                    if (st.Contains(word))
                        Console.Write(word);
                    else
                        Console.Write(new string(Enumerable.Repeat('X', word.Length).ToArray()));
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public static void ShowRandomPhoneNumbers()
        {
            while (true)
            {
                int count = int.Parse(Console.ReadLine());
                IEnumerable<string> phoneNumbers = RandomPhoneNumbers.PrintRandomPhoneNumbers(count);
                foreach (string number in phoneNumbers)
                    Console.WriteLine(number);
                Console.WriteLine();
            }
        }

        public static void SubStringMatches()
        {
            Console.WriteLine("Loading...");
            TST<bool> englishDict = EnglishDictionary.Load();
            SubStringMatches client = new SubStringMatches(englishDict.Keys());
            Console.WriteLine("Loaded.");
            while (true)
            {
                string s = Console.ReadLine();
                foreach (string word in client.StringThatContains(s))
                    Console.WriteLine(word);
                Console.WriteLine();
            }
        }

        public static void ShowCyclicRotation()
        {
            while (true)
            {
                string text1 = Console.ReadLine();
                string text2 = Console.ReadLine();

                if (text1.Length != text2.Length)
                {
                    Console.WriteLine(false);
                    continue;
                }

                string source = text1 + text1;
                KMP client = new KMP(text2);
                Console.WriteLine(client.Search(source) < source.Length);
            }
        }

        public static void TandemRepeatSearch()
        {
            while (true)
            {
                string source = Console.ReadLine();
                string pattern = Console.ReadLine();

                KMP client = new KMP(pattern);
                int[] result = client.FindAll(source).ToArray();
                (int index, int count) current = (result[0], 1);
                (int index, int count) longest = current;

                for (int i = 1; i < result.Length; i++)
                {
                    if (result[i] - result[i - 1] == pattern.Length)
                    {
                        current.count++;
                        if (current.count > longest.count)
                            longest = current;
                    }
                    else
                    {
                        current.index = result[i];
                        current.count = 1;
                    }
                }
                Console.WriteLine($"start index:{longest.index}\trepeat:{longest.count}");
            }
        }

        public static void TestGoTo()
        {
            string txt = "AABABABAAABAAAB";
            txt += "AABAAA";

            int i = 0;
            s0: if (txt[i++] != 'A') goto s0;
            s1: if (txt[i++] != 'A') goto s0;
            s2: if (txt[i++] != 'B') goto s2;
            s3: if (txt[i++] != 'A') goto s0;
            s4: if (txt[i++] != 'A') goto s0;
            s5: if (txt[i++] != 'A') goto s3;
            Console.WriteLine(i - 6);
        }

        public static void ShowKeyWordInContext()
        {
            string text = @"
it was the best of times it was the worst of times
it was the age of wisdom it was the age of foolishness
it was the epoch of belief it was the epoch of incredulity
it was the season of light it was the season of darkness
it was the spring of hope it was the winter of despair";
            text = text.Replace("\r\n", " ");

            KeywordInContext client = new KeywordInContext(text);
            foreach (var context in client.ContextList("it was the"))
                Console.WriteLine(context);
        }

        public static void ShowLongestRepeatedSubString()
        {
            string text = @"
it was the best of times it was the worst of times
it was the age of wisdom it was the age of foolishness
it was the epoch of belief it was the epoch of incredulity
it was the season of light it was the season of darkness
it was the spring of hope it was the winter of despair";
            text = text.Replace("\r\n", " ");

            Console.WriteLine(LongestRepeatedSubstring.Find(text));
        }

        public static void ShowRegexProof()
        {
            Console.WriteLine("Enter regex:");
            NFAProof client = new NFAProof(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("\nEnter text:");
                string text = Console.ReadLine();

                IEnumerable<int> proof = client.ShowProof(text);
                if (proof != null)
                {
                    foreach (int stage in proof)
                        Console.Write(stage + " ");
                    Console.WriteLine();
                    foreach (char c in client.ShowProofCharacters(text))
                        Console.Write(c + " ");
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("unmatched.");
            }
        }

        public static void ShowArbitrage()
        {
            Dictionary<int, string> indexName = new Dictionary<int, string>()
            {
                { 0, "USD" },
                { 1, "EUR" },
                { 2, "GBP" },
                { 3, "CHF" },
                { 4, "CAD" },
            };
            double[,] conversion = new double[5, 5]
            {
                { 1, 0.741, 0.657, 1.061, 1.005 },
                { 1.349, 1, 0.888, 1.433, 1.366 },
                { 1.521, 1.126, 1, 1.614, 1.538 },
                { 0.942, 0.698, 0.619, 1, 0.953 },
                { 0.995, 0.732, 0.650, 1.049, 1 }
            };

            StringBuilder sb = new StringBuilder("Conversion rate table:\r\n");
            int row = conversion.GetLength(0);
            int column = conversion.GetLength(1);
            for (int r = 0; r < row; r++)
            {
                sb.Append(indexName[r]);
                sb.Append(":\t");
                for (int c = 0; c < column; c++)
                {
                    sb.Append(conversion[r, c]);
                    sb.Append("\t");
                }
                sb.Append("\r\n");
            }
            Console.WriteLine(sb.ToString());

            Arbitrage client = new Arbitrage(conversion);
            sb = new StringBuilder("\r\nArbitrage:\r\n");
            foreach (int index in client.Route)
            {
                sb.Append(" --> ");
                sb.Append(indexName[index]);
            }
            Console.WriteLine(sb.ToString());

            sb = new StringBuilder("Value:\r\n1");
            int[] route = client.Route.ToArray();
            double arbiRate = 1;
            for (int i = 1; i < route.Length; i++)
            {
                double rate = conversion[route[i - 1], route[i]];
                arbiRate *= rate;
                sb.Append(" * ");
                sb.Append(Math.Round(rate, 3));
            }
            sb.Append(" = ");
            sb.Append(Math.Round(arbiRate, 5));
            Console.WriteLine(sb.ToString());

            sb = new StringBuilder("Path length:\r\n(0");
            double nLength = 0;
            for (int i = 1; i < route.Length; i++)
            {
                double rate = conversion[route[i - 1], route[i]];
                double pathLength = Math.Log(rate);
                nLength += pathLength;
                sb.Append(") + (");
                sb.Append(Math.Round(pathLength, 3));
            }
            sb.Append(") = (");
            sb.Append(Math.Round(nLength, 5));
            sb.Append(")");
            Console.WriteLine(sb.ToString());
        }
    }
}
