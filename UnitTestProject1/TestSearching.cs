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
    public class TestOrderedSymbolTable
    {
        private IOrderedSymbolTable<int, int> _ST_Int = new BinarySearchST<int, int>();
        private Random _ran = new Random(DateTime.Now.Second);

        [TestMethod]
        public void Test_Get_Put_Delete_Resize()
        {
            _ST_Int.Init();
            int count = 30_000;
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
            int count = 50_000;
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

            int count = 500_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(i, source[i]);

            for (int i = 0; i < 100_000; i++)
            {
                int index = _ran.Next(0, count * 2);
                if (index < count)
                    Assert.IsTrue(_ST_Int.Contains(index));
                else
                    Assert.IsFalse(_ST_Int.Contains(index));
            }
        }

        [TestMethod]
        public void Test_Min_Max()
        {
            _ST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(source[i], source[i]);

            int currentMin = -1;
            int currentMax = int.MaxValue;
            while (!_ST_Int.IsEmpty)
            {
                Assert.IsTrue(currentMin < _ST_Int.Min);
                currentMin = _ST_Int.Min;
                _ST_Int.DeleteMin();

                Assert.IsTrue(currentMax > _ST_Int.Max);
                currentMax = _ST_Int.Max;
                _ST_Int.DeleteMax();
            }
        }

        [TestMethod]
        public void Test_Floor_Ceiling()
        {
            _ST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(source[i] * 2, source[i] * 2);

            for (int i = 1; i < count * 2 - 1; i += 2)
            {
                Assert.AreEqual(i - 1, _ST_Int.Floor(i));
                Assert.AreEqual(i + 1, _ST_Int.Ceiling(i));
            }
        }

        [TestMethod]
        public void Test_Keys()
        {
            _ST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(i, source[i]);

            int end = _ran.Next(0, count);
            int start = end - _ran.Next(0, end);
            foreach (int n in _ST_Int.Keys(start, end))
                Assert.AreEqual(source[n], _ST_Int.Get(n));
        }

        [TestMethod]
        public void Test_Rank_Select()
        {
            _ST_Int.Init();

            int count = 50_000;
            int[] source = Util.GenerateRandomArray(0, count);
            for (int i = 0; i < source.Length; i++)
                _ST_Int.Put(i, source[i]);

            for (int i = 0; i < 10_000; i++)
            {
                Assert.AreEqual(i, _ST_Int.Rank(_ST_Int.Select(i)));
                Assert.AreEqual(i, _ST_Int.Select(_ST_Int.Rank(i)));
            }
        }
    }
}
