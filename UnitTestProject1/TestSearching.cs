using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using System;
using System.Collections.Generic;
using Utils;
using System.Linq;
using BasicDataStrcture;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSearching
    {
        [TestMethod]
        public void TestPerfectbalance()
        {
            BST_CountGet<int, int> bst = new BST_CountGet<int, int>();

            Queue<List<int>> sourceList = new Queue<List<int>>();
            int count = 7;
            sourceList.Enqueue(Enumerable.Range(0, count).ToList());

            while (sourceList.Count > 0)
            {
                List<int> source = sourceList.Dequeue();
                int mid = source.Count() / 2;

                bst.Put(source[mid], mid);

                var left = source.GetRange(0, mid);
                if (left.Count() > 0)
                    sourceList.Enqueue(left);

                var right = source.GetRange(mid + 1, source.Count - mid - 1);
                if (right.Count() > 0)
                    sourceList.Enqueue(right);
            }
        }

        [TestMethod]
        public void TestAll()
        {
            MathSet<int> ms = new MathSet<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            ms.Add(1);
            ms.Add(2);
            Assert.IsTrue(ms.Contains(1));
            Assert.IsTrue(ms.Contains(2));
            Assert.AreEqual(8, ms.Complement().Count());

            ms.Union(new int[] { 2, 3, 4 });
            Assert.AreEqual(4, ms.Keys().Count());

            ms.Delete(2);
            Assert.AreEqual(3, ms.Keys().Count());

            ms.Intersection(new int[] { 3, 4, 5, 6 });
            Assert.AreEqual(2, ms.Keys().Count());
        }
    }

    public class TestSymbolTable
    {
        protected ISymbolTable<int, int> _ST_Int;
        protected int _rowCount;

        public TestSymbolTable()
        {
            _rowCount = 10_000;
        }

        [TestMethod]
        public virtual void Test_PutOne_GetOne()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < _rowCount; i++)
            {
                _ST_Int.Put(source[i], i);
                Assert.AreEqual(i, _ST_Int.Get(source[i]));
            }
        }

        [TestMethod]
        public virtual void Test_PutOne_GetAll()
        {
            _rowCount = _rowCount / 20;
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < _rowCount; i++)
            {
                _ST_Int.Put(source[i], i);
                for (int j = 0; j <= i; j++)
                {
                    Assert.AreEqual(j, _ST_Int.Get(source[j]));
                }
            }
        }

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Resize()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < _rowCount; i++)
                    _ST_Int.Put(source[i], i);
                for (int i = 0; i < _rowCount; i++)
                    Assert.AreEqual(i, _ST_Int.Get(source[i]));
                for (int i = 0; i < _rowCount; i++)
                    Assert.IsTrue(_ST_Int.Delete(source[i]));
            }
        }

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Intermixed()
        {
            int[] source = Util.GenerateRandomArrayRepeat(0, _rowCount, 5);

            HashSet<int> insertedKeys = new HashSet<int>();
            foreach (int i in source)
            {
                int choice = Util.Ran.Next(0, 3);
                switch (choice)
                {
                    case 0:
                        {
                            _ST_Int.Put(i, i);
                            insertedKeys.Add(i);
                            break;
                        }
                    case 1:
                        {

                            int value = _ST_Int.Get(i);
                            if (insertedKeys.Contains(i))
                                Assert.AreEqual(i, value);
                            break;
                        }
                    case 2:
                        {
                            bool deleteResult = _ST_Int.Delete(i);
                            if (insertedKeys.Contains(i))
                            {
                                Assert.IsTrue(deleteResult);
                                insertedKeys.Remove(i);
                            }
                            else
                                Assert.IsFalse(deleteResult);
                            break;
                        }
                }
            }
        }

        [TestMethod]
        public virtual void Test_Contains()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(source[i], i);

            for (int i = 0; i < _rowCount; i++)
            {
                int index = Util.Ran.Next(0, _rowCount * 2);
                if (index < _rowCount)
                    Assert.IsTrue(_ST_Int.Contains(index));
                else
                    Assert.IsFalse(_ST_Int.Contains(index));
            }
        }
    }

    public class TestSymbolTableCert : TestSymbolTable
    {
        protected int _rowCount4Cert = 300;

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Resize_Cert()
        {
            _rowCount = _rowCount4Cert;
            _ST_Int = new CertWrapper4ST<ISymbolTable<int, int>, int, int>(_ST_Int);
            Test_Get_Put_Delete_Resize();
        }

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Intermixed_Cert()
        {
            _rowCount = _rowCount4Cert;
            _ST_Int = new CertWrapper4ST<ISymbolTable<int, int>, int, int>(_ST_Int);
            Test_Get_Put_Delete_Intermixed();
        }
    }

    public class TestMultiSymbolTable : TestSymbolTable
    {
        protected IMultiSymbolTable<int, int> MST_Int => _ST_Int as IMultiSymbolTable<int, int>;

        internal void SetTestObj(ISymbolTable<int, int> st) => _ST_Int = st;

        [TestMethod]
        public virtual void TestMultipleKey()
        {
            MST_Int.Put(1, 0);
            MST_Int.Put(2, 0);
            MST_Int.Put(2, -1);
            MST_Int.Put(2, -2);
            MST_Int.Put(3, 0);

            Assert.AreEqual(5, MST_Int.Size());
            Assert.IsTrue(MST_Int.Delete(1));
            Assert.IsFalse(MST_Int.Delete(4));

            List<int> values = MST_Int.GetAll(2).ToList();
            Assert.IsTrue(values.Contains(0));
            Assert.IsTrue(values.Contains(-1));
            Assert.IsTrue(values.Contains(-2));

            Assert.IsTrue(MST_Int.Delete(2));
            Assert.AreEqual(2, MST_Int.GetAll(2).Count());

            Assert.AreEqual(2, MST_Int.DeleteAll(2));
            Assert.AreEqual(0, MST_Int.GetAll(2).Count());
        }

        public override void Test_Get_Put_Delete_Intermixed() { }
    }

    public class TestHashST : TestSymbolTable
    {
        protected HashST<Trasaction, int> _ST_Trans;

        [TestMethod]
        public void TestHashCache()
        {
            DateTime now = DateTime.UtcNow;
            for (int i = 0; i < 100; i++)
            {
                var trans = new Trasaction(i.ToString(), now.AddDays(-i), i);
                _ST_Trans.Put(trans, i);
            }
            foreach (Trasaction tran in _ST_Trans.Keys())
                Assert.AreEqual(tran.Amount, _ST_Trans.Get(tran));
        }
    }

    public class TestOrderedSymbolTable : TestSymbolTable
    {
        protected IOrderedSymbolTable<int, int> OST_Int => _ST_Int as IOrderedSymbolTable<int, int>;

        public TestOrderedSymbolTable()
        {
            _rowCount = 50_000;
        }

        [TestMethod]
        public virtual void Test_Min_Max()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                OST_Int.Put(source[i], source[i]);

            int currentMin = -1;
            int currentMax = int.MaxValue;

            while (!OST_Int.IsEmpty)
            {
                Assert.IsTrue(currentMin < OST_Int.Min());
                currentMin = OST_Int.Min();
                OST_Int.DeleteMin();

                Assert.IsTrue(currentMax > OST_Int.Max());
                currentMax = OST_Int.Max();
                OST_Int.DeleteMax();
            }
        }

        [TestMethod]
        public virtual void Test_Floor_Ceiling()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                OST_Int.Put(source[i] * 2, source[i] * 2);

            for (int i = 1; i < _rowCount * 2 - 1; i += 2)
            {
                Assert.AreEqual(i - 1, OST_Int.Floor(i));
                Assert.AreEqual(i + 1, OST_Int.Ceiling(i));
            }
        }

        [TestMethod]
        public virtual void Test_Keys2()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                OST_Int.Put(source[i], i);

            int end = Util.Ran.Next(0, _rowCount);
            int start = end - Util.Ran.Next(0, end);
            foreach (int n in OST_Int.Keys(start, end))
            {
                Assert.AreEqual(n, source[OST_Int.Get(n)]);
            }
        }

        [TestMethod]
        public virtual void Test_Keys()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                OST_Int.Put(source[i], i);

            int previous = int.MinValue;
            foreach (int n in OST_Int.Keys())
            {
                Assert.IsTrue(previous < n);
                previous = n;
            }
        }

        [TestMethod]
        public virtual void Test_Rank_Select()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < _rowCount; i++)
                OST_Int.Put(source[i], i);

            for (int i = 0; i < _rowCount; i++)
            {
                Assert.AreEqual(i, OST_Int.Rank(OST_Int.Select(i)));
                Assert.AreEqual(i, OST_Int.Select(OST_Int.Rank(i)));
            }
        }
    }

    public class TestOrderedSymbolTableCert : TestOrderedSymbolTable
    {
        protected int _rowCount4Cert = 800;

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Resize_Cert()
        {
            _rowCount = _rowCount4Cert;
            _ST_Int = new CertWrapper4ST<ISymbolTable<int, int>, int, int>(_ST_Int);
            Test_Get_Put_Delete_Resize();
        }

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Intermixed_Cert()
        {
            _rowCount = _rowCount4Cert;
            _ST_Int = new CertWrapper4ST<ISymbolTable<int, int>, int, int>(_ST_Int);
            Test_Get_Put_Delete_Intermixed();
        }

        [TestMethod]
        public virtual void Test_Min_Max_Cert()
        {
            _rowCount = _rowCount4Cert;
            _ST_Int = new CertWrapper4OST<IOrderedSymbolTable<int, int>, int, int>(OST_Int);
            Test_Min_Max();
        }
    }

    public class TestMultiOrderedSymbolTable : TestOrderedSymbolTable
    {
        protected TestMultiSymbolTable _testClient;

        public TestMultiOrderedSymbolTable()
        {
            _testClient = new TestMultiSymbolTable();
        }

        [TestMethod]
        public virtual void TestMultipleKey()
        {
            _testClient.SetTestObj(_ST_Int);
            _testClient.TestMultipleKey();
        }
    }

    [TestClass]
    public class TestBinarySearchST : TestOrderedSymbolTable
    {
        public TestBinarySearchST()
        {
            _ST_Int = new BinarySearchST<int, int>();
        }
    }

    [TestClass]
    public class TestSequentialSearchST : TestSymbolTable
    {
        public TestSequentialSearchST()
        {
            _ST_Int = new SequentialSearchST<int, int>();
        }
    }

    [TestClass]
    public class TestSymbolTable_Cache : TestSymbolTable
    {
        public TestSymbolTable_Cache()
        {
            _ST_Int = new SequentialSearchST_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestSelfOrganizingSearch : TestSymbolTable
    {
        public TestSelfOrganizingSearch()
        {
            _ST_Int = new SelfOrganizingSearch<int, int>();
        }
    }

    [TestClass]
    public class TestOrderedInsertion : TestOrderedSymbolTableCert
    {
        public TestOrderedInsertion()
        {
            _ST_Int = new OrderedInsertion<int, int>();
        }
    }

    [TestClass]
    public class TestBinarySearch_Cache : TestOrderedSymbolTableCert
    {
        public TestBinarySearch_Cache()
        {
            _ST_Int = new BinarySearch_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestInterpolationSearch : TestOrderedSymbolTable
    {
        public TestInterpolationSearch()
        {
            _ST_Int = new InterpolationSearch<int>();
        }
    }

    [TestClass]
    public class TestBST : TestOrderedSymbolTableCert
    {
        public TestBST()
        {
            _ST_Int = new BST<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Cache : TestOrderedSymbolTableCert
    {
        public TestBST_Cache()
        {
            _ST_Int = new BST_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Iterator : TestOrderedSymbolTableCert
    {
        public TestBST_Iterator()
        {
            _ST_Int = new BST_Iterator<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Threading : TestOrderedSymbolTableCert
    {
        private BST_Threading<int, int> bst;

        public TestBST_Threading()
        {
            bst = new BST_Threading<int, int>();
            _ST_Int = bst;
        }

        [TestMethod]
        public void TestPrevNext()
        {
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            foreach (int n in source)
                OST_Int.Put(n, n);

            for (int i = 1; i < _rowCount - 1; i++)
            {
                Assert.AreEqual(i - 1, bst.Prev(i));
                Assert.AreEqual(i + 1, bst.Next(i));
            }

            for (int i = 0; i < _rowCount / 2; i++)
            {
                int key = Util.Ran.Next(0, _rowCount);
                bst.Delete(key);
            }

            int current = bst.Min();
            foreach (int k in bst.Keys())
            {
                Assert.AreEqual(k, current);
                current = bst.Next(current);
            }

            bst.DeleteMin();
            bst.DeleteMax();
            bst.DeleteMin();
            bst.DeleteMax();

            current = bst.Max();
            foreach (int k in bst.Keys().Reverse())
            {
                Assert.AreEqual(k, current);
                current = bst.Prev(current);
            }
        }
    }

    [TestClass]
    public class TestBST_23 : TestOrderedSymbolTableCert
    {
        public TestBST_23()
        {
            _ST_Int = new BST_23<int, int>();
        }
    }

    [TestClass]
    public class TestBST_23_Cache : TestOrderedSymbolTableCert
    {
        public TestBST_23_Cache()
        {
            _ST_Int = new BST_23_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestBST_23_WithoutBalance : TestOrderedSymbolTableCert
    {
        public TestBST_23_WithoutBalance()
        {
            _ST_Int = new BST_23_WithoutBalance<int, int>();
        }
    }

    [TestClass]
    public class TestBST_234 : TestOrderedSymbolTableCert
    {
        public TestBST_234()
        {
            _ST_Int = new BST_234<int, int>();
        }
    }

    [TestClass]
    public class TestSeperateChainingHashST : TestHashST
    {
        public TestSeperateChainingHashST()
        {
            _ST_Int = new SeperateChainingHashST<int, int>();
            _ST_Trans = new SeperateChainingHashST<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestLinearProbingHashST : TestHashST
    {
        public TestLinearProbingHashST()
        {
            _ST_Int = new LinearProbingHashST<int, int>();
            _ST_Trans = new LinearProbingHashST<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestLazyDeleteLinearProbing : TestHashST
    {
        public TestLazyDeleteLinearProbing()
        {
            _ST_Int = new LazyDeleteLinearProbing<int, int>();
            _ST_Trans = new LazyDeleteLinearProbing<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestDoubleProbing : TestSeperateChainingHashST
    {
        public TestDoubleProbing()
        {
            _ST_Int = new DoubleProbing<int, int>();
            _ST_Trans = new DoubleProbing<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestDoubleHashing : TestSeperateChainingHashST
    {
        public TestDoubleHashing()
        {
            _ST_Int = new DoubleHashing<int, int>();
            _ST_Trans = new DoubleHashing<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestCuckooHashing : TestSeperateChainingHashST
    {
        public TestCuckooHashing()
        {
            _ST_Int = new CuckooHashing<int, int>();
            _ST_Trans = new CuckooHashing<Trasaction, int>();
        }
    }

    [TestClass]
    public class TestSequentialSearchMultiST : TestMultiSymbolTable
    {
        public TestSequentialSearchMultiST()
        {
            _ST_Int = new SequentialSearchMultiST<int, int>();
        }
    }

    [TestClass]
    public class TestSeperateChainingMultiST : TestMultiSymbolTable
    {
        public TestSeperateChainingMultiST()
        {
            _ST_Int = new SeperateChainingMultiST<int, int>();
        }
    }
}

