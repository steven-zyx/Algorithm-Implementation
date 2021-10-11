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

        protected EdgeWeightedDigraph(int v)
        {
            V = v;
            _adjacencyList = new Bag_L<DirectedEdge>[V];
            for (int i = 0; i < V; i++)
                _adjacencyList[i] = new Bag_L<DirectedEdge>();
        }

        public EdgeWeightedDigraph(int v, IEnumerable<(int, int, double)> edges) : this(v)
        {
            foreach (var edge in edges)
                AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        protected void AddEdge(int v, int w, double weight)
        {
            _adjacencyList[v].Add(new DirectedEdge(v, w, weight));
            E++;
        }

        public IEnumerable<DirectedEdge> Edges()
        {
            foreach (var bag in _adjacencyList)
                foreach (DirectedEdge edge in bag)
                    yield return edge;
        }

        public IEnumerable<DirectedEdge> Adj(int v) => _adjacencyList[v];
    }
}
