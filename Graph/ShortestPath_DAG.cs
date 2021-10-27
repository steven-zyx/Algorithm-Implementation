using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath_DAG : ShortestPath4WeightedDigraph
    {
        public ShortestPath_DAG(EdgeWeightedDigraph g, int s) : base(g, s)
        {
            Topological client = new Topological(_g.Convert());
            IsDAG = client.IsDAG();

            if (IsDAG)
            {
                int[] order = client.Order().ToArray();
                int start = 0;
                for (; start < _g.V; start++)
                    if (order[start] == s)
                        break;
                for (; start < _g.V; start++)
                    Relax(order[start]);
            }
        }

        protected void Relax(int v)
        {
            foreach (DirectedEdge e in _g.Adj(v))
            {
                int w = e.To;
                if (DistTo[w] > DistTo[v] + e.Weight)
                {
                    DistTo[w] = DistTo[v] + e.Weight;
                    _edgeTo[w] = e;
                }
            }
        }

        public bool IsDAG { get; }
    }
}
