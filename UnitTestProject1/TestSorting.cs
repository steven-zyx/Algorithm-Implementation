using BasicDataStrcture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AlgorithmUnitTest.TestSorting
{
    [TestClass]
    public class TestSorting
    {

        [TestMethod]
        public void TestSelectionSort()
        {
            var source = Util.GenerateRandomArray(0, 30_000);
            var client = new SelectionSort();
            client.Sort(source);

            for (int i = 0; i < 30_000; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestInsertionSort()
        {
            var source = Util.GenerateRandomArray(0, 30_000);
            var client = new InsertionSort();
            client.Sort(source);

            for (int i = 0; i < 30_000; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestShellSort()
        {
            var source = Util.GenerateRandomArray(0, 2_000_000);
            var client = new ShellSort();
            client.Sort(source);

            for (int i = 0; i < 2_000_000; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestShellSort2()
        {
            var source = Util.GenerateRandomArray(0, 255).Select(x => (byte)x).ToArray();
            var client = new ShellSort();
            client.Sort_2(source);

            for (int i = 0; i < 255; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestDequeueSort()
        {
            Queue<int> source = new Queue<int>(new int[] { 1, 7, 6, 3, 4, 8, 9, 5 });
            DequeueSort client = new DequeueSort();
            client.Sort(source, 1, 9);

            int preivous = source.Dequeue();
            int current;
            while (source.Count > 0)
            {
                current = source.Dequeue();
                Assert.IsTrue(preivous < current);
            }
        }

        [TestMethod]
        public void TestMergeSort()
        {
            int count = 20_000_000;
            var source = Util.GenerateRandomArray(0, count);
            var client = new MergeSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestMergeSortImproved()
        {
            int count = 20_000_000;
            var source = Util.GenerateRandomArray(0, count);
            var client = new MergeSortImproved();
            source = client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestBottomUpMergeSort()
        {
            int count = 20_000_000;
            var source = Util.GenerateRandomArray(0, count);
            var client = new BottomUpMergeSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestLinkedListSort()
        {
            int count = 6_000_000;
            var startNode = Util.GenerateLinkedList(Util.GenerateRandomArray(0, count));

            LinkedListSort client = new LinkedListSort();
            Node<int> result = client.Sort(startNode);

            int value = 0;
            for (Node<int> current = result; current != null; current = current.Next)
            {
                Assert.AreEqual(value, current.Value);
                value++;
            }
        }

        [TestMethod]
        public void TestMergeSortedQueues()
        {
            int count = 10_000_000;
            Queue<int> left = new Queue<int>(Enumerable.Range(0, count / 10).Select(x => x * 10));
            Queue<int> right = new Queue<int>(Enumerable.Range(0, count));

            MergeSortedQueues client = new MergeSortedQueues();
            Queue<int> result = client.Sort(left, right);

            int current = result.Dequeue();
            while (result.Count > 0)
            {
                Assert.IsTrue(current <= result.Peek());
                current = result.Dequeue();
            }
        }

        [TestMethod]
        public void BottomUpQueueMergeSort()
        {
            int[] source = Util.GenerateRandomArray(0, 1_000_000);
            BottomUpQueueMergeSort client = new BottomUpQueueMergeSort();
            Queue<int> result = client.Sort(source);


            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(i, result.Dequeue());
            }
        }

        [TestMethod]
        public void TestFastMergeSort()
        {
            int[] source = Util.GenerateRandomArray(0, 20_000_000);
            FasterMerge client = new FasterMerge();
            client.Sort(source);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestNaturalMergeSort()
        {
            int[] source = Util.GenerateRandomArray(0, 20_000_000);
            NaturalMergeSort client = new NaturalMergeSort();
            client.Sort(source);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestNaturalMergeSort2()
        {
            int count = 20_000_000;
            int stepSize = 10_000;
            List<int> numbers = new List<int>(count);
            for (int i = count - stepSize; i >= 0; i -= stepSize)
                numbers.AddRange(Enumerable.Range(i, stepSize));
            int[] source = numbers.ToArray();

            NaturalMergeSort client = new NaturalMergeSort();
            client.Sort(source);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestCalcInversions()
        {
            int count = 30_000;
            int[] source = Util.GenerateRandomArray(0, count);
            int[] source2 = new int[source.Length];
            Array.Copy(source, source2, source.Length);
            Inversions client = new Inversions();
            long qR = client.QudraticCalc(source);
            long lR = client.Calculate(source2);
            Assert.AreEqual(qR, lR);
        }

        [TestMethod]
        public void TestIndirectSort()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            IndirectSort client = new IndirectSort();
            int[] perm = client.Sort(source);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[perm[i]]);
            }
        }

        [TestMethod]
        public void TestIndirectSort2()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            int i = 0;
            foreach (int x in source.OrderBy())
            {
                Assert.AreEqual(i, x);
                i++;
            }
        }

        [TestMethod]
        public void TestTriplicates()
        {
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();
            List<int> l3 = new List<int>();

            for (int i = 1; i < 10_000_000; i++)
            {
                if (i % 83 == 0) l1.Add(i);
                if (i % 89 == 0) l2.Add(i);
                if (i % 97 == 0) l3.Add(i);
            }

            Triplicates client = new Triplicates();
            int common = client.FindCommon(l1.ToArray(), l2.ToArray(), l3.ToArray());
            Assert.AreEqual(716_539, common);
        }

        [TestMethod]
        public void Test3WayMergeSort()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            _3WayMergeSort client = new _3WayMergeSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestQuickSort()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            QuickSort<int> client = new QuickSort<int>();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestSentinels()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            Sentinels client = new Sentinels();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestQuickSort_SameItem()
        {
            int count = 10_000_000;
            int[] source1 = Util.GenerateRandomArray(0, count);
            int[] source2 = Util.GenerateRandomArray(0, count);
            int[] source = new int[count * 2];
            Array.Copy(source1, source, count);
            Array.Copy(source2, 0, source, count, count);


            QuickSort<int> client = new QuickSort<int>();
            client.Sort(source);

            for (int i = 0; i < count * 2; i++)
            {
                Assert.AreEqual(i / 2, source[i]);
            }
        }

        [TestMethod]
        public void TestQuickSort3Way()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            QuickSort3Way client = new QuickSort3Way();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestQuickSort3Way_Duplicate()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] % 10;
            }
            QuickSort3Way client = new QuickSort3Way();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i / (count / 10), source[i]);
            }
        }

        [TestMethod]
        public void TestQuickSort_Duplicate()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] % 10;
            }
            QuickSort<int> client = new QuickSort<int>();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i / (count / 10), source[i]);
            }
        }

        [TestMethod]
        public void TestMedianOf3()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            MedianOf3Partitioning client = new MedianOf3Partitioning();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestMedianOf5()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            MedianOf5Partitioning client = new MedianOf5Partitioning();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestNutsAndBolts()
        {
            int count = 10_000_000;
            int[] nuts = Util.GenerateRandomArray(0, count);
            int[] bolts = Util.GenerateRandomArray(0, count);

            NutsAndBolts client = new NutsAndBolts();
            client.Sort(nuts, bolts);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, nuts[i]);
                Assert.AreEqual(i, bolts[i]);
            }
        }

        [TestMethod]
        public void TestBestCase()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            BestCase client = new BestCase();
            client.Generate(source);
            bool isBest = client.SortAndValidate(source);
        }

        [TestMethod]
        public void TestNonRecursiveQuickSort()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            NonRecursiveQuickSort client = new NonRecursiveQuickSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestFast3WayPartition()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            Fast3WayPartition client = new Fast3WayPartition();
            client.Sort(source);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestFast3WayPartition_Duplicate()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] % 10;
            }
            Fast3WayPartition client = new Fast3WayPartition();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i / (count / 10), source[i]);
            }
        }

        [TestMethod]
        public void TestJavaSystemSort()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            JavaSystemSort client = new JavaSystemSort();
            client.Sort(source);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestJavaSystemSort_Duplicate()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] % 10;
            }
            JavaSystemSort client = new JavaSystemSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i / (count / 10), source[i]);
            }
        }

        [TestMethod]
        public void TestSampleSort()
        {
            int count = 20_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            Random r = new Random(DateTime.Now.Second);
            HashSet<int> sample = new HashSet<int>();
            while (sample.Count < 10)
            {
                sample.Add(r.Next(0, count));
            }

            SampleSort client = new SampleSort();
            client.Sort(source, sample.ToArray());
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        [TestMethod]
        public void TestMaxPQ()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            MaxPQ pq = new MaxPQ(count);
            foreach (int n in source)
                pq.Insert(n);

            for (int i = count - 1; i >= 0; i--)
                Assert.AreEqual(i, pq.DelMax());
        }

        [TestMethod]
        public void TestIndexMinPQ_1()
        {
            int count = 5_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            IndexMinPQ<int> pq = new IndexMinPQ<int>(count);
            for (int i = 0; i < count; i++)
                pq.Insert(i, source[i]);

            Assert.IsFalse(pq.IsEmpty);
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(count - i, pq.Size);
                Assert.AreEqual(i, pq.Min);
                Assert.AreEqual(i, source[pq.MinIndex]);
                Assert.AreEqual(i, source[pq.DelMin()]);
            }
            Assert.IsTrue(pq.IsEmpty);
        }

        [TestMethod]
        public void TestIndexMinPQ_2()
        {
            int count = 5_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            int[] source2 = Util.GenerateRandomArray(0, count);

            IndexMinPQ<int> pq = new IndexMinPQ<int>(count);
            for (int i = 0; i < count; i++)
                pq.Insert(i, source[i]);

            for (int i = 0; i < count; i += 2)
            {
                pq.Delete(i);
                int j = i + 1;
                pq.Change(j, source2[j]);
            }

            int min = int.MinValue;
            for (int i = 0; i < count; i += 2)
            {
                Assert.IsFalse(pq.Contains(i));

                int currentMin = source2[pq.DelMin()];
                Assert.IsTrue(min < currentMin);
                min = currentMin;
            }
        }

        [TestMethod]
        public void TestMaxPQ_Resize()
        {
            int count = 1_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            MaxPQ pq = new MaxPQ();

            for (int x = 0; x <= 2; x++)
            {
                foreach (int n in source)
                    pq.Insert(n);

                for (int i = count - 1; i >= 0; i--)
                    Assert.AreEqual(i, pq.DelMax());
            }
        }

        [TestMethod]
        public void TestMaxPQ_Min()
        {
            int count = 1_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            MaxPQ pq = new MaxPQ();


            for (int i = 0; i < 3; i++)
            {
                Assert.ThrowsException<InvalidOperationException>(() => pq.Min);
                int min = int.MaxValue;
                foreach (int n in source)
                {
                    if (n < min)
                        min = n;
                    pq.Insert(n);
                    Assert.AreEqual(min, pq.Min);
                }
                foreach (int n in source)
                {
                    Assert.AreEqual(min, pq.Min);
                    pq.DelMax();
                }
                Assert.ThrowsException<InvalidOperationException>(() => pq.Min);
            }
        }

        [TestMethod]
        public void TestHeapSort()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            HeapSort client = new HeapSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
                Assert.AreEqual(i, source[i]);
        }


        [TestMethod]
        public void TestHeapSortAlternative()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            List<int> backup = new List<int>(source);
            HeapSort_Alternative client = new HeapSort_Alternative();
            client.Sort(source);

            for (int i = 0; i < count; i++)
                Assert.AreEqual(i, source[i]);
        }


        [TestMethod]
        public void TestHeapWithoutExchanges()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            HeapWithoutExchanges pq = new HeapWithoutExchanges(count);
            foreach (int n in source)
                pq.Insert(n);

            for (int i = count - 1; i >= 0; i--)
                Assert.AreEqual(i, pq.DelMax());
        }

        [TestMethod]
        public void TestFastInsert()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            List<int> backUp = new List<int>(source);

            FastInsert pq = new FastInsert(count);
            foreach (int n in source)
                pq.Insert(n);

            for (int i = count - 1; i >= 0; i--)
                Assert.AreEqual(i, pq.DelMax());
        }

        [TestMethod]
        public void TestIndexMinMaxPQ()
        {
            int count = 5_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            IndexMinMaxPQ pq = new IndexMinMaxPQ(count);
            foreach (int n in source)
                pq.Insert(n);

            int min = 0, max = count - 1;
            while (!pq.IsEmpty)
            {
                Assert.AreEqual(min++, pq.DeleteMin());
                Assert.AreEqual(max--, pq.DeleteMax());
            }
        }

        [TestMethod]
        public void TestPQwithExplicitLinks()
        {
            //while (true)
            //{
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            List<int> backup = source.ToList();

            PQwithExplicitLinks pq = new PQwithExplicitLinks();
            foreach (int n in source)
            {
                pq.Insert(n);
                //Assert.IsTrue(pq.IsConsistant());
            }

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, pq.DeleteMin());
                //Assert.IsTrue(pq.IsConsistant());
            }
            //}
        }

        [TestMethod]
        public void TestMultiwayHeap()
        {
            //while (true)
            //{
            int count = 1_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            //List<int> backup = source.ToList();

            MultiwayHeap pq = new MultiwayHeap(count, 3);
            foreach (int n in source)
            {
                pq.Insert(n);
                //Assert.IsTrue(pq.TestConsistant());
            }

            for (int i = source.Length - 1; i >= 0; i--)
            {
                Assert.AreEqual(i, pq.DeleteMax());
                //Assert.IsTrue(pq.TestConsistant());
            }
            //}
        }


        [TestMethod]
        public void TestMultiwayHeap_Floyds()
        {
            //while (true)
            //{
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);
            //List<int> backup = source.ToList();

            MultiwayHeap_Floyds pq = new MultiwayHeap_Floyds(count, 3);
            foreach (int n in source)
            {
                pq.Insert(n);
                //Assert.IsTrue(pq.TestConsistant());
            }

            for (int i = source.Length - 1; i >= 0; i--)
            {
                Assert.AreEqual(i, pq.DeleteMax());
                //Assert.IsTrue(pq.TestConsistant());
            }
            //}
        }

        [TestMethod]
        public void TestMinPQ()
        {
            int count = 10_000_000;
            int[] source = Util.GenerateRandomArray(0, count);

            var pq = new MinPQ<int>();
            foreach (int n in source)
                pq.Insert(n);

            for (int i = 0; i < count; i++)
                Assert.AreEqual(i, pq.DeleteMin());
        }

        [TestMethod]
        public void TestDynamicMedianFinding()
        {
            int count = 10_000_000;
            int[] source = Enumerable.Range(0, count).ToArray();

            DynamicMedianFinding client = new DynamicMedianFinding();
            foreach (int x in source)
            {
                client.Insert(x);
                Assert.IsTrue(client.Median == x / 2 || client.Median == x / 2 + 1);
            }

            float middle = count / 2;
            int previousM = client.DeleteMedian();
            while (client.Count > 0)
            {
                int currentM = client.DeleteMedian();
                Assert.IsTrue(Math.Abs(currentM - middle) >= Math.Abs(previousM - middle));
                previousM = currentM;
            }
        }

        [TestMethod]
        public void TestFindTheMedian()
        {
            int count = 100_000_001;
            int[] source = Util.GenerateRandomArray(0, count);

            FindTheMedian client = new FindTheMedian();
            Assert.AreEqual(count / 2, client.FindMedian(source));
        }

        [TestMethod]
        public void TestSelectionWithSampling()
        {
            int count = 100_000_000;
            int k = 100;
            int[] source = Util.GenerateRandomArray(0, count);

            SelectionWithSampling client = new SelectionWithSampling();
            Assert.AreEqual(k, client.FindXth(source, k));
        }
    }
}
