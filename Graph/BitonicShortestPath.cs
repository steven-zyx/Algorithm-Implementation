using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class BitonicShortestPath : IMonotonicShortestPath
    {
        protected EdgeWeightedDigraph _g;
        protected Queue<BitonicPath> _q;
        protected BitonicPath[] _paths;

        public BitonicShortestPath(EdgeWeightedDigraph g, int s)
        {
            _g = g;
            _q = new Queue<BitonicPath>();
            _paths = new BitonicPath[g.V];

            foreach (Edge e in _g.Adj(s))
            {
                int w = e.To;
                _q.Enqueue(new BitonicPath()
                {
                    Edges = new List<Edge> { e },
                    Weight = e.Weight,
                    Vertices = new HashSet<int>() { s, w },
                    IsIncreasing = true
                });
            }

            while (_q.Count() > 0)
                Relax(_q.Dequeue());
        }

        public void Relax(BitonicPath currentP)
        {
            double lastWeight = currentP.LastEdgeWeight;
            foreach (Edge e in _g.Adj(currentP.LastV))
            {
                int w = e.To;
                if (currentP.Vertices.Contains(w))
                    continue;

                if (currentP.IsIncreasing && e.Weight > lastWeight)
                {
                    _q.Enqueue(currentP.CopyAndAdd(e));
                }
                else if (e.Weight < lastWeight)
                {
                    BitonicPath oldP = _paths[w];
                    if (oldP == null || currentP.Weight + e.Weight < oldP.Weight)
                    {
                        _paths[w] = currentP.CopyAndAdd(e);
                        _q.Enqueue(_paths[w]);
                    }
                    else if (currentP.LastEdgeWeight > oldP.LastEdgeWeight)
                    {
                        _q.Enqueue(currentP.CopyAndAdd(e));
                    }
                }
            }
        }


        public double DistTo(int v)
        {
            if (_paths[v] != null)
                return _paths[v].Weight;
            else
                return double.PositiveInfinity;
        }

        public IEnumerable<Edge> PathTo(int v)
        {
            if (_paths[v] != null)
                return _paths[v].Edges;
            else
                return null;
        }
    }
}
