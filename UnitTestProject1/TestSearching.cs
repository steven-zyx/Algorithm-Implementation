using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using System;
using System.Collections.Generic;
using Utils;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSearching
    {

    }

    [TestClass]
    public class TestSymbolTable
    {
        protected Random _ran;
        protected ISymbolTable<int, int> _ST_Int;

        public TestSymbolTable()
        {
            _ran = new Random(DateTime.Now.Second);
            _ST_Int = new SequentialSearchST<int, int>();
        }

        [TestMethod]
        public void Test_Get_Put_Delete_Resize()
        {
            _ST_Int.Init();
            int count = 10_000;
            int[] source = Util.GenerateRandomArray(0, count);

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < count; i++)
                    _ST_Int.Put(i, source[i]);
                for (int i = 0; i < count; i++)
                    Assert.AreEqual(source[i], _ST_Int.Get(i));
                for (int i = 0; i < count; i++)
                    Assert.IsTrue(_ST_Int.Delete(i));
            }
        }

        [TestMethod]
        public void Test_Get_Put_Delete_Intermixed()
        {
            _ST_Int.Init();
            int count = 10_000;
            int[] source = Util.GenerateRandomArrayRepeat(0, count, 5);

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
        public void Test_Contains()
        {
            _ST_Int.Init();

            int count = 10_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(i, source[i]);

            for (int i = 0; i < 10_000; i++)
            {
                int index = _ran.Next(0, count * 2);
                if (index < count)
                    Assert.IsTrue(_ST_Int.Contains(index));
                else
                    Assert.IsFalse(_ST_Int.Contains(index));
            }
        }
    }

    [TestClass]
    public class TestOrderedSymbolTable : TestSymbolTable
    {
        private IOrderedSymbolTable<int, int> _OST_Int;

        public TestOrderedSymbolTable()
        {
            _ST_Int = new BinarySearchST<int, int>();
            _OST_Int = new BinarySearchST<int, int>();
        }

        [TestMethod]
        public void Test_Min_Max()
        {
            _OST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i], source[i]);

            int currentMin = -1;
            int currentMax = int.MaxValue;
            while (!_OST_Int.IsEmpty)
            {
                Assert.IsTrue(currentMin < _OST_Int.Min);
                currentMin = _OST_Int.Min;
                _OST_Int.DeleteMin();

                Assert.IsTrue(currentMax > _OST_Int.Max);
                currentMax = _OST_Int.Max;
                _OST_Int.DeleteMax();
            }
        }

        [TestMethod]
        public void Test_Floor_Ceiling()
        {
            _OST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(source[i] * 2, source[i] * 2);

            for (int i = 1; i < count * 2 - 1; i += 2)
            {
                Assert.AreEqual(i - 1, _OST_Int.Floor(i));
                Assert.AreEqual(i + 1, _OST_Int.Ceiling(i));
            }
        }

        [TestMethod]
        public void Test_Keys()
        {
            _OST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(i, source[i]);

            int end = _ran.Next(0, count);
            int start = end - _ran.Next(0, end);
            foreach (int n in _OST_Int.Keys(start, end))
                Assert.AreEqual(source[n], _OST_Int.Get(n));
        }

        [TestMethod]
        public void Test_Rank_Select()
        {
            _OST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _OST_Int.Put(i, source[i]);

            for (int i = 0; i < 10_000; i++)
            {
                Assert.AreEqual(i, _OST_Int.Rank(_OST_Int.Select(i)));
                Assert.AreEqual(i, _OST_Int.Select(_OST_Int.Rank(i)));
            }
        }
    }
}
