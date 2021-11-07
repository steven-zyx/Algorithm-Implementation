using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPathBetween2Subset : MultisourceShortestPath
    {
        protected int _nearestV;

        public ShortestPathBetween2Subset(EdgeWeightedDigraph g, IEnumerable<int> s, IEnumerable<int> t)
            : base(g, s)
        {
            _nearestV = t.First();
            foreach (int v in t)
                if (DistTo[v] < DistTo[_nearestV])
                    _nearestV = v;
        }

        public IEnumerable<Edge> PathFromStoT()
        {
            for (int v = _nearestV; _edgeTo[v] != null; v = _edgeTo[v].From)
                yield return _edgeTo[v];
        }
    }
}
