using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmImplementation.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;
using Utils;
using System.Reflection;
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

            File.Delete(fileName);
        }
    }

    [TestClass]
    public class TestDirectedGraph : TestGraph
    {
        protected Digraph _simpleDiG => _simpleG as Digraph;
        protected Digraph _simpleDAG;
        protected Digraph _simpleDAG2;

        public TestDirectedGraph()
        {
            int[] data = { 7, 5, 0, 1, 1, 2, 2, 3, 3, 0, 4, 5 };
            _simpleG = new Digraph(data);

            data = new int[] { 13, 15, 0, 1, 0, 5, 0, 6, 2, 0, 2, 3, 3, 5, 5, 4, 6, 4, 6, 9, 7, 6, 8, 7, 9, 10, 9, 11, 9, 12, 11, 12 };
            _simpleDAG = new Digraph(data);

            data = new int[] { 15, 14, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 1, 10, 10, 11, 11, 12, 8, 9, 9, 7, 8, 13, 13, 14, 14, 0, 0, 12 };
            _simpleDAG2 = new Digraph(data);
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
            DirectedDFS client = new DirectedDFS(_simpleDiG, sources);
            Assert.IsTrue(client.Marked[0]);
            Assert.IsTrue(client.Marked[1]);
            Assert.IsTrue(client.Marked[2]);
            Assert.IsTrue(client.Marked[3]);
            Assert.IsFalse(client.Marked[4]);
            Assert.IsFalse(client.Marked[5]);
            Assert.IsFalse(client.Marked[6]);

            sources = new int[] { 0, 4, 6 };
            client = new DirectedDFS(_simpleDiG, sources);
            for (int i = 0; i < _simpleG.V; i++)
                Assert.IsTrue(client.Marked[i]);
        }

        [TestMethod]
        public void TestFindCycle()
        {
            DirectedCycle client = new DirectedCycle(_simpleDiG);
            Assert.IsTrue(client.HasCycle);
            Assert.AreEqual("01230", string.Join("", client.Cycle));
        }

        [TestMethod]
        public void TestTopologicalOrder()
        {
            Topological client = new Topological(_simpleDAG);
            Assert.IsTrue(client.IsDAG());
            Assert.AreEqual("8723015649101112", string.Join("", client.Order()));
        }

        [TestMethod]
        public void TestQueueBasedTopological()
        {
            Topological_QueueBased client = new Topological_QueueBased(_simpleDAG);
            Assert.IsTrue(client.IsDAG);
            Assert.AreEqual("2830751694111012", string.Join("", client.Order()));
        }

        [TestMethod]
        public void TestStronglyConncectedComponent()
        {
            SCC client = new SCC(_simpleDiG);
            Assert.AreEqual(4, client.Count);
            Assert.IsTrue(client.StronglyConnected(0, 1));
            Assert.IsTrue(client.StronglyConnected(0, 2));
            Assert.IsTrue(client.StronglyConnected(0, 3));
            Assert.IsTrue(client.StronglyConnected(1, 2));
            Assert.IsTrue(client.StronglyConnected(1, 3));
            Assert.IsTrue(client.StronglyConnected(2, 3));
            Assert.IsFalse(client.StronglyConnected(4, 5));
            Assert.IsFalse(client.StronglyConnected(4, 2));
            Assert.IsFalse(client.StronglyConnected(5, 1));
            Assert.IsFalse(client.StronglyConnected(6, 0));
            Assert.IsFalse(client.StronglyConnected(6, 5));
        }

        [TestMethod]
        public void TestLongestPathDAG()
        {
            LongestPath_DAG_Unweighted client = new LongestPath_DAG_Unweighted(_simpleDAG, 0, 12);
            Assert.AreEqual("0691112", string.Join("", client.Path));
        }

        private void DoTestLCAofDAG(Func<int, int, int> fCalculate)
        {
            Assert.AreEqual(2, fCalculate(1, 3));
            Assert.AreEqual(9, fCalculate(12, 10));
            Assert.AreEqual(-1, fCalculate(3, 7));
        }

        [TestMethod]
        public void TestLCAofDAG_Wrong1() => DoTestLCAofDAG((v1, v2) => (new LCAofDAG_Wrong1(_simpleDAG, v1, v2)).LCA);

        [TestMethod]
        public void TestLCAofDAG_Wrong2() => DoTestLCAofDAG((v1, v2) => (new LCAofDAG_Wrong2(_simpleDAG, v1, v2)).LCA);

        [TestMethod]
        public void TestLCAofDAG()
        {
            Func<int, int, int> fCalculate = (v1, v2) =>
              {
                  LCAofDAG client = new LCAofDAG(_simpleDAG, v1, v2);
                  int[] LCAs = client.LCAs().ToArray();
                  if (LCAs.Length > 0)
                      return LCAs[0];
                  else
                      return -1;
              };
            DoTestLCAofDAG(fCalculate);
        }

        [TestMethod]
        public void TestBFS_Height()
        {
            BreadthFirstSearchHeight client = new BreadthFirstSearchHeight(_simpleDAG2, 1);
            Assert.AreEqual(6, client.Height[7]);
            Assert.AreEqual(3, client.Height[12]);

            client = new BreadthFirstSearchHeight(_simpleDAG2, 8);
            Assert.AreEqual(2, client.Height[7]);
            Assert.AreEqual(4, client.Height[12]);
        }

        [TestMethod]
        public void TestShortestAncestralPath()
        {
            ShortestAncestralPath client = new ShortestAncestralPath(_simpleDAG2, 1, 8);
            string path = string.Join(",", client.ShortestAncestralPaths().First());
            Assert.AreEqual("1,10,11,12,0,14,13,8", path);
        }

        [TestMethod]
        public void TestArithmaticDAG()
        {
            //( 3 * 4 ) / 5 + 2 + ( 3 * 4 )
            string[] values = { "+", "/", "+", "5", "*", "2", "3", "4" };
            int[] data = { values.Length, 8, 0, 2, 0, 1, 1, 3, 1, 4, 2, 5, 2, 4, 4, 7, 4, 6 };
            Digraph g = new Digraph(data);
            ArithmaticDAG client = new ArithmaticDAG(g, values);
            Assert.AreEqual(16.4, Math.Round(client.Result, 10));

            //( ( 3 * 4 ) + ( 3 * 4 ) + ( 3 * 4 ) + ( 3 * 4 ) ) / 6
            values = new string[] { "/", "+", "6", "*", "3", "4" };
            data = new int[] { values.Length, 8, 0, 2, 0, 1, 1, 3, 1, 3, 1, 3, 1, 3, 3, 5, 3, 4 };
            g = new Digraph(data);
            client = new ArithmaticDAG(g, values);
            Assert.AreEqual(8, Math.Round(client.Result, 10));
        }
    }

    [TestClass]
    public class TestWeightedGraph
    {
        protected EdgeWeightedGraph _simpleWeG;
        protected EdgeWeightedGraph_Matrix _simpleWeG_Matrix;

        public TestWeightedGraph()
        {
            (int, int, double)[] edges = new (int, int, double)[]
            {
                (4,5,0.35),
                (4,7,0.37),
                (5,7,0.28),
                (0,7,0.16),
                (1,5,0.32),
                (0,4,0.38),
                (2,3,0.17),
                (1,7,0.19),
                (0,2,0.26),
                (1,2,0.36),
                (1,3,0.29),
                (2,7,0.34),
                (6,2,0.40),
                (3,6,0.52),
                (6,0,0.58),
                (6,4,0.93)
            };
            _simpleWeG = new EdgeWeightedGraph(8, edges);
            _simpleWeG_Matrix = new EdgeWeightedGraph_Matrix(8, edges);
        }

        private void FindMST(IMST client)
        {
            HashSet<double> weights = client.Edges().Select(x => x.Weight).ToHashSet();
            Assert.IsTrue(weights.Contains(0.16));
            Assert.IsTrue(weights.Contains(0.19));
            Assert.IsTrue(weights.Contains(0.26));
            Assert.IsTrue(weights.Contains(0.17));
            Assert.IsTrue(weights.Contains(0.28));
            Assert.IsTrue(weights.Contains(0.35));
            Assert.IsTrue(weights.Contains(0.40));
            Assert.AreEqual(1.81, Math.Round(client.Weight, 10));
        }

        [TestMethod]
        public void TestLazyPrim()
        {
            LazyPrim client = new LazyPrim(_simpleWeG);
            FindMST(client);
        }

        [TestMethod]
        public void TestEagerPrim()
        {
            EagerPrim client = new EagerPrim(_simpleWeG);
            FindMST(client);
        }

        [TestMethod]
        public void TestKruskal()
        {
            Kruskal client = new Kruskal(_simpleWeG);
            FindMST(client);
        }

        [TestMethod]
        public void TestCycleDetection()
        {
            CycleDetection_WeightedGraph client = new CycleDetection_WeightedGraph(_simpleWeG);
            Assert.IsTrue(client.HasCycle);

            HashSet<int> vertices = new HashSet<int>();
            foreach (Edge e in client.Cycle)
            {
                int v = e.Either(), w = e.Other(v);
                vertices.Add(v);
                vertices.Add(w);
            }
            vertices.IsSupersetOf(new int[] { 0, 4, 6 });
        }

        [TestMethod]
        public void TestVyssotsky()
        {
            Vyssotsky client = new Vyssotsky(_simpleWeG);
            FindMST(client);
        }

        [TestMethod]
        public void TestReverseDelete()
        {
            ReverseDelete client = new ReverseDelete(_simpleWeG);
            FindMST(client);
        }

        [TestMethod]
        public void TestCriticalEdges()
        {
            (int, int, double)[] edges = new (int, int, double)[]
            {
                (0, 1, 1),
                (0, 2, 2),
                (1, 2, 2)
            };
            EdgeWeightedGraph g1 = new EdgeWeightedGraph(3, edges);
            CriticalEdges client = new CriticalEdges(g1);
            Assert.AreEqual(1, client.Critical().Count());

            edges = new (int, int, double)[]
            {
                (0, 1, 2),
                (1, 2, 3),
                (2, 3, 2),
                (3, 0, 3)
            };
            EdgeWeightedGraph g2 = new EdgeWeightedGraph(4, edges);
            client = new CriticalEdges(g2);
            Assert.AreEqual(2, client.Critical().Count());

            edges = new (int, int, double)[]
            {
                (0, 1, 3),
                (1, 2, 3),
                (2, 3, 2),
                (3, 0, 3)
            };
            EdgeWeightedGraph g3 = new EdgeWeightedGraph(4, edges);
            client = new CriticalEdges(g3);
            Assert.AreEqual(1, client.Critical().Count());
        }

        [TestMethod]
        public void TestCertification()
        {
            EagerPrim mstClient = new EagerPrim(_simpleWeG);
            Certification certClient = new Certification(_simpleWeG, mstClient.Edges());
            Assert.IsTrue(certClient.IsMSTEdges);

            List<Edge> edges = mstClient.Edges().ToList();
            int v = edges[2].Either(), w = edges[2].Other(v);
            edges[2] = new Edge(v, w, edges[2].Weight - 0.1);
            certClient = new Certification(_simpleWeG, edges);
            Assert.IsFalse(certClient.IsMSTEdges);

            edges = mstClient.Edges().ToList();
            v = edges[5].Either();
            w = edges[5].Other(v);
            edges[5] = new Edge(v, 2, edges[5].Weight + 0.1);
            certClient = new Certification(_simpleWeG, edges);
            Assert.IsFalse(certClient.IsMSTEdges);

            edges = mstClient.Edges().ToList();
            edges.RemoveAt(2);
            certClient = new Certification(_simpleWeG, edges);
            Assert.IsFalse(certClient.IsMSTEdges);
        }

        [TestMethod]
        public void TestMatrix()
        {
            LazyPrim_SpaceEfficient client = new LazyPrim_SpaceEfficient(_simpleWeG_Matrix);
            FindMST(client);
        }

        [TestMethod]
        public void TestEagerPrimForDenseGraph()
        {
            EagerPrim_DenseGraph client = new EagerPrim_DenseGraph(_simpleWeG);
            FindMST(client);
        }
    }

    [TestClass]
    public class TestDirectedWeightedGraph
    {
        enum Monotonicity
        {
            Increasing,
            Decreasing,
            Bitonic
        }

        protected EdgeWeightedDigraph _simpleWeDiG;

        protected EdgeWeightedDigraph_Matrix _simpleWeDiG_Matrix;

        protected EdgeWeightedDigraph _simpleWeDAG;

        protected EdgeWeightedDigraph _simpleWeDig_N;

        protected EdgeWeightedDigraph _simpleWeDig_NC;

        protected EdgeWeightedDigraph _simpleWeDig_N_I;

        public TestDirectedWeightedGraph()
        {
            (int, int, double)[] edges = new (int, int, double)[]
            {
                (4, 5, 0.35),
                (5, 4, 0.35),
                (4, 7, 0.37),
                (5, 7, 0.28),
                (7, 5, 0.28),
                (5, 1, 0.32),
                (0, 4, 0.38),
                (0, 2, 0.26),
                (7, 3, 0.39),
                (1, 3, 0.29),
                (2, 7, 0.34),
                (6, 2, 0.40),
                (3, 6, 0.52),
                (6, 0, 0.58),
                (6, 4, 0.93)
            };
            _simpleWeDiG = new EdgeWeightedDigraph(8, edges);
            _simpleWeDiG_Matrix = new EdgeWeightedDigraph_Matrix(8, edges);

            edges = new (int, int, double)[]
            {
                (5, 4, 0.35),
                (4, 7, 0.37),
                (5, 7, 0.28),
                (5, 1, 0.32),
                (4, 0, 0.38),
                (0, 2, 0.26),
                (3, 7, 0.39),
                (1, 3, 0.29),
                (7, 2, 0.34),
                (6, 2, 0.40),
                (3, 6, 0.52),
                (6, 0, 0.58),
                (6, 4, 0.93)
            };
            _simpleWeDAG = new EdgeWeightedDigraph(8, edges);

            edges = new (int, int, double)[]
            {
                (4, 5, 0.35 ),
                (5, 4, 0.35 ),
                (4, 7, 0.37 ),
                (5, 7, 0.28 ),
                (7, 5, 0.28 ),
                (5, 1, 0.32 ),
                (0, 4, 0.38 ),
                (0, 2, 0.26 ),
                (7, 3, 0.39 ),
                (1, 3, 0.29 ),
                (2, 7, 0.34 ),
                (6, 2, -1.20),
                (3, 6, 0.52 ),
                (6, 0, -1.40),
                (6, 4, -1.25)
            };
            _simpleWeDig_N = new EdgeWeightedDigraph(8, edges);

            edges[1].Item3 = -0.66;
            _simpleWeDig_NC = new EdgeWeightedDigraph(8, edges);

            edges = new (int, int, double)[]
            {
                (4, 5, 35 ),
                (5, 4, 35 ),
                (4, 7, 37 ),
                (5, 7, 28 ),
                (7, 5, 28 ),
                (5, 1, 32 ),
                (0, 4, 38 ),
                (0, 2, 26 ),
                (7, 3, 39 ),
                (1, 3, 29 ),
                (2, 7, 34 ),
                (6, 2, -120),
                (3, 6, 52 ),
                (6, 0, -140),
                (6, 4, -125)
            };
            _simpleWeDig_N_I = new EdgeWeightedDigraph(8, edges);
        }

        private void DoTestShortestPath_PositiveWeight(IShortestPath4WeightedDigraph client)
        {
            (int vertex, double dist, string route)[] answer =
            {
                (1, 1.05, "045"),
                (2, 0.26, "0"),
                (3, 0.99, "027"),
                (4, 0.38, "0"),
                (5, 0.73, "04"),
                (6, 1.51, "0273"),
                (7, 0.60, "02")
            };

            foreach (var pair in answer)
            {
                double dist = Math.Round(client.DistTo[pair.vertex], 10);
                Assert.AreEqual(pair.dist, dist);
                IEnumerable<int> route = client.PathTo(pair.vertex).Select(x => x.From);
                Assert.AreEqual(pair.route, string.Join("", route));
            }
        }

        [TestMethod]
        public void TestShortestPath() => DoTestShortestPath_PositiveWeight(new WeightedDijkstra(_simpleWeDiG, 0));

        [TestMethod]
        public void TestShortestPaht_DenseGraph() => DoTestShortestPath_PositiveWeight(new WeightedDijkstra_Matrix(_simpleWeDiG_Matrix, 0));

        [TestMethod]
        public void TestLazyDijkstra() => DoTestShortestPath_PositiveWeight(new LazyDijkstra(_simpleWeDiG, 0));

        [TestMethod]
        public void TestShortestPath_Source_Sink()
        {
            WeightedDijkstra_Source_Sink client = new WeightedDijkstra_Source_Sink(_simpleWeDiG, 0, 6);

            double dist = Math.Round(client.DistTo[6], 10);
            Assert.AreEqual(1.51, dist);

            IEnumerable<int> route = client.PathTo(6).Select(x => x.From);
            Assert.AreEqual("0273", string.Join("", route));
        }

        [TestMethod]
        public void TestShortestPath_DAG()
        {
            ShortestPath_DAG client = new ShortestPath_DAG(_simpleWeDAG, 5);
            Assert.IsTrue(client.IsDAG);

            (int vertex, double dist, string route)[] answer =
            {
                (0, 0.73, "54"),
                (1, 0.32, "5"),
                (2, 0.62, "57"),
                (3, 0.61, "51"),
                (4, 0.35, "5"),
                (5, 0.00, ""),
                (6, 1.13, "513"),
                (7, 0.28, "5")
            };

            foreach (var pair in answer)
            {
                double dist = Math.Round(client.DistTo[pair.vertex], 10);
                Assert.AreEqual(pair.dist, dist);

                IEnumerable<int> route = client.PathTo(pair.vertex).Select(x => x.From);
                Assert.AreEqual(pair.route, string.Join("", route));
            }
        }

        [TestMethod]
        public void TestLongestPath_DAG()
        {
            LongestPath_DAG client = new LongestPath_DAG(_simpleWeDAG, 5);
            Assert.IsTrue(client.IsDAG);

            (int vertex, double dist, string route)[] answer =
            {
                (0, 2.44, "51364"),
                (1, 0.32, "5"),
                (2, 2.77, "513647"),
                (3, 0.61, "51"),
                (4, 2.06, "5136"),
                (5, 0.00, ""),
                (6, 1.13, "513"),
                (7, 2.43, "51364")
            };

            foreach (var pair in answer)
            {
                double dist = Math.Round(client.DistTo(pair.vertex), 10);
                Assert.AreEqual(pair.dist, dist);

                IEnumerable<int> route = client.PathTo(pair.vertex).Select(x => x.From);
                Assert.AreEqual(pair.route, string.Join("", route));
            }
        }

        [TestMethod]
        public void TestParallelJobScheduling()
        {
            (int job, int duration, int[] followings)[] problem =
            {
                (0, 41, new int[] { 1, 7, 9}),
                (1, 51, new int[] { 2 }),
                (2, 50, null),
                (3, 36, null),
                (4, 38, null),
                (5, 45, null),
                (6, 21, new int[] { 3, 8 }),
                (7, 32, new int[] { 3, 8 }),
                (8, 32, new int[] { 2 }),
                (9, 29, new int[] { 4, 6 })
            };
            (int job, int startTime)[] answer =
            {
                (0, 0),
                (1, 41),
                (2, 123),
                (3, 91),
                (4, 70),
                (5, 0),
                (6, 70),
                (7, 41),
                (8, 91),
                (9, 41)
            };

            ParallelJobScheduling_Simple client_S = new ParallelJobScheduling_Simple(problem);
            foreach (var pair in answer)
                Assert.AreEqual(pair.startTime, client_S.StartTime(pair.job));

            (int job, int time, int relativeTo)[] deadlines =
            {
                (2, 12, 4),
                (2, 70, 7)
            };
            ParallelJobScheduling_Deadlines client_D = new ParallelJobScheduling_Deadlines(problem, deadlines);
            answer[4].startTime = 111;
            answer[7].startTime = 53;
            foreach (var pair in answer)
                Assert.AreEqual(pair.startTime, client_D.StartTime(pair.job));

            (int job, int time, int relativeTo)[] deadlines2 =
            {
                (2, 12, 4),
                (2, 70, 7),
                (4, 80, 0)
            };
            client_D = new ParallelJobScheduling_Deadlines(problem, deadlines2);
            Assert.IsTrue(client_D.HasCycle);
            HashSet<int> cycle = client_D.Cycle.ToHashSet();
            Assert.IsTrue(cycle.IsSupersetOf(new int[] { 0, 10, 2, 4, }));
        }

        private void DoTestShorestPath_NegativeWeight(Func<BellmanFord> fFeasible, Func<BellmanFord> fCycle)
        {
            BellmanFord client = fFeasible();
            (int vertex, double dist, string route)[] answer =
            {
                (1, 0.93, "0273645"),
                (5, 0.61, "027364"),
                (4, 0.26, "02736"),
                (6, 1.51, "0273"),
                (3, 0.99, "027"),
                (7, 0.60, "02"),
                (2, 0.26, "0"),
                (0, 0.00, "")
            };

            foreach (var triple in answer)
            {
                double dist = Math.Round(client.DistTo[triple.vertex], 10);
                Assert.AreEqual(triple.dist, dist);

                IEnumerable<int> route = client.PathTo(triple.vertex).Select(x => x.From);
                Assert.AreEqual(triple.route, string.Join("", route));
            }

            client = fCycle();
            Assert.IsTrue(client.HasCycle);
            string cycleRoute = string.Join("", client.Cycle);
            Assert.AreEqual("454", cycleRoute);
        }

        [TestMethod]
        public void TestBellmanFord()
        {
            DoTestShorestPath_NegativeWeight(
                () => new BellmanFord(_simpleWeDig_N, 0),
                () => new BellmanFord(_simpleWeDig_NC, 0));
        }

        [TestMethod]
        public void TestBellmanFord_ParentChecking()
        {
            DoTestShorestPath_NegativeWeight(
                () => new BellmanFord_ParentChecking(_simpleWeDig_N, 0),
                () => new BellmanFord_ParentChecking(_simpleWeDig_NC, 0));
        }

        private void DoTestNegetiveCycleDetection(Func<EdgeWeightedDigraph, NegativeCycleDetectionBase> fCreateInstance)
        {
            NegativeCycleDetectionBase client = fCreateInstance(_simpleWeDig_NC);
            Assert.IsTrue(client.HasCycle);
            Assert.IsTrue(client.Cycle.ToHashSet().IsSupersetOf(new int[] { 4, 5 }));

            client = fCreateInstance(_simpleWeDig_N);
            Assert.IsFalse(client.HasCycle);
        }

        [TestMethod]
        public void TestNegetiveCycleDetection() => DoTestNegetiveCycleDetection(x => new NegetiveCycleDetection(x));

        [TestMethod]
        public void TestNegativeCycleDetection_Zero() => DoTestNegetiveCycleDetection(x => new NegativeCycleDetection_Zero(x));

        [TestMethod]
        public void TestShortestpathInAGrid()
        {
            int[,] matrix =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            ShortestPathInAGrid client = new ShortestPathInAGrid(matrix);

            IEnumerable<int> path = client.PathTo((2, 2));
            Assert.AreEqual("12369", string.Join("", path));

            path = client.PathTo((2, 1));
            Assert.AreEqual("1258", string.Join("", path));
        }

        [TestMethod]
        public void TestNeighbors()
        {
            (int vertex, double dist, string route)[] answer =
            {
                (1, 1.05, "045"),
                (2, 0.26, "0"),
                (3, 0.99, "027"),
                (4, 0.38, "0"),
                (5, 0.73, "04"),
                (6, 1.51, "0273"),
                (7, 0.60, "02")
            };

            double distance = 1;
            Neighbors client = new Neighbors(_simpleWeDiG, 0, distance);
            foreach (var pair in answer)
            {
                double dist = Math.Round(client.DistTo[pair.vertex], 10);
                if (dist <= distance)
                {
                    Assert.AreEqual(pair.dist, dist);
                    IEnumerable<int> route = client.PathTo(pair.vertex).Select(x => x.From);
                    Assert.AreEqual(pair.route, string.Join("", route));
                }
                else
                {
                    Assert.AreEqual(double.PositiveInfinity, dist);
                    Assert.AreEqual(0, client.PathTo(pair.vertex).Count());
                }
            }
        }

        [TestMethod]
        public void TestCriticalWeightedEdge()
        {
            (int from, int to, double weight)[] edges =
            {
                (0, 1, 1),
                (1, 2, 4),
                (0, 3, 2),
                (3, 2, 3),
                (2, 4, 5),
                (3, 4, 9)
            };
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(5, edges);
            CriticalWeightedEdges client = new CriticalWeightedEdges(g, 0, 4);
            Assert.AreEqual(1, client.CriticalEdges().Count());

            Edge e = client.CriticalEdges().First();
            Assert.AreEqual(2, e.From);
            Assert.AreEqual(4, e.To);

            bool[,] sensitivity = client.Sensitivity();

            foreach (var item in edges)
            {
                if (item.from == 2 && item.to == 4)
                    Assert.IsFalse(sensitivity[item.from, item.to]);
                else
                    Assert.IsTrue(sensitivity[item.from, item.to]);
            }
        }

        [TestMethod]
        public void TestBiDirectionalSearch()
        {
            BiDirectionalSearch client = new BiDirectionalSearch(_simpleWeDiG, 0, 6);
            double spWeight = Math.Round(client.Distance, 10);
            Assert.AreEqual(1.51, spWeight);
            string shortestPath = string.Join("", client.Edges.Select(x => x.From));
            Assert.AreEqual("0273", shortestPath);
        }

        [TestMethod]
        public void TestDijkstraNegativeWeight()
        {
            WeightedDijkstra client = new WeightedDijkstra(_simpleWeDig_N, 0);
            (int vertex, double dist, string route)[] answer =
            {
                (1, 0.93, "0273645"),
                (5, 0.61, "027364"),
                (4, 0.26, "02736"),
                (6, 1.51, "0273"),
                (3, 0.99, "027"),
                (7, 0.60, "02"),
                (2, 0.26, "0"),
                (0, 0.00, "")
            };

            foreach (var triple in answer)
            {
                double dist = Math.Round(client.DistTo[triple.vertex], 10);
                Assert.AreEqual(triple.dist, dist);

                IEnumerable<int> route = client.PathTo(triple.vertex).Select(x => x.From);
                Assert.AreEqual(triple.route, string.Join("", route));
            }
        }

        [TestMethod]
        public void TestFastBellmanFord()
        {
            FastBellmanFord client = new FastBellmanFord(_simpleWeDig_N_I, 0, 140);
            (int vertex, double dist, string route)[] answer =
            {
                (0, 0, ""),
                (1, 93, "0273645"),
                (2, 26, "0"),
                (3, 99, "027"),
                (4, 26, "02736"),
                (5, 61, "027364"),
                (6, 151, "0273"),
                (7, 60, "02")
            };
            foreach (var triple in answer)
            {
                double dist = Math.Round(client.DistTo[triple.vertex], 10);
                Assert.AreEqual(triple.dist, dist);

                IEnumerable<int> route = client.PathTo(triple.vertex).Select(x => x.From);
                Assert.AreEqual(triple.route, string.Join("", route));
            }
        }

        private void DoTestMonotonicPath(
            Monotonicity mType,
            int vertex,
            (int from, int to, double dist)[] edges,
            (int t, double dist, string path)[] answers,
            int[] noAnswers)
        {
            for (int i = 0; i < 10; i++)
            {
                EdgeWeightedDigraph g = new EdgeWeightedDigraph(vertex, edges);
                IMonotonicShortestPath client;
                if (mType == Monotonicity.Increasing)
                    client = new MonotonicIncreasingShortestpath(g, 0);
                else if (mType == Monotonicity.Decreasing)
                    client = new MonotonicDecreasingShorestPath(g, 0);
                else
                    client = new BitonicShortestPath(g, 0);

                foreach (var triple in answers)
                {
                    double dist = Math.Round(client.DistTo(triple.t), 10);
                    Assert.AreEqual(triple.dist, dist);

                    IEnumerable<int> route = client.PathTo(triple.t).Select(x => x.From);
                    Assert.AreEqual(triple.path, string.Join("", route));
                }
                foreach (var v in noAnswers)
                {
                    Assert.AreEqual(double.PositiveInfinity, client.DistTo(v));
                }

                Util.Shuffle(edges);
            }
        }


        [TestMethod]
        public void TestMonotonicDecreasingPath()
        {
            (int from, int to, double dist)[] edges =
            {
                (0, 1, 10),
                (1, 2, 1),
                (0, 3, 20),
                (3, 2, -30),
                (2, 4, -5),
                (2, 5, -35),
                (2, 6, 10)
            };
            (int t, double dist, string path)[] answers =
            {
                (1, 10, "0"),
                (2, -10, "03"),
                (3, 20, "0"),
                (4, 6, "012"),
                (5, -45, "032"),
            };
            int[] noAnswers = { 6 };
            DoTestMonotonicPath(Monotonicity.Decreasing, 7, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 6),
                (0, 2, 15),
                (2, 1, 12),
                (1, 3, 9),
                (1, 4, 3)
            };
            answers = new (int t, double dist, string path)[]
            {
                (1, 6, "0"),
                (2, 15, "0"),
                (3, 36, "021"),
                (4, 9, "01"),
            };
            noAnswers = new int[0];
            DoTestMonotonicPath(Monotonicity.Decreasing, 5, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 20),
                (0, 2, 15),
                (2, 1, 10),
                (1, 3, 5),
                (1, 4, -5)
            };
            answers = new (int t, double dist, string path)[]
            {
                (1, 20, "0"),
                (2, 15, "0"),
                (3, 25, "01"),
                (4, 15, "01")
            };
            noAnswers = new int[0];
            DoTestMonotonicPath(Monotonicity.Decreasing, 5, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 4),
                (1, 2, 3),
                (2, 5, -1),
                (2, 3, 2),
                (3, 6, -2),
                (3, 1, 1),
                (1, 4, -3)
            };
            answers = new (int t, double dist, string path)[]
            {
                (1, 4, "0"),
                (2, 7, "01"),
                (5, 6, "012"),
                (3, 9, "012"),
                (6, 7, "0123"),
                (4, 1, "01")
            };
            noAnswers = new int[0];
            DoTestMonotonicPath(Monotonicity.Decreasing, 7, edges, answers, noAnswers);
        }

        [TestMethod]
        public void TestMonotonicIncreasingPath()
        {
            (int from, int to, double dist)[] edges =
            {
                (0, 1, -30),
                (1, 2, 20),
                (0, 3, 1),
                (3, 2, 10),
                (2, 4, 15),
                (2, 5, 35),
                (2, 6, 5)
            };
            (int t, double dist, string path)[] answers =
            {
                (1, -30, "0"),
                (2, -10, "01"),
                (3, 1, "0"),
                (4, 26, "032"),
                (5, 25, "012"),
            };
            int[] noAnswers = { 6 };
            DoTestMonotonicPath(Monotonicity.Increasing, 7, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 8),
                (0, 2, 4),
                (2, 1, 5),
                (1, 3, 6),
                (1, 4, 9)
            };
            answers = new (int t, double dist, string path)[]
            {
                (1, 8, "0"),
                (2, 4, "0"),
                (3, 15, "021"),
                (4, 17, "01"),
            };
            noAnswers = new int[0];
            DoTestMonotonicPath(Monotonicity.Increasing, 5, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 20),
                (0, 2, 10),
                (2, 1, 15),
                (1, 3, 25),
                (1, 4, 30)
            };
            answers = new (int t, double dist, string path)[]
            {
                (1, 20, "0"),
                (2, 10, "0"),
                (3, 45, "01"),
                (4, 50, "01")
            };
            noAnswers = new int[0];
            DoTestMonotonicPath(Monotonicity.Increasing, 5, edges, answers, noAnswers);
        }

        [TestMethod]
        public void TestBitonicPath()
        {
            (int from, int to, double dist)[] edges;
            (int t, double dist, string path)[] answers;
            int[] noAnswers;

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 5),
                (1, 2, 10),
                (2, 0, 7)
            };
            answers = new (int t, double dist, string path)[0];
            noAnswers = new int[] { 1, 2 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 3, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 5),
                (1, 2, 20),
                (2, 1, 10)
            };
            answers = new (int t, double dist, string path)[0];
            noAnswers = new int[] { 0, 1, 2 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 3, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 10),
                (1, 2, 5)
            };
            answers = new (int t, double dist, string path)[]
            {
                (2, 15, "01")
            };
            noAnswers = new int[] { 1 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 3, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 10),
                (1, 2, 20),
                (1, 3, 5),
                (2, 4, 15)
            };
            answers = new (int t, double dist, string path)[]
            {
                (3, 15, "01"),
                (4, 45, "012")
            };
            noAnswers = new int[] { 1, 2 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 5, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 5),
                (1, 2, 10)
            };
            answers = new (int t, double dist, string path)[0];
            noAnswers = new int[] { 1, 2 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 3, edges, answers, noAnswers);

            edges = new (int from, int to, double dist)[]
            {
                (0, 1, 40),
                (1, 3, 50),
                (0, 2, 20),
                (2, 3, 30),
                (3, 4, 45),
                (3, 5, 25),
                (3, 6, 5),
                (0, 7, 15),
                (7, 3, 10),
                (3, 8, 60)
            };
            answers = new (int t, double dist, string path)[]
            {
                (4, 135, "013"),
                (5, 75, "023"),
                (6, 30, "073"),
                (3, 25, "07")
            };
            noAnswers = new int[] { 1, 2, 8 };
            DoTestMonotonicPath(Monotonicity.Bitonic, 9, edges, answers, noAnswers);
        }
    }
}
