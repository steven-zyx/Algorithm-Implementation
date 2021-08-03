using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using String;

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
            string[] stringList = Util.GenerateString(1_000_000, 10);
            LSDSort client = new LSDSort();
            client.Sort(stringList, 10);
            Assert.IsTrue(stringList.IsSorted());
        }
    }
}
