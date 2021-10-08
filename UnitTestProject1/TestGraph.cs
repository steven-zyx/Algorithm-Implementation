using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmImplementation.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;
using Utils;

namespace AlgorithmUnitTest.TestGraph
{
    public abstract class TestGraph
    {
        protected Graph _simpleG;

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
    }


    [TestClass]
    public class TestUndirectedGraph : TestGraph
    {
        public TestUndirectedGraph()
        {
            int[] data = { 7, 5, 0, 1, 1, 2, 2, 3, 3, 0, 4, 5 };
            _simpleG = new Graph(data);
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

        [TestMethod]
        public void TestEulerianAndHamiltonianCycle()
        {
            int[][] dataList = new int[4][]
            {
                new int[] { 10, 15, 0, 1, 0, 2, 0, 3, 1, 3, 1, 4, 2, 5, 2, 9, 3, 6, 4, 7, 4, 8, 5, 8, 5, 9, 6, 7, 6, 9, 7, 8 },
                new int[] { 10, 15, 0, 1, 0, 2, 0, 3, 1, 3, 0, 3, 2, 5, 5, 6, 3, 6, 4, 7, 4, 8, 5, 8, 5, 9, 6, 7, 6, 9, 8, 8 },
                new int[] { 10, 15, 0, 1, 1, 2, 1, 3, 0, 3, 0, 4, 2, 5, 2, 9, 3, 6, 4, 7, 4, 8, 5, 8, 5, 9, 6, 7, 6, 9, 7, 8 },
                new int[] { 10, 15, 4, 1, 7, 9, 6, 2, 7, 3, 5, 0, 0, 2, 0, 8, 1, 6, 3, 9, 6, 3, 2, 8, 1, 5, 9, 8, 4, 5, 4, 7 }
            };
            (bool IsEulerianCycle, bool IsHamiltonianCycle)[] result = new (bool IsEulerianCycle, bool IsHamiltonianCycle)[]
            {
                (false,true),
                (true,false),
                (false,true),
                (false,true)
            };

            for (int i = 0; i < 4; i++)
            {
                Graph simpleGraph = new Graph(dataList[i]);
                EulerianCycle eCycle = new EulerianCycle(simpleGraph);
                eCycle.Process();
                Assert.AreEqual(result[i].IsEulerianCycle, eCycle.IsEulerianCycle);

                HamiltonianCycle hCycle = new HamiltonianCycle(simpleGraph);
                hCycle.Process();
                Assert.AreEqual(result[i].IsHamiltonianCycle, hCycle.IsHamiltonianCycle);
            };
        }

        [TestMethod]
        public void TestParallelEdge()
        {
            ParallelEdgeDetection client = new ParallelEdgeDetection(_simpleG);
            client.Process();
            Assert.AreEqual(0, client.ParallelEdge);

            _simpleG.AddEdge(1, 0);
            _simpleG.AddEdge(4, 5);
            _simpleG.AddEdge(5, 4);
            client = new ParallelEdgeDetection(_simpleG);
            client.Process();
            Assert.AreEqual(3, client.ParallelEdge);
        }

        [TestMethod]
        public void TestSymbolGraph()
        {
            string fileName = Util.DesktopPath + "graph.txt";
            string[] lines = new string[]
            {
                "basketball steven,john",
                "cosmetics amy",
                "food steven",
                "food amy",
                "house john,amy"
            };
            File.WriteAllLines(fileName, lines);

            SymbolGraph graph = new SymbolGraph(fileName, ',');
            Assert.AreEqual(2, graph.Adj("steven").Count());
            Assert.AreEqual(2, graph.Adj("john").Count());
            Assert.AreEqual(3, graph.Adj("amy").Count());
            Assert.AreEqual(2, graph.Adj("basketball").Count());
            Assert.AreEqual(1, graph.Adj("cosmetics").Count());
            Assert.AreEqual(2, graph.Adj("house").Count());
            Assert.AreEqual(2, graph.Adj("food").Count());
        }
    }

    [TestClass]
    public class TestDirectedGraph : TestGraph
    {
        public TestDirectedGraph()
        {
            int[] data = { 7, 5, 0, 1, 1, 2, 2, 3, 3, 0, 4, 5 };
            _simpleG = new Digraph(data);
        }

        [TestMethod]
        public void TestBFS()
        {
            BreadthFirstSearch client = new BreadthFirstSearch(_simpleG, 0);
            client.Process();
            Assert.AreEqual(1, client.PathTo(0).Count());
            Assert.AreEqual(2, client.PathTo(1).Count());
            Assert.AreEqual(4, client.PathTo(3).Count());
            Assert.AreEqual(3, client.PathTo(2).Count());
            Assert.IsNull(client.PathTo(4));
            Assert.IsNull(client.PathTo(5));
            Assert.IsNull(client.PathTo(6));
        }

        [TestMethod]
        public void TestMultipleSourceReachability()
        {
            int[] sources = { 0 };
            DirectedDFS client = new DirectedDFS(_simpleG, sources);
            Assert.IsTrue(client.Marked[0]);
            Assert.IsTrue(client.Marked[1]);
            Assert.IsTrue(client.Marked[2]);
            Assert.IsTrue(client.Marked[3]);
            Assert.IsFalse(client.Marked[4]);
            Assert.IsFalse(client.Marked[5]);
            Assert.IsFalse(client.Marked[6]);

            sources = new int[] { 0, 4, 6 };
            client = new DirectedDFS(_simpleG, sources);
            for (int i = 0; i < _simpleG.V; i++)
                Assert.IsTrue(client.Marked[i]);
        }

        [TestMethod]
        public void TestFindCycle()
        {
            DirectedCycle client = new DirectedCycle(_simpleG);
            Assert.IsTrue(client.HasCycle);
            Assert.AreEqual("01230", string.Join("", client.Cycle));
        }
    }
}
