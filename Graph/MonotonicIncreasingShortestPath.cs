using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class MonotonicIncreasingShortestpath : MonotonicDecreasingShorestPath
    {
        public MonotonicIncreasingShortestpath(EdgeWeightedDigraph g, int s) : base(g, s) { }

        protected override void Relax(EdgeWeightedPath currentP)
        {
            int v = currentP.LastV;
            double lastWeight = currentP.LastEdgeWeight;
            foreach (Edge e in _g.Adj(v))
                if (e.Weight > lastWeight)
                {
                    int w = e.To;
                    EdgeWeightedPath path = _paths[w];
                    if (path == null || currentP.Weight + e.Weight < path.Weight)
                    {
                        _paths[w] = currentP.CopyAndAdd(e);
                        _q.Enqueue(_paths[w]);
                    }
                    else if (currentP.LastEdgeWeight < path.LastEdgeWeight)
                    {
                        _q.Enqueue(currentP.CopyAndAdd(e));
                    }
                }
        }
    }
}
