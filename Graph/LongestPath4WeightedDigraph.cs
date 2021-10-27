using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public abstract class LongestPath4WeightedDigraph
    {
        protected ShortestPath4WeightedDigraph _client;

        protected EdgeWeightedDigraph Negate(EdgeWeightedDigraph g)
        {
            EdgeWeightedDigraph nagetiveG = new EdgeWeightedDigraph(g.V);
            for (int i = 0; i < g.V; i++)
                foreach (DirectedEdge e in g.Adj(i))
                    nagetiveG.AddEdge(e.From, e.To, -e.Weight);
            return nagetiveG;
        }

        public double DistTo(int v) => -_client.DistTo[v];

        public IEnumerable<DirectedEdge> PathTo(int v) => _client.PathTo(v);
    }
}
