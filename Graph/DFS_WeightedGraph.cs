using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class DFS_WeightedGraph
    {
        public bool[] Marked { get; protected set; }
        protected EdgeWeightedGraph _g;

        public DFS_WeightedGraph(EdgeWeightedGraph g, int v)
        {
            _g = g;
            Marked = new bool[g.V];
            DFS(v);
        }

        protected void DFS(int v)
        {
            Marked[v] = true;
            foreach (Edge e in _g.Adj(v))
            {
                int w = e.Other(v);
                if (!Marked[w])
                    DFS(w);
            }
        }
    }
}
