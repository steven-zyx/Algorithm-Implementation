using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LongestPath_DAG
    {
        protected ShortestPath_DAG _client;

        public LongestPath_DAG(EdgeWeightedDigraph g, int s)
        {
            EdgeWeightedDigraph nagetiveG = new EdgeWeightedDigraph(g.V);
            for (int i = 0; i < g.V; i++)
                foreach (DirectedEdge e in g.Adj(i))
                    nagetiveG.AddEdge(e.From, e.To, -e.Weight);

            _client = new ShortestPath_DAG(nagetiveG, s);
        }

        public double DistTo(int v) => -_client.DistTo[v];

        public IEnumerable<DirectedEdge> PathTo(int v) => _client.PathTo(v);

        public bool IsDAG => _client.IsDAG;
    }
}
