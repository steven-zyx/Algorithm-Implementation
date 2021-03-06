using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using Sorting;
using String;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utils;
using System.Text;
using System.Reflection;


namespace AlgorithmUnitTest.TestString
{
    public abstract class TestStringBase
    {
        protected Alphabet _alphabet;

        public TestStringBase()
        {
            _alphabet = new Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
    }

    [TestClass]
    public class TestString : TestStringBase
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

        private void DoTestRegex(string regex, string[] matchCases, string[] unmatchCases)
        {
            NFA client = new NFA(regex);
            foreach (string text in matchCases)
                Assert.IsTrue(client.Recognize(text));
            foreach (string text in unmatchCases)
                Assert.IsFalse(client.Recognize(text));
        }

        private void DoTestRegex2(string regex, string[] matchCases, string[] unmatchCases)
        {
            Regex client = new Regex(regex);
            foreach (string text in matchCases)
                Assert.IsTrue(client.IsMatch(text));
            foreach (string text in unmatchCases)
                Assert.IsFalse(client.IsMatch(text));
        }

        [TestMethod]
        public void TestRegex()
        {
            DoTestRegex("X(AB|CD)*Y",
                new string[] { "XABY", "XCDY", "XABABCDCDY", "XCDABABCDY", "XY" },
                new string[] { "AB", "XXYY", "Y", "X", "XAB", "ABY" });

            DoTestRegex("X(A|B|C)*Y",
                new string[] { "XABCBCACBY", "XY", "XAAAAAAAAY" },
                new string[] { "XDY", "XA", "AY", "" });

            DoTestRegex("A.*B",
                new string[] { "AXFSDFVCVSDFB", "AB", "AAAAABBBBB" },
                new string[] { "B", "A", "1B", "A1", "" });

            DoTestRegex("A.+B",
                new string[] { "AXFSDFVCVSDFB", "A1B", "AAAAABBBBB" },
                new string[] { "AB", "B", "A", "1B", "A1", "" });

            DoTestRegex("A[XYZ]B",
                new string[] { "AXB", "AYB", "AZB" },
                new string[] { "AB", "A1B", "AX", "XB", "X", "" });

            DoTestRegex("AB?C",
                new string[] { "ABC", "AC" },
                new string[] { "AB", "BC", "", "A1C" });

            DoTestRegex("A(BC)?D",
                new string[] { "ABCD", "AD" },
                new string[] { "ABC", "A", "BCD", "D", "", "AXYD" });

            DoTestRegex("AB{3}C",
                new string[] { "ABBBC" },
                new string[] { "ABBC", "ABC", "AC", "AB", "ABB", "BC", "BBC", "" });

            DoTestRegex("A(BC){2}D",
                new string[] { "ABCBCD" },
                new string[] { "AD", "ABCD", "ABCBCBCD", "ABCBC", "BCBCD" });

            DoTestRegex("AB{3,}C",
                new string[] { "ABBBC", "ABBBBBBBBBC" },
                new string[] { "ABC", "ABBC", "AC", "" });

            DoTestRegex("A(BC){2,}D",
                new string[] { "ABCBCD", "ABCBCBCBCD" },
                new string[] { "ABCD", "ABC", "BCD", "AD", "" });

            DoTestRegex("AB{3,5}C",
                new string[] { "ABBBC", "ABBBBC", "ABBBBBC" },
                new string[] { "ABC", "ABBC", "ABBBBBBC", "AB", "BC", "AC", "" });

            DoTestRegex("A(BC){2,4}D",
                new string[] { "ABCBCD", "ABCBCBCD", "ABCBCBCBCD" },
                new string[] { "ABCD", "ABCBCBCBCBCD", "AD", "ABC", "BCD", "" });

            DoTestRegex("A[BCD]{2,4}E",
                new string[] { "ABCE", "ADCBE", "ABCDBE", "ACCCCE", "ADDE" },
                new string[] { "AE", "ABCDBCE", "ABE", "ACE", "ABCDBCDE", "", "ABC", "CDE" });

            DoTestRegex("A[0-9]B",
                new string[] { "A1B", "A9B" },
                new string[] { "AB", "ACB", "A1", "1B", "" });

            DoTestRegex("A[0b-x1]B",
                new string[] { "A1B", "A0B", "AbB", "AxB", "AeB" },
                new string[] { "A2B", "A9B", "AaB", "AzB", "AAB", "AZB", "A0", "0B", "AB", "" });

            DoTestRegex("A[^BC]D",
                new string[] { "A1D", "A9D", "ADD", "AZD" },
                new string[] { "ABD", "ACD", "A1", "1D", "AD", "" });

            DoTestRegex("A[^b-y09]B",
                new string[] { "AaB", "AzB", "A1B", "A8B" },
                new string[] { "AbB", "AeB", "AyB", "A0B", "A9B", "AB", "", "Aa", "aB" });
        }

