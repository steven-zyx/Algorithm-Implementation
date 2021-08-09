using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using String;
using Sorting;

namespace UnitTestProject1
{
    [TestClass]
    public class TestString
    {
        [TestMethod]
        public void TestKeyIndexedCounting()
        {
            int[] source = Util.GenerateRandomArrayRepeat(0, 30, 1_000_000);
            KeyIndexedCounting client = new KeyIndexedCounting();
            client.Sort(source, 30);
            Assert.IsTrue(source.IsSorted());
        }

        [TestMethod]
        public void TestLSDSort()
        {
            string[] stringList = Util.GenerateFixedLengthString(1_000_000, 10);
            LSDSort client = new LSDSort();
            client.Sort(stringList, 10);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestMSDStringSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(1_000_000, 10);
            MSDStringSort client = new MSDStringSort();
            Alphabet a = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            client.Sort(stringList, a);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestThreeWayStringQuickSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(1_000_000, 10);
            ThreeWayStringQuickSort client = new ThreeWayStringQuickSort();
            client.Sort(stringList);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestQueueSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(1_000_000, 10);
            QueueSort client = new QueueSort();
            Alphabet a = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            client.Sort(stringList, a);
            Assert.IsTrue(stringList.IsSorted());
        }


        [TestMethod]
        public void TestHybridSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(1_000_000, 10);
            HybridSort client = new HybridSort();
            Alphabet a = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            client.Sort(stringList, a);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestThreeWayIntArrayQuickSort()
        {
            int[][] intArrayList = Util.GenerateIntArray(1_000_000, 10);
            ThreeWayIntArrayQuickSort client = new ThreeWayIntArrayQuickSort();
            client.Sort(intArrayList);
            Assert.IsTrue(intArrayList.IsSorted());
        }

        [TestMethod]
        public void TestMSDIntSort()
        {
            int[] source = Util.GenerateRandomArray(0, 1_000_000);
            MSDIntSort client = new MSDIntSort();
            client.Sort(source, null);
            Assert.IsTrue(source.IsSorted());
        }

        [TestMethod]
        public void Test3WayLinkedList()
        {
            string[] textList = Util.GenerateDynamicLengthString(1_000_000, 10);
            var linkedList = Util.GenerateDoublyLinkedList(textList);
            ThreeWayStringQuickSort4LinkedList client = new ThreeWayStringQuickSort4LinkedList();
            client.Sort(linkedList.start, linkedList.end);
            Util.IsSorted(linkedList.start);
        }

        [TestMethod]
        public void TestInplaceKeyIndexedCounting()
        {
            int[] source = Util.GenerateRandomArrayRepeat(0, 30, 1_000_000);
            KeyIndexedCounting client = new KeyIndexedCounting();
            client.InplaceSort(source, 30);
            Assert.IsTrue(source.IsSorted());
        }
    }
}
