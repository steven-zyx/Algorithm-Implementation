using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class MonotonicDecreasingShorestPath
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
            int v = currentP.LastV;
            double lastWeight = currentP.LastEdgeWeight;
            foreach (Edge e in _g.Adj(v))
                if (e.Weight < lastWeight)
                {
                    int w = e.To;
                    EdgeWeightedPath path = _paths[w];
                    if (path == null || currentP.Weight + e.Weight < path.Weight)
                    {
                        _paths[w] = currentP.CopyAndAdd(e);
                        _q.Enqueue(_paths[w]);
                    }
                    else if (currentP.LastEdgeWeight > path.LastEdgeWeight)
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

    public class EdgeWeightedPath
    {
        public double Weight { get; set; }

        public List<Edge> Edges { get; set; }

        public int LastV => Edges[Edges.Count - 1].To;

        public double LastEdgeWeight => Edges[Edges.Count - 1].Weight;

        public EdgeWeightedPath CopyAndAdd(Edge e)
        {
            EdgeWeightedPath newPath = new EdgeWeightedPath();
            newPath.Edges = new List<Edge>();
            newPath.Edges.AddRange(this.Edges);
            newPath.Edges.Add(e);
            newPath.Weight = this.Weight + e.Weight;
            return newPath;
        }
    }
}
