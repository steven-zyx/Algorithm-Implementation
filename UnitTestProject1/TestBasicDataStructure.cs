using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDataStrcture;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.Concurrent;
using System.IO;

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
            Queue1<int> numbers = new Queue1<int>();
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
            Queue2<int> numbers = new Queue2<int>();
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
            Stack1<int> numbers = new Stack1<int>();
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
            Stack2<int> numbers = new Stack2<int>();
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
            Deque1<int> deque = new Deque1<int>();
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
            Deque2<int> deque = new Deque2<int>();
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
            int size = 128;
            RingBuffer2<int> rBuffer = new RingBuffer2<int>(size);
            for (int i = 0; i < 500_000; i++)
            {
                int elementCount = i % size;

                for (int n = 0; n < elementCount; n++)
                {
                    //Assert.AreEqual(n, rBuffer.Count);
                    rBuffer.Enqueue(n);
                }
                for (int n = 0; n < elementCount; n++)
                {
                    //Assert.AreEqual(elementCount - n, rBuffer.Count);
                    Assert.AreEqual(n, rBuffer.Dequeue());
                }
                //Assert.AreEqual(0, rBuffer.Count);
            }
        }

        [TestMethod]
        public void TestRingBuffer_1Read1Write_Concurrent()
        {
            for (int n = 0; n < 3; n++)
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
                    bool isFinished = false;
                    while (true)
                    {
                        int result = rBuffer.Dequeue(out isFinished);
                        if (!isFinished)
                        {
                            Assert.AreEqual(i, result);
                            i++;
                        }
                        else
                            break;
                    }
                });

                writeTask.Start();
                readTask.Start();
                Task.WaitAll(writeTask, readTask);
                //Assert.AreEqual(0, rBuffer.Count);
            }
        }

        [TestMethod]
        public void TestRingBuffer_1WriteNRead_Concurrent()
        {
            var rBuffer = new RingBuffer2<int>(100);

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
                    List<int> resultList = new List<int>();
                    bool isFinished = false;
                    while (true)
                    {
                        int result = rBuffer.Dequeue(out isFinished);
                        if (!isFinished)
                            resultList.Add(result);
                        else
                            break;
                    }
                    return resultList;
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
            //File.WriteAllText(@"C:\Users\v-yuzhu\Desktop\log.txt", rBuffer._log.ToString());


            finalResult.Sort();
            for (int i = 0; i < finalResult.Count; i++)
            {
                Assert.AreEqual(i, finalResult[i]);
            }
            //Assert.AreEqual(0, rBuffer.Count);
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
                    foreach (int n in Enumerable.Range(0, 20_000_000))
                        rBuffer.Enqueue(n);
                });
                writeTasks.Add(task);
            }

            var readTasks = new List<Task<List<int>>>();
            for (int i = 0; i < 2; i++)
            {
                var task = new Task<List<int>>(() =>
                {
                    List<int> result = new List<int>();
                    bool isFinished = false;
                    while (true)
                    {
                        int value = rBuffer.Dequeue(out isFinished);
                        if (!isFinished)
                            result.Add(value);
                        else
                            break;
                    }
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
    }
}
