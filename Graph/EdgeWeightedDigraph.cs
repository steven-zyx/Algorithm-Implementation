using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedDigraph : EdgeWeightedGraph
    {
        public EdgeWeightedDigraph(int v) : base(v) { }

        public EdgeWeightedDigraph(int v, IEnumerable<(int, int, double)> edges) : base(v, edges) { }

        public override void AddEdge(Edge e)
        {
            _adjacencyList[e.From].Add(e);
            E++;
        }

        public override void RemoveEdge(Edge e)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Edge> Edges()
        {
            foreach (var bag in _adjacencyList)
                foreach (Edge edge in bag)
                    yield return edge;
        }

        public Digraph Convert()
        {
            Digraph dg = new Digraph(V);
            foreach (Bag_L<Edge> edges in _adjacencyList)
                foreach (Edge e in edges)
                    dg.AddEdge(e.From, e.To);
            return dg;
        }
    }
}
