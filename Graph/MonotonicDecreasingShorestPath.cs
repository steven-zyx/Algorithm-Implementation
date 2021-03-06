using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class MonotonicDecreasingShorestPath : IMonotonicShortestPath
    {
        protected EdgeWeightedDigraph _g;
        protected Queue<EdgeWeightedPath> _q;
        protected EdgeWeightedPath[] _paths;

        public MonotonicDecreasingShorestPath(EdgeWeightedDigraph g, int s)
        {
            _g = g;
            _q = new Queue<EdgeWeightedPath>();
            _paths = new EdgeWeightedPath[g.V];

            foreach (Edge e in _g.Adj(s))
            {
                int w = e.To;
                _paths[w] = new EdgeWeightedPath()
                {
                    Edges = new List<Edge>() { e },
                    Weight = e.Weight
                };
                _q.Enqueue(_paths[w]);
            }
            while (_q.Count() > 0)
                Relax(_q.Dequeue());
        }

        protected virtual void Relax(EdgeWeightedPath currentP)
        {
            foreach (Edge e in _g.Adj(currentP.LastV))
                if (e.Weight < currentP.LastEdgeWeight)
                {
                    int w = e.To;
                    EdgeWeightedPath oldP = _paths[w];
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
