using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedGraph
    {
        protected Bag_L<Edge>[] _adjacencyList;

        public int V { get; }

        public int E { get; protected set; }

        protected EdgeWeightedGraph(int v)
        {
            V = v;
            _adjacencyList = new Bag_L<Edge>[V];
            for (int i = 0; i < v; i++)
                _adjacencyList[i] = new Bag_L<Edge>();
        }

        public EdgeWeightedGraph(int v, IEnumerable<(int, int, double)> edges) : this(v)
        {
            foreach (var edge in edges)
                AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        public void AddEdge(int v, int w, double weight)
        {
            Edge e = new Edge(v, w, weight);
            _adjacencyList[v].Add(e);
            _adjacencyList[w].Add(e);
            E++;
        }

        public IEnumerable<Edge> Adj(int v) => _adjacencyList[v];

        public IEnumerable<Edge> Edges()
        {
            for (int i = 0; i < V; i++)
                foreach (Edge e in _adjacencyList[i])
                    if (i < e.Other(i))
                        yield return e;
        }

    }
}
