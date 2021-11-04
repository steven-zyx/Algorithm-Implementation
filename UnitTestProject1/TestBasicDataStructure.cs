using BasicDataStrcture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlgorithmUnitTest.TestBasicDataStructure
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

        [TestMethod]
        public void TestDijkStraS2Stack()
        {
            (string expression, double result)[] source =
            {
                ("( 1 + 2 )",3),
                ("( sqrt ( ( ( ( 1 + 2 ) / 3 ) * 4 ) + 21 ) )",5)
            };
            DijkstraS2Stack client = new DijkstraS2Stack();
            foreach (var pair in source)
                Assert.AreEqual(pair.result, client.Evaluate(pair.expression));
        }

        [TestMethod]
        public void TestUnionFind()
        {
            UnionFind client = new UnionFind(10);
            client.Union(4, 3);
            client.Union(3, 8);
            client.Union(6, 5);
            client.Union(9, 4);
            client.Union(2, 1);
            client.Union(5, 0);
            client.Union(7, 2);
            client.Union(6, 1);
            client.Union(1, 0);
            client.Union(6, 7);

            int[][] connectedVertices = new int[2][]
            {
                new int[]{4,3,8,9 },
                new int[]{0,6,5,2,1,7 }
            };
            foreach (int[] vertices in connectedVertices)
                foreach (int vertex in vertices)
                    foreach (int other in vertices)
                    {
                        Assert.IsTrue(client.Connected(vertex, other));
                        Assert.IsTrue(client.Connected(other, vertex));
                    }
            foreach (int vertex in connectedVertices[0])
                foreach (int other in connectedVertices[1])
                {
                    Assert.IsFalse(client.Connected(vertex, other));
                    Assert.IsFalse(client.Connected(other, vertex));
                }
        }

        [TestMethod]
        public void TestBagRemoving()
        {
            int[] source = { 1, 2, 3, };
            Bag_L<int> bag = new Bag_L<int>();
            foreach (int value in source)
                bag.Add(value);
            Assert.AreEqual("321", string.Join("", bag));

            bag.Remove(1);
            Assert.AreEqual("32", string.Join("", bag));

            bag.Remove(3);
            Assert.AreEqual("2", string.Join("", bag));

            bag.Remove(2);
            Assert.AreEqual("", string.Join("", bag));
        }
    }
}
