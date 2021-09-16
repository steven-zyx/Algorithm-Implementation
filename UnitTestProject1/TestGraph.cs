using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmImplementation.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmUnitTest.TestGraph
{
    [TestClass]
    public class TestGraph
    {
        protected Graph _simpleG;

        public TestGraph()
        {
            int[] data = { 7, 5, 0, 1, 1, 2, 2, 3, 3, 0, 4, 5 };
            _simpleG = new Graph(data);
        }

        [TestMethod]
        public void TestDFS_Simple()
        {
            DepthFirstSearch client = new DepthFirstSearch(_simpleG, 0);
            Assert.IsTrue(client.Marked(0));
            Assert.IsTrue(client.Marked(1));
            Assert.IsTrue(client.Marked(2));
            Assert.IsTrue(client.Marked(3));
            Assert.IsFalse(client.Marked(4));
            Assert.IsFalse(client.Marked(5));
            Assert.IsFalse(client.Marked(6));
            Assert.AreEqual(4, client.Count());
        }
    }
}
