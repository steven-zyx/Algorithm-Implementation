using BasicDataStrcture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;
using BasicDataStrcture;
using Searching;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void Test_Get_Put_Resize()
        {
            _ST_Int.Init();
            int count = 100_000;
            
        }
    }
}
