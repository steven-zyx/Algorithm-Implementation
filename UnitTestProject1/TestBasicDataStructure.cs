using BasicDataStrcture;
using Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;
using System.Diagnostics;


namespace UnitTestProject1
{
    [TestClass]
    public class TestBasicDataStructure
    {
        [TestMethod]
        public void TestList1Correct()
        {
            List1<int> numbers = new List1<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i);
            }
            Assert.AreEqual(9, numbers[9]);
            Assert.AreEqual(0, numbers[0]);
            Assert.AreEqual(5, numbers[5]);
            Assert.AreEqual(10, numbers.Count);
        }

        [TestMethod]
        public void TestQueue1Correct()
        {
            Queue_N<int> numbers = new Queue_N<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Enqueue(i);
            }
            Assert.AreEqual(10, numbers.Length);
            Assert.AreEqual(0, numbers.Dequeue());

            for (int i = 0; i < 5; i++)
            {
                numbers.Dequeue();
            }
            Assert.AreEqual(4, numbers.Length);
            Assert.AreEqual(6, numbers.Dequeue());

            for (int i = 0; i < 3; i++)
            {
                numbers.Dequeue();
            }
            Assert.ThrowsException<Exception>(() => numbers.Dequeue());

            numbers.Enqueue(11);
            numbers.Enqueue(12);
            Assert.AreEqual(2, numbers.Length);
            Assert.AreEqual(11, numbers.Dequeue());
            Assert.AreEqual(12, numbers.Dequeue());
        }

        [TestMethod]
        public void TestQueue2Correct()
        {
            Queue_A<int> numbers = new Queue_A<int>();
            for (int i = 0; i < 100_000; i++)
            {
                numbers.Enqueue(i);
            }
            Assert.AreEqual(100_000, numbers.Length);
            Assert.AreEqual(0, numbers.Dequeue());

            for (int i = 0; i < 50_000; i++)
            {
                numbers.Dequeue();
            }
            Assert.AreEqual(49_999, numbers.Length);
            Assert.AreEqual(50_001, numbers.Dequeue());

            for (int i = 0; i < 49_998; i++)
            {
                numbers.Dequeue();
            }
            Assert.ThrowsException<Exception>(() => numbers.Dequeue());

            numbers.Enqueue(100_001);
            numbers.Enqueue(100_002);
            Assert.AreEqual(2, numbers.Length);
            Assert.AreEqual(100_001, numbers.Dequeue());
            Assert.AreEqual(100_002, numbers.Dequeue());
        }

        [TestMethod]
        public void TestStack1Correct()
        {
            Stack_N<int> numbers = new Stack_N<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Push(i);
            }
            Assert.AreEqual(10, numbers.Length);
            Assert.AreEqual(9, numbers.Pop());

            for (int i = 0; i < 5; i++)
            {
                numbers.Pop();
            }
            Assert.AreEqual(4, numbers.Length);
            Assert.AreEqual(3, numbers.Pop());

            for (int i = 0; i < 3; i++)
            {
                numbers.Pop();
            }
            Assert.ThrowsException<Exception>(() => numbers.Pop());

            numbers.Push(11);
            numbers.Push(12);
            Assert.AreEqual(2, numbers.Length);
            Assert.AreEqual(12, numbers.Pop());
            Assert.AreEqual(11, numbers.Pop());
        }

        [TestMethod]
        public void TestStack2Correct()
        {
            Stack_A<int> numbers = new Stack_A<int>();
            for (int i = 0; i < 100_000; i++)
            {
                numbers.Push(i);
            }
            Assert.AreEqual(100_000, numbers.Length);
            Assert.AreEqual(99_999, numbers.Pop());

            for (int i = 0; i < 50_000; i++)
            {
                numbers.Pop();
            }
            Assert.AreEqual(49_999, numbers.Length);
            Assert.AreEqual(49_998, numbers.Pop());

            for (int i = 0; i < 49_998; i++)
            {
                numbers.Pop();
            }
            Assert.ThrowsException<Exception>(() => numbers.Pop());

            numbers.Push(100_001);
            numbers.Push(100_002);
            Assert.AreEqual(2, numbers.Length);
            Assert.AreEqual(100_002, numbers.Pop());
            Assert.AreEqual(100_001, numbers.Pop());
        }

        [TestMethod]
        public void TestStequeCorrect()
        {
            Steque<int> steque = new Steque<int>();
            for (int i = 0; i < 20; i++)
                steque.Push(i);
            Assert.AreEqual(19, steque.Pop());

            for (int i = 0; i < 18; i++)
                steque.Pop();
            Assert.AreEqual(0, steque.Pop());
            Assert.ThrowsException<Exception>(() => steque.Pop());

            for (int i = 0; i < 20; i++)
                steque.Enqueue(i);
            Assert.AreEqual(0, steque.Pop());

            for (int i = 0; i < 18; i++)
                steque.Pop();
            Assert.AreEqual(19, steque.Pop());
            Assert.ThrowsException<Exception>(() => steque.Pop());
        }

        [TestMethod]
        public void TestStequeCorrect2()
        {
            int[] data = new int[] { 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };


            Steque<int> steque = new Steque<int>();
            for (int i = 0; i < 5; i++)
                steque.Enqueue(i);

            for (int i = 0; i < 5; i++)
                steque.Push(i);

            for (int i = 0; i < 5; i++)
                steque.Enqueue(i);

            for (int i = 0; i < 15; i++)
            {
                Assert.AreEqual(data[i], steque.Pop());
            }
        }

        [TestMethod]
        public void TestDeque1Correct1()
        {
            Deque_N<int> deque = new Deque_N<int>();
            for (int i = 0; i < 5; i++)
                deque.PushLeft(i);
            Assert.AreEqual(5, deque.Size);

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(4 - i, deque.PopLeft());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());
            Assert.ThrowsException<Exception>(() => deque.PopRight());

            for (int i = 0; i < 5; i++)
                deque.PushRight(i);
            Assert.AreEqual(5, deque.Size);

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(4 - i, deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());

            for (int i = 0; i < 5; i++)
                deque.PushLeft(i);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(i, deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());
        }

        [TestMethod]
        public void TestDeque2Correct1()
        {
            Deque_A<int> deque = new Deque_A<int>();
            for (int i = 0; i < 100_000; i++)
                deque.PushLeft(i);
            Assert.AreEqual(100_000, deque.Size);

            for (int i = 0; i < 100_000; i++)
                Assert.AreEqual(100_000 - 1 - i, deque.PopLeft());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());
            Assert.ThrowsException<Exception>(() => deque.PopRight());

            for (int i = 0; i < 100_000; i++)
                deque.PushRight(i);
            Assert.AreEqual(100_000, deque.Size);

            for (int i = 0; i < 100_000; i++)
                Assert.AreEqual(100_000 - 1 - i, deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());

            for (int i = 0; i < 100_000; i++)
                deque.PushLeft(i);
            for (int i = 0; i < 100_000; i++)
                Assert.AreEqual(i, deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopRight());
            Assert.ThrowsException<Exception>(() => deque.PopLeft());
        }

        [TestMethod]
        public void TestJosephus1()
        {
            int[] expectedtoDied = new int[] { 1, 3, 5, 0, 4, 2, 6 };
            Josephus josephusClient = new Josephus(7, 2);
            for (int i = 0; i < expectedtoDied.Length; i++)
            {
                Assert.AreEqual(expectedtoDied[i], josephusClient.DiedPeople.Dequeue());
            }
        }

        [TestMethod]
        public void TestJosephus2()
        {
            int[] expectedtoDied = new int[] { 3, 0, 5, 4, 6, 2, 1 };
            Josephus josephusClient = new Josephus(7, 4);
            for (int i = 0; i < expectedtoDied.Length; i++)
            {
                Assert.AreEqual(expectedtoDied[i], josephusClient.DiedPeople.Dequeue());
            }
        }

        [TestMethod]
        public void TestJosephus3()
        {
            int[] expectedtoDied = new int[] { 2, 5, 8, 1, 6, 0, 7, 4, 9, 3 };
            Josephus josephusClient = new Josephus(10, 3);
            for (int i = 0; i < expectedtoDied.Length; i++)
            {
                Assert.AreEqual(expectedtoDied[i], josephusClient.DiedPeople.Dequeue());
            }
        }

        [TestMethod]
        public void TestGeneralizedQueue_A()
        {
            GeneralizedQueue_A<int> queueA = new GeneralizedQueue_A<int>();
            for (int i = 1; i <= 100_000; i++)
            {
                queueA.Insert(i);
            }
            Assert.AreEqual(3, queueA.Delete(3));
            Assert.AreEqual(10_001, queueA.Delete(10_000));
            Assert.AreEqual(50_002, queueA.Delete(50_000));

            int offset = 3;
            for (int i = 99_997; i >= 1; i--)
            {
                if (i == 2 || i == 9_999 || i == 49_999)
                    offset--;
                Assert.AreEqual(i + offset, queueA.Delete(i));
            }
        }

        [TestMethod]
        public void TestGeneralizedQueue_L()
        {
            GeneralizedQueue_L<int> queueL = new GeneralizedQueue_L<int>();
            for (int i = 1; i <= 100_000; i++)
            {
                queueL.Insert(i);
            }
            Assert.AreEqual(3, queueL.Delete(3));
            Assert.AreEqual(10_001, queueL.Delete(10_000));
            Assert.AreEqual(50_002, queueL.Delete(50_000));

            int offset = 3;
            for (int i = 99_997; i >= 1; i--)
            {
                if (i == 2 || i == 9_999 || i == 49_999)
                    offset--;
                Assert.AreEqual(i + offset, queueL.Delete(i));
            }
        }

        [TestMethod]
        public void TestRingBuffer_Serial()
        {
            int size = 100;
            RingBuffer2<int> rBuffer = new RingBuffer2<int>(size);
            for (int i = 0; i < 500_000; i++)
            {
                int elementCount = i % size;

                for (int n = 0; n < elementCount; n++)
                {
                    rBuffer.Enqueue(n);
                }
                int value;
                for (int n = 0; n < elementCount; n++)
                {
                    rBuffer.Dequeue(out value);
                    Assert.AreEqual(n, value);
                }
            }
        }

        [TestMethod]
        public void TestRingBuffer_1Read1Write_Concurrent()
        {
            int loopCount = 20_000_000;
            RingBuffer2<int> rBuffer = new RingBuffer2<int>(100_000);

            Task writeTask = new Task(() =>
            {
                for (int i = 0; i < loopCount; i++)
                    rBuffer.Enqueue(i);
                rBuffer.FinishWrite();
            });
            Task readTask = new Task(() =>
            {
                int i = 0;
                int value;
                while (rBuffer.Dequeue(out value))
                {
                    Assert.AreEqual(i, value);
                    i++;
                }
            });

            writeTask.Start();
            readTask.Start();
            Task.WaitAll(writeTask, readTask);
        }

        [TestMethod]
        public void TestRingBuffer_1WriteNRead_Concurrent()
        {
            var rBuffer = new RingBuffer2<int>(100_000);

            Task writeTask = new Task(() =>
            {
                foreach (int n in Enumerable.Range(0, 4_000_000))
                    rBuffer.Enqueue(n);
                rBuffer.FinishWrite();
            });

            var readTasks = new List<Task<List<int>>>();
            for (int i = 0; i < 2; i++)
            {
                var task = new Task<List<int>>(() =>
                {
                    List<int> result = new List<int>();
                    int value;
                    while (rBuffer.Dequeue(out value))
                        result.Add(value);
                    return result;
                });
                readTasks.Add(task);
            }



            writeTask.Start();
            readTasks.ForEach(x => x.Start());
            writeTask.Wait();
            List<int> finalResult = new List<int>();
            foreach (var readTask in readTasks)
            {
                finalResult.AddRange(readTask.Result);
            }


            finalResult.Sort();
            for (int i = 0; i < finalResult.Count; i++)
            {
                Assert.AreEqual(i, finalResult[i]);
            }
        }

        [TestMethod]
        public void TestRingBuffer_NWriteNRead_Concurrent()
        {
            var rBuffer = new RingBuffer2<int>(100_000);

            List<Task> writeTasks = new List<Task>();
            for (int i = 0; i < 3; i++)
            {
                var task = new Task(() =>
                {
                    foreach (int n in Enumerable.Range(0, 10_000_000))
                        rBuffer.Enqueue(n);
                });
                writeTasks.Add(task);
            }

            var readTasks = new List<Task<List<int>>>();
            for (int i = 0; i < 3; i++)
            {
                var task = new Task<List<int>>(() =>
                {
                    List<int> result = new List<int>();
                    //int value;
                    //while (rBuffer.Dequeue(out value))
                    //    result.Add(value);
                    foreach (int n in rBuffer.GetComsumingEnumerator())
                        result.Add(n);
                    return result;
                });
                readTasks.Add(task);
            }

            writeTasks.ForEach(x => x.Start());
            readTasks.ForEach(x => x.Start());
            Task.WaitAll(writeTasks.ToArray());
            rBuffer.FinishWrite();
            List<int> finalResult = new List<int>();
            foreach (var readTask in readTasks)
            {
                finalResult.AddRange(readTask.Result);
            }

            finalResult.Sort();
            for (int i = 0; i < finalResult.Count; i++)
            {
                Assert.AreEqual(i / writeTasks.Count, finalResult[i]);
            }
        }

        [TestMethod]
        public void TestMoveToFront()
        {
            char[] input = new char[] { 'a', 'b', 'c', 'd', 'b', 'c', 'a' };
            MoveToFront mtf = new MoveToFront();
            foreach (char c in input)
            {
                mtf.Add(c);
            }

            Queue<char> output = new Queue<char>(new char[] { 'a', 'c', 'b', 'd' });
            foreach (char c in mtf)
            {
                Assert.AreEqual(output.Dequeue(), c);
            }
        }

        [TestMethod]
        public void TestCopyAQueue_Enumerate()
        {
            List<int> data = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Queue_A<int> queueA = new Queue_A<int>();
            data.ForEach(x => queueA.Enqueue(x));

            Queue_N<int> queueN = new Queue_N<int>(queueA as IEnumerable);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(queueA.Dequeue(), queueN.Dequeue());
            }

            data.ForEach(x => queueN.Enqueue(x));
            Queue_A<int> queueA2 = new Queue_A<int>(queueN as IEnumerable);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(queueN.Dequeue(), queueA2.Dequeue());
            }
        }

        [TestMethod]
        public void TestCopyAQueue_Dequeue()
        {
            List<int> data = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Queue_A<int> queueA = new Queue_A<int>();
            data.ForEach(x => queueA.Enqueue(x));

            Queue_N<int> queueN = new Queue_N<int>(queueA as IQueue<int>);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(queueA.Dequeue(), queueN.Dequeue());
            }

            data.ForEach(x => queueN.Enqueue(x));
            Queue_A<int> queueA2 = new Queue_A<int>(queueN as IQueue<int>);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(queueN.Dequeue(), queueA2.Dequeue());
            }
        }

        [TestMethod]
        public void TestCopyAStack_Enumerate()
        {
            List<int> data = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var stackA = new Stack_A<int>();
            data.ForEach(x => stackA.Push(x));

            var stackN = new Stack_N<int>(stackA as IStack<int>);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(stackA.Pop(), stackN.Pop());
            }

            data.ForEach(x => stackN.Push(x));
            var queueA2 = new Stack_A<int>(stackN as IStack<int>);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(stackN.Pop(), queueA2.Pop());
            }
        }

        [TestMethod]
        public void TestStackGenerability()
        {
            int?[] sequence1 = new int?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, null, null, null };
            int?[] sequence2 = new int?[] { 1, 2, null, null, 1, 2, 3, null, null, null, 1, 2, 3, 4, null };
            int?[] sequence3 = new int?[] { 1, 2, null, null, 1, 2, 3, null, null, 4, 5, null, null, null, null, 1, 2, 3 };

            Assert.IsTrue(Stack_A<int>.IsGenerable(sequence1));
            Assert.IsTrue(Stack_A<int>.IsGenerable(sequence2));
            Assert.IsFalse(Stack_A<int>.IsGenerable(sequence3));
        }

        [TestMethod]
        public void TestDoubleStack()
        {
            int[] input1 = new int[] { 1, 2, 3, 4, 5 };
            int[] input2 = new int[] { -1, -2, -3, -4, -5 };
            var ds = new DoubleStack<int, Deque_A<int>>();


            foreach (int i in input1)
                ds.Push1(i);
            Assert.ThrowsException<Exception>(() => ds.Pop2());

            foreach (int i in input2)
                ds.Push2(i);

            for (int i = 4; i >= 0; i--)
            {
                Assert.AreEqual(input1[i], ds.Pop1());
                Assert.AreEqual(input2[i], ds.Pop2());
            }
            Assert.ThrowsException<Exception>(() => ds.Pop1());
            Assert.ThrowsException<Exception>(() => ds.Pop2());
        }

        [TestMethod]
        public void TestFailFastOfStack()
        {
            Stack_A<int> stack = new Stack_A<int>();
            for (int i = 0; i < 10; i++)
                stack.Push(i);

            var enumerator = stack.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(9, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(8, enumerator.Current);

            stack.Push(100);
            Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
        }

        private int[] GenerateRandomArray(int start, int count)
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

        [TestMethod]
        public void TestSelectionSort()
        {
            var source = GenerateRandomArray(0, 30_000);
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
            var source = GenerateRandomArray(0, 30_000);
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
            var source = GenerateRandomArray(0, 2_000_000);
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
            var source = GenerateRandomArray(0, 255).Select(x => (byte)x).ToArray();
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
            var source = GenerateRandomArray(0, count);
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
            var source = GenerateRandomArray(0, count);
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
            var source = GenerateRandomArray(0, count);
            var client = new BottomUpMergeSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i, source[i]);
            }
        }


        private Node<int> GenerateLinkedList(int[] numbers)
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

        [TestMethod]
        public void TestLinkedListSort()
        {
            int count = 6_000_000;
            var startNode = GenerateLinkedList(GenerateRandomArray(0, count));

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
            int[] source = GenerateRandomArray(0, 1_000_000);
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
            int[] source = GenerateRandomArray(0, 20_000_000);
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
            int[] source = GenerateRandomArray(0, 20_000_000);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
            QuickSort client = new QuickSort();
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source1 = GenerateRandomArray(0, count);
            int[] source2 = GenerateRandomArray(0, count);
            int[] source = new int[count * 2];
            Array.Copy(source1, source, count);
            Array.Copy(source2, 0, source, count, count);


            QuickSort client = new QuickSort();
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] % 10;
            }
            QuickSort client = new QuickSort();
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] nuts = GenerateRandomArray(0, count);
            int[] bolts = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);
            BestCase client = new BestCase();
            client.Generate(source);
            bool isBest = client.SortAndValidate(source);
        }

        [TestMethod]
        public void TestNonRecursiveQuickSort()
        {
            int count = 20_000_000;
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);

            IndexMinPQ pq = new IndexMinPQ(count);
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
            int[] source = GenerateRandomArray(0, count);
            int[] source2 = GenerateRandomArray(0, count);

            IndexMinPQ pq = new IndexMinPQ(count);
            for (int i = 0; i < count; i++)
                pq.Insert(i, source[i]);

            for (int i = 0; i < count; i += 2)
            {
                pq.Delete(i);
                int j = i + 1;
                //pq.Change(j, source2[j]);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
            HeapSort client = new HeapSort();
            client.Sort(source);

            for (int i = 0; i < count; i++)
                Assert.AreEqual(i, source[i]);
        }


        [TestMethod]
        public void TestHeapSortAlternative()
        {
            int count = 10_000_000;
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);
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
            int[] source = GenerateRandomArray(0, count);

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
            int[] source = GenerateRandomArray(0, count);

            FindTheMedian client = new FindTheMedian();
            Assert.AreEqual(count / 2, client.FindMedian(source));
        }


    }
}
