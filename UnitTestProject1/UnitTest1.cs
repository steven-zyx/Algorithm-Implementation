using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDataStrcture;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class TestBasicDataStructure
    {
        [TestMethod]
        public void TestListCorrect()
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
    }
}
