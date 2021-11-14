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
            foreach (Edge e in _g.Adj(currentP.LastV))
                if (e.Weight > currentP.LastEdgeWeight)
                {
                    int w = e.To;
                    EdgeWeightedPath oldP = _paths[w];
                    if (oldP == null || currentP.Weight + e.Weight < oldP.Weight)
                    {
                        _paths[w] = currentP.CopyAndAdd(e);
                        _q.Enqueue(_paths[w]);
                    }
                    else if (currentP.LastEdgeWeight < oldP.LastEdgeWeight)
                    {
                        _q.Enqueue(currentP.CopyAndAdd(e));
                    }
                }
        }
    }
}
