using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedDigraph
    {
        public int V { get; }

        public int E { get; protected set; }

        protected Bag_L<DirectedEdge>[] _adjacencyList;

        internal EdgeWeightedDigraph(int v)
        {
            V = v;
            _adjacencyList = new Bag_L<DirectedEdge>[V];
            for (int i = 0; i < V; i++)
                _adjacencyList[i] = new Bag_L<DirectedEdge>();
        }

        public EdgeWeightedDigraph((int, int, double)[] edges) : this(edges.Length)
        {
            foreach (var edge in edges)
                AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        public void AddEdge(DirectedEdge e)
        {
            _adjacencyList[e.From].Add(e);
            E++;
        }

        public void AddEdge(int v, int w, double weight) => AddEdge(new DirectedEdge(v, w, weight));

        public IEnumerable<DirectedEdge> Edges()
        {
            foreach (var bag in _adjacencyList)
                foreach (DirectedEdge edge in bag)
                    yield return edge;
        }

        public IEnumerable<DirectedEdge> Adj(int v) => _adjacencyList[v];

        public Digraph Convert()
        {
            Digraph dg = new Digraph(V);
            foreach (Bag_L<DirectedEdge> edges in _adjacencyList)
                foreach (DirectedEdge e in edges)
                    dg.AddEdge(e.From, e.To);
            return dg;
        }
    }
}