        [TestMethod]
        public void TestOneLevelREs()
        {
            string character = @"[^|*()]";
            string closure = @"([^|*()]+\*)";
            string pipe = @"(\|[^|*()]+)";
            string brackets = string.Format(@"\(({0}|{1})+({0}|{1}|{2})*\)", character, closure, pipe);
            string regex = string.Format(@"^(\|?{3}\*?|{0}|{1}|{2})*$", character, closure, pipe, brackets);

            string[] matchCases = new string[]
            {
                @"(0.*1)*",
                @"(1.*0)*",
                @"01.*|(01|10)*"
            };
            string[] unmatchCases = new string[]
            {
                @"0(|0)1",
                @"0(0|)1",
                @"01.**10",
                @"01.||10",
                @"01.|*10",
                @"01(01|(10|1))",
                @"01(*1)10"
            };

            DoTestRegex2(regex, matchCases, unmatchCases);
        }

        [TestMethod]
        public void ChallengingREs()
        {
            DoTestRegex2(@"^1?(01|0)*$",
                new string[] { "01010000", "", "0000" },
                new string[] { "110", "011", "11" });

            DoTestRegex2(@"^((1?0{2,})|(0{1,}1?0{1,})|(0{2,}1?))$",
                new string[] { "001", "010", "100", "0000001000000", "000001", "100000" },
                new string[] { "11", "1100", "10" });

            DoTestRegex2(@"^(1.)*1?$",
                new string[] { "1010", "1111", "10101", "111010", "" },
                new string[] { "0000", "0101", "1000" });

            string no11 = "(^10.*1$)";
            string no111 = "(^11.+1$)|(^1.+11$)";
            string others = "(^0.*$)|(^1.*0$)|(^$)";
            DoTestRegex2(no11 + "|" + no111 + "|" + others,
                new string[] { "011", "110", "0111", "1110", "1111", "101", "1011", "1101", "11011", "11101110" },
                new string[] { "11", "111" });
        }

        [TestMethod]
        public void TestBinaryDivisibility()
        {
            DoTestRegex2(@"^.*0$",
                new string[] { "1100", "0110", "11110" },
                new string[] { "", "111", "001", "1000001" });
        }
    }

    [TestClass]
    public class TestCompression : TestStringBase
    {
        [TestMethod]
        public void TestFixedLengthEncodingSimple()
        {
            string testFile = Util.DesktopPath + @"TestOutput.tsv";
            int length = 2;
            int[] numbers = { 2, 1, 0, 3, 2 };

            using (BinaryStdOut output = new BinaryStdOut(testFile))
                foreach (int n in numbers)
                    output.Write(n, length);

            using (BinaryStdIn input = new BinaryStdIn(testFile))
                foreach (int n in numbers)
                    Assert.AreEqual(n, input.ReadInt(length));

            File.Delete(testFile);
        }

        [TestMethod]
        public void TestFixedLengthEncoding()
        {
            _alphabet = new Alphabet("ACTG");
            string content = Util.GenerateLongString(_alphabet.Charcters, 3_000_000);
            string testFile = Util.DesktopPath + "testFile.txt";
            int length = _alphabet.LgR();

            using (BinaryStdOut output = new BinaryStdOut(testFile))
            {
                foreach (char c in content)
                    output.Write(_alphabet.ToIndex(c), length);
            }

            string expanded;
            using (BinaryStdIn input = new BinaryStdIn(testFile))
            {
                char[] value = new char[content.Length];
                for (int i = 0; i < content.Length; i++)
                    value[i] = _alphabet.ToChar(input.ReadInt(length));
                expanded = new string(value);
            }

            Assert.AreEqual(content, expanded);
            File.Delete(testFile);
        }

        protected byte[] CompressAndExpand<T>(byte[] source) where T : ICompression, new()
        {
            string sourceFile = Util.DesktopPath + "Source.tsv";
            string compressedFile = Util.DesktopPath + "compressedFile.tsv";
            string expandedFile = Util.DesktopPath + "expandedFile.tsv";
            File.Delete(sourceFile);
            File.Delete(compressedFile);
            File.Delete(expandedFile);

            File.WriteAllBytes(sourceFile, source);
            T client = new T();
            client.Compress(sourceFile, compressedFile);
            client.Expand(compressedFile, expandedFile);
            byte[] expanded = File.ReadAllBytes(expandedFile);

            File.Delete(sourceFile);
            File.Delete(compressedFile);
            File.Delete(expandedFile);
            return expanded;
        }

