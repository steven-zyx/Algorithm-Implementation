using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Certification
    {
        public bool IsMSTEdges { get; }

        public Certification(EdgeWeightedGraph g, IEnumerable<Edge> edges)
        {
            HashSet<Edge> edgeSet = new HashSet<Edge>(edges);
            if (edgeSet.Count != g.V - 1)
            {
                IsMSTEdges = false;
                return;
            }

            HashSet<int> left = new HashSet<int>();
            left.Add(0);
            HashSet<int> right = Enumerable.Range(1, g.V - 1).ToHashSet();

            while (right.Count > 0)
            {
                Edge minEdge = new Edge(-1, -1, double.PositiveInfinity);
                foreach (int v in left)
                    foreach (Edge e in g.Adj(v))
                        if (right.Contains(e.Other(v)) && e.Weight < minEdge.Weight)
                            minEdge = e;
                if (!edgeSet.Contains(minEdge))
                {
                    IsMSTEdges = false;
                    return;
                }
                else
                {
                    int v = minEdge.Either(), w = minEdge.Other(v);
                    if (right.Contains(v))
                    {
                        left.Add(v);
                        right.Remove(v);
                    }
                    else
                    {
                        left.Add(w);
                        right.Remove(w);
                    }
                }
            }
            IsMSTEdges = true;
        }
    }
}
