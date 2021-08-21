﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using String;
using Sorting;
using System.Linq;
using System.Text.RegularExpressions;
using Searching;

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
        protected ISymbolTable<string, int> _st;
        protected Alphabet _alphabet;
        protected int _rowCount;
        protected string[] _stringList;

        protected IStringSymbolTable<int> _stStr => _st as IStringSymbolTable<int>;

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
        public void TestRandomlyPutGetDelete()
        {
            string[] textList = Util.GenerateDynamicLengthString(_alphabet.Charcters, _rowCount, 15);
            HashSet<string> addedList = new HashSet<string>();
            for (int i = 0; i < _rowCount * 4; i++)
            {
                string text = textList[Util.Ran.Next(0, textList.Length)];
                switch (Util.Ran.Next(0, 3))
                {
                    case 0:
                        {
                            addedList.Add(text);
                            _st.Put(text, 1);
                            break;
                        }
                    case 1:
                        {
                            int value = _st.Get(text);
                            if (addedList.Contains(text))
                                Assert.AreEqual(1, value);
                            else
                                Assert.AreEqual(0, value);
                            break;
                        }
                    case 2:
                        {
                            addedList.Remove(text);
                            _st.Delete(text);
                            break;
                        }
                }
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

            foreach (string text in stringList)
            {
                if (count % 500 == 0)
                    Assert.AreEqual(count, _st.Size());

                _st.Delete(text);
                count--;
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
        public void TestKeysWithPrefixSimple()
        {
            string[] textList = { "ABC", "ABD", "ACE", "ZYX" };
            foreach (string text in textList)
                _st.Put(text, 1);

            string[] keys = _stStr.KeysWithPrefix("AB").OrderBy(x => x).ToArray();
            Assert.AreEqual("ABC", keys[0]);
            Assert.AreEqual("ABD", keys[1]);
        }

        [TestMethod]
        public void TestKeysWithPrefix()
        {
            _rowCount /= 2;

            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 20);
            foreach (string text in stringList)
                _st.Put(text, 1);

            HashSet<string> prefixList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 100, 6);
            foreach (string prefix in prefixList)
            {
                HashSet<string> correct = stringList.Where(x => x.StartsWith(prefix)).ToHashSet();
                Assert.IsTrue(correct.SetEquals(_stStr.KeysWithPrefix(prefix)));
            }
        }

        [TestMethod]
        public void TestLongestPrefixOfSimple()
        {
            string[] textList = { "abc", "abcd", "abce", "zyx" };
            foreach (string text in textList)
                _stStr.Put(text, 1);

            string prefix = _stStr.LongestPrefixOf("abcdef");
            Assert.AreEqual("abcd", prefix);
        }

        [TestMethod]
        public void TestLongestPrefixOf()
        {
            _rowCount *= 5;
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 5).ToHashSet();
            stringList.Remove("");

            foreach (string text in stringList)
                _st.Put(text, 1);

            HashSet<string> longStringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 20, 20);
            foreach (string longStr in longStringList)
            {
                string correct = "";
                foreach (string text in stringList)
                    if (longStr.StartsWith(text) && text.Length > correct.Length)
                        correct = text;

                Assert.AreEqual(correct, _stStr.LongestPrefixOf(longStr));
            }
        }

        [TestMethod]
        public void KeysThatMatchSimple()
        {
            string[] textList = { "ABC", "ADC", "1EC", "ABD" };
            foreach (string text in textList)
                _st.Put(text, 1);

            string[] keys = _stStr.KeysThatMatch("A.C").OrderBy(x => x).ToArray();
            Assert.AreEqual("ABC", keys[0]);
            Assert.AreEqual("ADC", keys[1]);
        }

        [TestMethod]
        public void TestKeysThatMatch()
        {
            _rowCount *= 2;
            HashSet<string> stringList = Util.GenerateFixedLengthString_Distinct(_alphabet.Charcters, _rowCount, 4);
            foreach (string text in stringList)
                _st.Put(text, 1);

            HashSet<string> patternList = Util.GenerateFixedLengthString_Distinct(_alphabet.Charcters, 25, 4);
            foreach (string pattern in patternList)
            {
                char[] temp = pattern.ToCharArray();
                temp[Util.Ran.Next(0, temp.Length - 1)] = '.';
                string regexText = new string(temp);
                Regex regex = new Regex(regexText);

                HashSet<string> correct = stringList.Where(x => regex.IsMatch(x)).ToHashSet<string>();
                Assert.IsTrue(correct.SetEquals(_stStr.KeysThatMatch(regexText)));
            }
        }
    }

    public class TestStringSTCert : TestStringST
    {
        protected int _rowCount4Cert = 300;

        [TestMethod]
        public void TestPutGet_Cert()
        {
            _rowCount = _rowCount4Cert;
            _st = new CertWrapper4ST<ISymbolTable<string, int>, string, int>(_st);
            TestPutGet();
        }

        [TestMethod]
        public void TestDelete_Cert()
        {
            _rowCount = _rowCount4Cert;
            _st = new CertWrapper4ST<ISymbolTable<string, int>, string, int>(_st);
            TestDelete();
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

    [TestClass]
    public class TestTST : TestStringSTCert
    {
        public TestTST()
        {
            _st = new TST<int>();
        }
    }

    [TestClass]
    public class TestTST_Empty : TestStringSTCert
    {
        public TestTST_Empty()
        {
            _st = new TST_Empty<int>();
        }
    }

    [TestClass]
    public class TestTrie_Ordered : TestStringSTCert
    {
        protected Trie_Ordered<int> _trieO => _st as Trie_Ordered<int>;

        public TestTrie_Ordered()
        {
            _st = new Trie_Ordered<int>(_alphabet);
        }

        [TestMethod]
        public void TestRankSimple()
        {
            _trieO.Put("A", 1);
            _trieO.Put("C", 1);
            _trieO.Put("E", 1);
            Assert.AreEqual(1, _trieO.Rank("B"));
            Assert.AreEqual(1, _trieO.Rank("C"));
            Assert.AreEqual(2, _trieO.Rank("D"));
            Assert.AreEqual(3, _trieO.Rank("F"));

            _trieO.Put("CB", 1);
            _trieO.Put("CD", 1);

            Assert.AreEqual(1, _trieO.Rank("C"));
            Assert.AreEqual(2, _trieO.Rank("CA"));
            Assert.AreEqual(3, _trieO.Rank("CC"));
            Assert.AreEqual(3, _trieO.Rank("CD"));
            Assert.AreEqual(4, _trieO.Rank("CE"));
            Assert.AreEqual(4, _trieO.Rank("E"));
            Assert.AreEqual(5, _trieO.Rank("F"));

            _trieO.Put("", 1);
            Assert.AreEqual(1, _trieO.Rank("A"));
        }

        [TestMethod]
        public void TestRank()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _st.Put(text, 1);

            Array.Sort(textList);
            for (int i = 0; i < _rowCount / 2; i++)
            {
                int index = Util.Ran.Next(0, _rowCount);
                Assert.AreEqual(index, _trieO.Rank(textList[index]));
            }
        }

        [TestMethod]
        public void TestSelectSimple()
        {
            _trieO.Put("", 1);
            _trieO.Put("A", 1);
            _trieO.Put("C", 1);
            _trieO.Put("E", 1);
            _trieO.Put("CB", 1);
            _trieO.Put("CD", 1);

            Assert.AreEqual("", _trieO.Select(0));
            Assert.AreEqual("A", _trieO.Select(1));
            Assert.AreEqual("C", _trieO.Select(2));
            Assert.AreEqual("CB", _trieO.Select(3));
            Assert.AreEqual("CD", _trieO.Select(4));
            Assert.AreEqual("E", _trieO.Select(5));
            Assert.IsNull(_trieO.Select(6));
        }

        [TestMethod]
        public void TestSelect()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _st.Put(text, 1);

            Array.Sort(textList);
            for (int i = 0; i < _rowCount; i++)
            {
                int index = Util.Ran.Next(0, _rowCount);
                Assert.AreEqual(textList[index], _trieO.Select(index));
            }
        }

        [TestMethod]
        public void TestMinSimple()
        {
            _trieO.Put("E", 1);
            Assert.AreEqual("E", _trieO.Min());
            _trieO.Put("CD", 1);
            Assert.AreEqual("CD", _trieO.Min());
            _trieO.Put("CB", 1);
            Assert.AreEqual("CB", _trieO.Min());
            _trieO.Put("C", 1);
            Assert.AreEqual("C", _trieO.Min());
            _trieO.Put("A", 1);
            Assert.AreEqual("A", _trieO.Min());
            _trieO.Put("", 1);
            Assert.AreEqual("", _trieO.Min());
        }

        [TestMethod]
        public void TestMin()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _trieO.Put(text, 1);

            Array.Sort(textList);
            foreach (string text in textList)
            {
                Assert.AreEqual(text, _trieO.Min());
                _trieO.Delete(text);
            }
        }

        [TestMethod]
        public void TestMaxSimple()
        {
            _trieO.Put("", 1);
            Assert.AreEqual("", _trieO.Max());
            _trieO.Put("A", 1);
            Assert.AreEqual("A", _trieO.Max());
            _trieO.Put("C", 1);
            Assert.AreEqual("C", _trieO.Max());
            _trieO.Put("CB", 1);
            Assert.AreEqual("CB", _trieO.Max());
            _trieO.Put("CD", 1);
            Assert.AreEqual("CD", _trieO.Max());
            _trieO.Put("E", 1);
            Assert.AreEqual("E", _trieO.Max());
        }

        [TestMethod]
        public void TestMax()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _trieO.Put(text, 1);

            Array.Sort(textList);
            foreach (string text in textList.Reverse())
            {
                Assert.AreEqual(text, _trieO.Max());
                _trieO.Delete(text);
            }
        }

        [TestMethod]
        public void TestCeilingSimple()
        {
            _trieO.Put("", 1);
            _trieO.Put("A", 1);
            _trieO.Put("C", 1);
            _trieO.Put("E", 1);
            _trieO.Put("CB", 1);
            _trieO.Put("CD", 1);

            Assert.AreEqual("CB", _trieO.Ceiling("CB"));
            Assert.AreEqual("E", _trieO.Ceiling("E"));
            //Assert.AreEqual("", _trieO.Ceiling(""));

            Assert.AreEqual("CB", _trieO.Ceiling("CA"));
            Assert.AreEqual("CD", _trieO.Ceiling("CC"));
            Assert.AreEqual("C", _trieO.Ceiling("B"));
            Assert.AreEqual("E", _trieO.Ceiling("CE"));
            Assert.IsNull(_trieO.Ceiling("F"));
        }
    }

    [TestClass]
    public class TestTST_Ordered : TestStringSTCert
    {
        public TestTST_Ordered()
        {
            _st = new TST_Ordered<int>();
        }
    }
}