        [TestMethod]
        public void TestRunLengthEncodingSimple()
        {
            string source = "ABCDEFG";
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<RunLengthEncoding>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestRunLengthEncoding()
        {
            BitArray source = Util.GenerateRandomBits(4_000_000);
            byte[] sourceByte = new byte[source.Length / 8];
            source.CopyTo(sourceByte, 0);
            BitArray expanded = new BitArray(CompressAndExpand<RunLengthEncoding>(sourceByte));

            for (int i = 0; i < source.Length; i++)
                Assert.AreEqual(source[i], expanded[i]);
        }

        [TestMethod]
        public void TestHaffmanSimple()
        {
            string source = "ABCDEFG";
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<Haffman>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestHaffman()
        {
            string source = Util.GenerateLongString(_alphabet.Charcters, 400_000);
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<Haffman>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestLZWSimple()
        {
            string source = "ABABABA";
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<LZW>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestLZW()
        {
            string source = Util.GenerateLongString(_alphabet.Charcters, 400_000);
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<LZW>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestFixedLengthEncoding_TrieSimple()
        {
            string source = "ABC";
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<FixedLengthEncoding_Trie>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);

            source = Util.GenerateLongString("ABCDE".ToCharArray(), 100_000);
            expanded = Encoding.ASCII.GetString(CompressAndExpand<FixedLengthEncoding_Trie>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestFixedLengthEncoding_Trie()
        {
            string source = Util.GenerateLongString(_alphabet.Charcters, 400_000);
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<FixedLengthEncoding_Trie>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }

        [TestMethod]
        public void TestLZW_Rebuilding()
        {
            string source = Util.GenerateLongString(_alphabet.Charcters, 400_000);
            string expanded = Encoding.ASCII.GetString(CompressAndExpand<LZW_Rebuilding>(Encoding.ASCII.GetBytes(source)));
            Assert.AreEqual(source, expanded);
        }
    }

    [TestClass]
    public class TestSubstringSearch : TestStringBase
    {
        protected void DoSubstringSearch<T>() where T : SubStringSearch
        {
            SubStringSearch client = null;
            for (int i = 0; i < 100_000; i++)
            {
                string text = Util.GenerateLongString(_alphabet.Charcters, 1_000);
                string pattern = Util.GenerateLongString(_alphabet.Charcters, 3);
                int correct = text.IndexOf(pattern);

                client = Activator.CreateInstance(typeof(T), pattern) as SubStringSearch;
                int index = client.Search(text);

                if (correct >= 0)
                    Assert.AreEqual(correct, index);
                else
                    Assert.AreEqual(text.Length, index);
            }
        }

        protected void DoBinaryStdInSearch<T>() where T : SubStringSearch
        {
            string sourceFile = Util.DesktopPath + "sourcefile.txt";
            SubStringSearch client = null;
            for (int i = 0; i < 2_000; i++)
            {
                string text = Util.GenerateLongString(_alphabet.Charcters, 1_000);
                string pattern = Util.GenerateLongString(_alphabet.Charcters, 3);
                int correct = text.IndexOf(pattern);

                File.WriteAllText(sourceFile, text);
                client = Activator.CreateInstance(typeof(T), pattern) as SubStringSearch;
                using (BinaryStdIn input = new BinaryStdIn(sourceFile))
                {
                    int index = client.Search(input);
                    if (correct >= 0)
                        Assert.AreEqual(correct, index);
                    else
                        Assert.AreEqual(text.Length, index);
                }
                File.Delete(sourceFile);
            }
        }

        protected void DoSubStringFindAll<T>() where T : SubStringSearch
        {
            SubStringSearch client = null;
            for (int i = 0; i < 100_000; i++)
            {
                string text = Util.GenerateLongString(_alphabet.Charcters, 1_000);
                string pattern = Util.GenerateLongString(_alphabet.Charcters, 3);

                client = Activator.CreateInstance(typeof(T), pattern) as SubStringSearch;
                int correct = -1;
                foreach (int position in client.FindAll(text).ToArray())
                {
                    correct = text.IndexOf(pattern, correct + 1);
                    Assert.AreEqual(correct, position);
                }
                Assert.AreEqual(-1, text.IndexOf(pattern, correct + 1));
            }
        }

        [TestMethod]
        public void TestBruteSearch1() => DoSubstringSearch<BruteSearch1>();

        [TestMethod]
        public void TestBruteSearch1_FindAll() => DoSubStringFindAll<BruteSearch1>();

        [TestMethod]
        public void TestBruteSearch1_BinaryStdIn() => DoBinaryStdInSearch<BruteSearch1>();

        [TestMethod]
        public void TestBruteSearch2() => DoSubstringSearch<BruteSearch2>();

        [TestMethod]
        public void TestBruteSearch2_FindAll() => DoSubStringFindAll<BruteSearch2>();

        [TestMethod]
        public void TestKMP() => DoSubstringSearch<KMP>();

        [TestMethod]
        public void TestKMP_BinaryStdIn() => DoBinaryStdInSearch<KMP>();

        [TestMethod]
        public void TestKMP_FindAll() => DoSubStringFindAll<KMP>();

        [TestMethod]
        public void TestKMP_Simple()
        {
            string pattern = "ABABAC";
            KMP client = new KMP(pattern);

            Assert.AreEqual(0, client.Search("ABABAC"));
            Assert.AreEqual(6, client.Search("ABABAD"));
            Assert.AreEqual(2, client.Search("ABABABACAC"));
        }

        [TestMethod]
        public void TestBoyerMoore() => DoSubstringSearch<BoyerMoore>();

        [TestMethod]
        public void TestBoyerMoore_BinaryStdIn() => DoBinaryStdInSearch<BoyerMoore>();

        [TestMethod]
        public void TestBoyerMoore_FindAll() => DoSubStringFindAll<BoyerMoore>();

        [TestMethod]
        public void TestRabinKarp() => DoSubstringSearch<RabinKarp>();

        [TestMethod]
        public void TestRabinKarp_BinaryStdIn() => DoBinaryStdInSearch<RabinKarp>();

        [TestMethod]
        public void TestRabinKarp_FindAll() => DoSubStringFindAll<RabinKarp>();

        [TestMethod]
        public void TestRabinKarp_Simple()
        {
            string pattern = "AB";
            RabinKarp client = new RabinKarp(pattern);
            Assert.AreEqual(0, client.Search("ABCD"));
            Assert.AreEqual(1, client.Search("0AB"));
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
        public void TestPutGetSimple()
        {
            string key = "AB";
            _st.Put(key, 8);
            Assert.AreEqual(8, _st.Get(key));
            _st.Put(key, -8);
            Assert.AreEqual(-8, _st.Get(key));

            List<KeyValuePair<string, int>> testData = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("A",1),
                new KeyValuePair<string, int>("ABCDE",2),
                new KeyValuePair<string, int>("ABCDEFG",3),
                new KeyValuePair<string, int>("ABCXYZ",4),
                new KeyValuePair<string, int>("OBCXYZ",5),
                new KeyValuePair<string, int>("ABCXYW",6),
                new KeyValuePair<string, int>("AUC",7),
            };

            foreach (var pair in testData)
            {
                _st.Put(pair.Key, pair.Value);
                Assert.AreEqual(pair.Value, _st.Get(pair.Key));
            }
            foreach (var pair in testData)
                Assert.AreEqual(pair.Value, _st.Get(pair.Key));
        }

        [TestMethod]
        public void TestPutGet()
        {
            int length = 15;
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, length);
            Dictionary<string, int> dict = stringList.ToDictionary(x => x, y => Util.Ran.Next(1, int.MaxValue));

            for (int i = 0; i < 2; i++)
            {
                foreach (var pair in dict)
                    _st.Put(pair.Key, pair.Value);
                foreach (var pair in dict)
                    Assert.AreEqual(pair.Value, _st.Get(pair.Key));

                HashSet<string> otherStringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 100, length);
                foreach (string text in otherStringList)
                    if (!dict.ContainsKey(text))
                        Assert.AreEqual(0, _st.Get(text));
            }
        }

        [TestMethod]
        public void TestDeleteSimple()
        {
            List<KeyValuePair<string, int>> testData = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("ABCXYZ",1),
                new KeyValuePair<string, int>("ABCD",2),
                new KeyValuePair<string, int>("ABCX",3),
                new KeyValuePair<string, int>("ABCDEF",4),
            };
            testData.ForEach(x => _st.Put(x.Key, x.Value));

            for (int i = 0; i < testData.Count; i++)
            {
                Assert.IsTrue(_st.Contains(testData[i].Key));
                _st.Delete(testData[i].Key);
                Assert.IsFalse(_st.Contains(testData[i].Key));
                Assert.AreEqual(0, _st.Get(testData[i].Key));
                for (int j = i + 1; j < testData.Count; j++)
                    Assert.AreEqual(testData[j].Value, _st.Get(testData[j].Key));
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

        public void TestDeleteRandomlyRecrusive()
        {
            while (true)
            {
                _st = new TST_NoOneWayBranching<int>();

                HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 5, 3);
                stringList.Remove("");

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
        }

        [TestMethod]
        public void TestDeleteRandomly()
        {
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            stringList.Remove("");
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

        public void TestKeysRecrusive()
        {
            while (true)
            {
                _st = new Trie_NoOneWayBranching<int>(_alphabet);
                HashSet<string> testData = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 5, 3);
                foreach (string text in testData)
                    _st.Put(text, 1);

                Assert.IsTrue(testData.SetEquals(_st.Keys()));
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
        protected TrieST<int> TrieST => _st as TrieST<int>;

        public TestTrie()
        {
            _st = new TrieST<int>(_alphabet);
        }

        [TestMethod]
        public void TestContainsPrefix()
        {
            HashSet<string> stringList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15);
            foreach (string str in stringList)
                _st.Put(str, 1);

            HashSet<string> prefixList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, 200, 3);
            foreach (string pf in prefixList)
            {
                bool ifContains = stringList.Any(x => x.StartsWith(pf));
                Assert.AreEqual(ifContains, TrieST.ContainsPrefix(pf));
            }
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
            Assert.AreEqual("", _trieO.Ceiling(""));

            Assert.AreEqual("CB", _trieO.Ceiling("CA"));
            Assert.AreEqual("CD", _trieO.Ceiling("CC"));
            Assert.AreEqual("C", _trieO.Ceiling("B"));
            Assert.AreEqual("E", _trieO.Ceiling("CE"));
            Assert.IsNull(_trieO.Ceiling("F"));
        }

        [TestMethod]
        public void TestCeiling()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _trieO.Put(text, 1);

            Array.Sort(textList);
            for (int i = 0; i < _rowCount - 1; i++)
            {
                _trieO.Delete(textList[i]);
                Assert.AreEqual(textList[i + 1], _trieO.Ceiling(textList[i]));
            }
        }

        [TestMethod]
        public void TestFloorSimple()
        {
            _trieO.Put("", 1);
            _trieO.Put("A", 1);
            _trieO.Put("C", 1);
            _trieO.Put("E", 1);
            _trieO.Put("CB", 1);
            _trieO.Put("CD", 1);

            Assert.AreEqual("CB", _trieO.Floor("CB"));
            Assert.AreEqual("E", _trieO.Floor("E"));
            Assert.AreEqual("", _trieO.Floor(""));

            Assert.AreEqual("", _trieO.Floor("9"));
            Assert.AreEqual("A", _trieO.Floor("B"));
            Assert.AreEqual("C", _trieO.Floor("CA"));
            Assert.AreEqual("CB", _trieO.Floor("CC"));
            Assert.AreEqual("CD", _trieO.Floor("CE"));
            Assert.AreEqual("CD", _trieO.Floor("D"));
            Assert.AreEqual("E", _trieO.Floor("F"));
            Assert.AreEqual("E", _trieO.Floor("FA"));

            _trieO.Delete("");
            Assert.IsNull(_trieO.Floor("9"));
        }

        [TestMethod]
        public void TestFloor()
        {
            string[] textList = Util.GenerateDynamicLengthString_Distinct(_alphabet.Charcters, _rowCount, 15).ToArray();
            foreach (string text in textList)
                _trieO.Put(text, 1);

            Array.Sort(textList);
            for (int i = _rowCount - 1; i > 0; i--)
            {
                _trieO.Delete(textList[i]);
                Assert.AreEqual(textList[i - 1], _trieO.Floor(textList[i]));
            }
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

    [TestClass]
    public class TestTrie_NoOneWayBranching : TestStringSTCert
    {
        public TestTrie_NoOneWayBranching()
        {
            _st = new Trie_NoOneWayBranching<int>(_alphabet);
        }
    }

    [TestClass]
    public class TestTST_NoOneWayBranching : TestStringSTCert
    {
        public TestTST_NoOneWayBranching()
        {
            _st = new TST_NoOneWayBranching<int>();
        }
    }

    [TestClass]
    public class TestHybridST : TestStringSTCert
    {
        public TestHybridST()
        {
            _st = new HybridST<int>(_alphabet);
        }
    }
}