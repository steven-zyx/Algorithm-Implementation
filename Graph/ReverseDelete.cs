using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ReverseDelete : IMST
    {
        protected EdgeWeightedGraph _g;

        public ReverseDelete(EdgeWeightedGraph g)
        {
            _g = g;
            Edge[] edges = g.Edges().OrderByDescending(x => x.Weight).ToArray();
            foreach (Edge e in edges)
            {
                g.RemoveEdge(e);
                int v = e.Either(), w = e.Other(v);
                DFS_WeightedGraph dfsClient = new DFS_WeightedGraph(g, v);
                if (!dfsClient.Marked[w])
                    g.AddEdge(e);
            }
        }

        public double Weight => Edges().Sum(x => x.Weight);

        public IEnumerable<Edge> Edges() => _g.Edges();
    }
}
