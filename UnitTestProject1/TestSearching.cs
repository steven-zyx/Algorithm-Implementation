﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using System;
using System.Collections.Generic;
using Utils;
using System.Linq;

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
    }

    [TestClass]
    public class TestSymbolTable
    {
        protected Random _ran;
        protected ISymbolTable<int, int> _ST_Int;
        protected int _rowCount;

        public TestSymbolTable()
        {
            _ran = new Random((int)DateTime.Now.Ticks);
            _ST_Int = new SequentialSearchST<int, int>();
            _rowCount = 15_000;
        }

        [TestMethod]
        public virtual void Test_Get_Put_Delete_Resize()
        {
            _ST_Int.Init();
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
            _ST_Int.Init();
            int[] source = Util.GenerateRandomArrayRepeat(0, _rowCount, 5);

            HashSet<int> insertedKeys = new HashSet<int>();
            foreach (int i in source)
            {
                int choice = _ran.Next(0, 3);
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
            _ST_Int.Init();
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(source[i], i);

            for (int i = 0; i < _rowCount; i++)
            {
                int index = _ran.Next(0, _rowCount * 2);
                if (index < _rowCount)
                    Assert.IsTrue(_ST_Int.Contains(index));
                else
                    Assert.IsFalse(_ST_Int.Contains(index));
            }
        }
    }

    [TestClass]
    public class TestOrderedSymbolTable : TestSymbolTable
    {
        protected IOrderedSymbolTable<int, int> _OST_Int;

        public TestOrderedSymbolTable()
        {
            _ST_Int = new BinarySearchST<int, int>();
            _OST_Int = new BinarySearchST<int, int>();
            _rowCount = 50_000;
        }

        [TestMethod]
        public virtual void Test_Min_Max()
        {
            _OST_Int.Init();

            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i], source[i]);

            int currentMin = -1;
            int currentMax = int.MaxValue;
            while (!_OST_Int.IsEmpty)
            {
                Assert.IsTrue(currentMin < _OST_Int.Min());
                currentMin = _OST_Int.Min();
                _OST_Int.DeleteMin();

                Assert.IsTrue(currentMax > _OST_Int.Max());
                currentMax = _OST_Int.Max();
                _OST_Int.DeleteMax();
            }
        }

        [TestMethod]
        public virtual void Test_Floor_Ceiling()
        {
            _OST_Int.Init();

            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i] * 2, source[i] * 2);

            for (int i = 1; i < _rowCount * 2 - 1; i += 2)
            {
                Assert.AreEqual(i - 1, _OST_Int.Floor(i));
                Assert.AreEqual(i + 1, _OST_Int.Ceiling(i));
            }
        }

        [TestMethod]
        public virtual void Test_Keys2()
        {
            _OST_Int.Init();

            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i], i);

            int end = _ran.Next(0, _rowCount);
            int start = end - _ran.Next(0, end);
            foreach (int n in _OST_Int.Keys(start, end))
            {
                Assert.AreEqual(n, source[_OST_Int.Get(n)]);
            }
        }

        [TestMethod]
        public virtual void Test_Keys()
        {
            _OST_Int.Init();
            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i], i);

            int previous = int.MinValue;
            foreach (int n in _OST_Int.Keys())
            {
                Assert.IsTrue(previous < n);
                previous = n;
            }
        }

        [TestMethod]
        public virtual void Test_Rank_Select()
        {
            _OST_Int.Init();

            int[] source = Util.GenerateRandomArray(0, _rowCount);
            for (int i = 0; i < _rowCount; i++)
                _OST_Int.Put(source[i], i);

            for (int i = 0; i < _rowCount; i++)
            {
                Assert.AreEqual(i, _OST_Int.Rank(_OST_Int.Select(i)));
                Assert.AreEqual(i, _OST_Int.Select(_OST_Int.Rank(i)));
            }
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
    public class TestOrderedInsertion : TestOrderedSymbolTable
    {
        public TestOrderedInsertion()
        {
            _ST_Int = new OrderedInsertion<int, int>();
            _OST_Int = new OrderedInsertion<int, int>();
        }
    }

    [TestClass]
    public class TestBinarySearch_Cache : TestOrderedSymbolTable
    {
        public TestBinarySearch_Cache()
        {
            _ST_Int = new BinarySearch_Cache<int, int>();
            _OST_Int = new BinarySearch_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestInterpolationSearch : TestOrderedSymbolTable
    {
        public TestInterpolationSearch()
        {
            _ST_Int = new InterpolationSearch<int>();
            _OST_Int = new InterpolationSearch<int>();
        }
    }

    [TestClass]
    public class TestBST : TestOrderedSymbolTable
    {
        public TestBST()
        {
            _ST_Int = new BST<int, int>();
            _OST_Int = new BST<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Cache : TestOrderedSymbolTable
    {
        public TestBST_Cache()
        {
            _ST_Int = new BST_Cache<int, int>();
            _OST_Int = new BST_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Certificate : TestOrderedSymbolTable
    {
        public TestBST_Certificate()
        {
            _ST_Int = new BST_Certificate<int, int>();
            _OST_Int = new BST_Certificate<int, int>();
            _rowCount = 1_000;
        }
    }

    [TestClass]
    public class TestBST_Iterator : TestOrderedSymbolTable
    {
        public TestBST_Iterator()
        {
            _ST_Int = new BST_Iterator<int, int>();
            _OST_Int = new BST_Iterator<int, int>();
        }
    }

    [TestClass]
    public class TestBST_Threading : TestOrderedSymbolTable
    {
        private BST_Threading<int, int> bst;

        public TestBST_Threading()
        {
            bst = new BST_Threading<int, int>();
            _ST_Int = bst;
            _OST_Int = bst;
        }

        [TestMethod]
        public void TestPrevNext()
        {
            bst.Init();

            int[] source = Util.GenerateRandomArray(0, _rowCount);
            foreach (int n in source)
                _OST_Int.Put(n, n);

            for (int i = 1; i < _rowCount - 1; i++)
            {
                Assert.AreEqual(i - 1, bst.Prev(i));
                Assert.AreEqual(i + 1, bst.Next(i));
            }

            for (int i = 0; i < _rowCount / 2; i++)
            {
                int key = _ran.Next(0, _rowCount);
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
    public class TestBST_23 : TestOrderedSymbolTable
    {
        public TestBST_23()
        {
            _OST_Int = new BST_23<int, int>();
            _ST_Int = new BST_23<int, int>();
        }
    }

    [TestClass]
    public class TestBST_234 : TestOrderedSymbolTable
    {
        public TestBST_234()
        {
            _OST_Int = new BST_234<int, int>();
            _ST_Int = new BST_234<int, int>();
        }

        public override void Test_Get_Put_Delete_Intermixed()
        {
            base.Test_Get_Put_Delete_Intermixed();
        }

        public override void Test_Get_Put_Delete_Resize()
        {
            base.Test_Get_Put_Delete_Resize();
        }
    }

    [TestClass]
    public class TestBST_23_Cache : TestOrderedSymbolTable
    {
        public TestBST_23_Cache()
        {
            _ST_Int = new BST_23_Cache<int, int>();
            _OST_Int = new BST_23_Cache<int, int>();
        }
    }

    [TestClass]
    public class TestSeperateChainingHashST : TestSymbolTable
    {
        public TestSeperateChainingHashST()
        {
            int primeCeiling = Util.PrimeCeiling(_rowCount);
            _ST_Int = new SeperateChainingHashST<int, int>(primeCeiling);
        }
    }
}
