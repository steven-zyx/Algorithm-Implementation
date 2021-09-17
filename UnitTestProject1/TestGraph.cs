using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmImplementation.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
        public void TestDFS()
        {
            DepthFirstSearch client = new DepthFirstSearch(_simpleG, 0);
            client.Process();
            Assert.IsTrue(client.Marked[0]);
            Assert.IsTrue(client.Marked[1]);
            Assert.IsTrue(client.Marked[2]);
            Assert.IsTrue(client.Marked[3]);
            Assert.IsFalse(client.Marked[4]);
            Assert.IsFalse(client.Marked[5]);
            Assert.IsFalse(client.Marked[6]);
            Assert.AreEqual(4, client.CountInComponent);
        }

        [TestMethod]
        public void TestFindingPaths()
        {
            FindingPaths client = new FindingPaths(_simpleG, 0);
            client.Process();
            Assert.AreEqual(1, client.PathTo(0).Count());
            Assert.IsTrue(client.PathTo(1).Count() >= 1);
            Assert.IsTrue(client.PathTo(3).Count() >= 1);
            Assert.AreEqual(3, client.PathTo(2).Count());
            Assert.IsNull(client.PathTo(4));
            Assert.IsNull(client.PathTo(5));
            Assert.IsNull(client.PathTo(6));
        }

        [TestMethod]
        public void TestBFS()
        {
            BreadthFirstSearch client = new BreadthFirstSearch(_simpleG, 0);
            client.Process();
            Assert.AreEqual(1, client.PathTo(0).Count());
            Assert.AreEqual(2, client.PathTo(1).Count());
            Assert.AreEqual(2, client.PathTo(3).Count());
            Assert.AreEqual(3, client.PathTo(2).Count());
            Assert.IsNull(client.PathTo(4));
            Assert.IsNull(client.PathTo(5));
            Assert.IsNull(client.PathTo(6));
        }

        [TestMethod]
        public void TestConnectComponent()
        {
            ConnectedComponent client = new ConnectedComponent(_simpleG);
            client.Process();
            Assert.AreEqual(3, client.ComponentCount);
            Assert.AreEqual(0, client.ID[0]);
            Assert.AreEqual(0, client.ID[1]);
            Assert.AreEqual(0, client.ID[2]);
            Assert.AreEqual(0, client.ID[3]);
            Assert.AreEqual(1, client.ID[4]);
            Assert.AreEqual(1, client.ID[5]);
            Assert.AreEqual(2, client.ID[6]);
        }

        [TestMethod]
        public void TestBipartite()
        {
            TwoColor client = new TwoColor(_simpleG);
            client.Process();
            Assert.IsTrue(client.IsBipartite);

            _simpleG.AddEdge(1, 3);
            client = new TwoColor(_simpleG);
            client.Process();
            Assert.IsFalse(client.IsBipartite);
        }

        [TestMethod]
        public void TestCycle()
        {
            Cycle client = new Cycle(_simpleG);
            client.Process();
            Assert.IsTrue(client.HasCycle);
        }
    }
}
