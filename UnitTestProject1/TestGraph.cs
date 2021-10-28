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
        protected Digraph _simpleDiG => _simpleG as Digraph;

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
            _simpleG = new Digraph(new int[] { 7, 5, 0, 1, 1, 2, 2, 3, 4, 5 });
            Topological client = new Topological(_simpleDiG);
            Assert.IsTrue(client.IsDAG());
            Assert.AreEqual("6450123", string.Join("", client.Order()));
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
    }

    [TestClass]
    public class TestWeightedGraph
    {
        protected EdgeWeightedGraph _simpleWeG;

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
            Assert.AreEqual(1.81, client.Weight);
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
    }

    [TestClass]
    public class TestDirectedWeightedGraph
    {
        protected EdgeWeightedDigraph _simpleWeDiG;

        protected EdgeWeightedDigraph _simpleWeDAG;

        protected EdgeWeightedDigraph _simpleWeDig_N;

        protected EdgeWeightedDigraph _simpleWeDig_NC;

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
            _simpleWeDiG = new EdgeWeightedDigraph(edges);

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
            _simpleWeDAG = new EdgeWeightedDigraph(edges);

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
            _simpleWeDig_N = new EdgeWeightedDigraph(edges);

            edges[1].Item3 = -0.66;
            _simpleWeDig_NC = new EdgeWeightedDigraph(edges);
        }

        [TestMethod]
        public void TestShortestPath()
        {
            ShortestPath client = new ShortestPath(_simpleWeDiG, 0);
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
        public void TestShortestPath_Source_Sink()
        {
            ShortestPath_Source_Sink client = new ShortestPath_Source_Sink(_simpleWeDiG, 0, 6);

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

        [TestMethod]
        public void TestShortestPath_NativeCycle()
        {
            ShortestPath_NagetiveCycle client = new ShortestPath_NagetiveCycle(_simpleWeDig_N, 0);
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

            client = new ShortestPath_NagetiveCycle(_simpleWeDig_NC, 0);
            Assert.IsTrue(client.HasCycle);
            string cycleRoute = string.Join("", client.Cycle);
            Assert.AreEqual("454", cycleRoute);
        }

        [TestMethod]
        public void TestNegetiveCycleDetection()
        {
            NegetiveCycleDetection client = new NegetiveCycleDetection(_simpleWeDig_NC);
            Assert.IsTrue(client.HasCycle);
            string cycleRoute = string.Join("", client.Cycle);
            Assert.AreEqual("454", cycleRoute);

            client = new NegetiveCycleDetection(_simpleWeDig_N);
            Assert.IsFalse(client.HasCycle);
        }
    }
}
