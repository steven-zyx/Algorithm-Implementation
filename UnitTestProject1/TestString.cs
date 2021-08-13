using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using String;
using Sorting;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class TestString
    {
        protected Alphabet _alphabet;

        public TestString()
        {
            _alphabet = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

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
            string[] stringList = Util.GenerateFixedLengthString(_alphabet.Charcters, 1_000_000, 10);
            LSDSort client = new LSDSort();
            client.Sort(stringList, 10);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestMSDStringSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(_alphabet.Charcters, 1_000_000, 10);
            MSDStringSort client = new MSDStringSort();
            client.Sort(stringList, _alphabet);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestThreeWayStringQuickSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(_alphabet.Charcters, 1_000_000, 10);
            ThreeWayStringQuickSort client = new ThreeWayStringQuickSort();
            client.Sort(stringList);
            Assert.IsTrue(stringList.IsSorted());
        }

        [TestMethod]
        public void TestQueueSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(_alphabet.Charcters, 1_000_000, 10);
            QueueSort client = new QueueSort();
            client.Sort(stringList, _alphabet);
            Assert.IsTrue(stringList.IsSorted());
        }


        [TestMethod]
        public void TestHybridSort()
        {
            string[] stringList = Util.GenerateDynamicLengthString(_alphabet.Charcters, 1_000_000, 10);
            HybridSort client = new HybridSort();
            client.Sort(stringList, _alphabet);
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
            string[] textList = Util.GenerateDynamicLengthString(_alphabet.Charcters, 1_000_000, 10);
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

    public class TestStringST
    {
        protected IStringSymbolTable<int> _st;
        protected Alphabet _alphabet;
        protected int _rowCount;
        protected string[] _stringList;

        public TestStringST()
        {
            _rowCount = 100_000;
            _alphabet = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [TestMethod]
        public void TestPutGet()
        {
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            Dictionary<string, int> dict = stringList.ToDictionary(x => x, y => Util.Ran.Next(1, int.MaxValue));

            for (int i = 0; i < 2; i++)
            {
                foreach (var pair in dict)
                    _st.Put(pair.Key, pair.Value);
                foreach (var pair in dict)
                    Assert.AreEqual(pair.Value, _st.Get(pair.Key));

                HashSet<string> otherStringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 100, 15);
                foreach (string text in otherStringList)
                    if (!dict.ContainsKey(text))
                        Assert.AreEqual(0, _st.Get(text));
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            foreach (string text in stringList)
                _st.Put(text, 1);

            foreach (string text in stringList)
            {
                Assert.IsTrue(_st.Contains(text));
                _st.Delete(text);
                Assert.IsFalse(_st.Contains(text));
            }
        }

        [TestMethod]
        public void TestDeleteRandomly()
        {
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            foreach (string text in stringList)
                _st.Put(text, 1);

            string[] randomStringList = stringList.ToArray();
            Util.Shuffle(randomStringList);
            foreach (string text in randomStringList)
            {
                Assert.IsTrue(_st.Contains(text));
                _st.Delete(text);
                Assert.IsFalse(_st.Contains(text));
            }
        }

        [TestMethod]
        public void TestSize()
        {
            _rowCount /= 10;
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);

            int count = 0;
            foreach (string text in stringList)
            {
                if (count % 500 == 0)
                    Assert.AreEqual(count, _st.Size());

                _st.Put(text, 1);
                count++;
            }
        }

        [TestMethod]
        public void TestKeys()
        {
            _rowCount /= 10;
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            HashSet<string> addedString = new HashSet<string>();

            int count = 0;
            foreach (string text in stringList)
            {
                if (count % 500 == 0)
                    Assert.IsTrue(addedString.SetEquals(_st.Keys()));

                _st.Put(text, 1);
                addedString.Add(text);
                count++;
            }
        }

        [TestMethod]
        public void TestKeysWithPrefix()
        {
            _rowCount /= 2;

            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            foreach (string text in stringList)
                _st.Put(text, 1);

            HashSet<string> prefixList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 100, 4);
            foreach (string prefix in prefixList)
            {
                HashSet<string> correct = stringList.Where(x => x.StartsWith(prefix)).ToHashSet();
                Assert.IsTrue(correct.SetEquals(_st.KeysWithPrefix(prefix)));
            }
        }

        [TestMethod]
        public void TestLongestPrefixOf()
        {
            _rowCount *= 5;
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 5);
            foreach (string text in stringList)
                _st.Put(text, 1);

            HashSet<string> longStringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 20, 20);
            foreach (string longStr in longStringList)
            {
                string correct = "";
                foreach (string text in stringList)
                    if (longStr.StartsWith(text) && text.Length > correct.Length)
                        correct = text;

                Assert.AreEqual(correct, _st.LongestPrefixOf(longStr));
            }
        }
    }

    [TestClass]
    public class TestTrie : TestStringST
    {
        public TestTrie()
        {
            _st = new TrieST<int>(_alphabet);
        }
    }
}