using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class LazyDijkstra : ShortestPath4WeightedDigraph
    {
        protected bool[] _marked;
        protected MinPQ<Edge> _pq;

        public LazyDijkstra(EdgeWeightedDigraph g, int s) : base(g)
        {
            _marked = new bool[g.V];
            _pq = new MinPQ<Edge>(g.E, new EdgeComparer(DistTo));

            DistTo[s] = 0;
            Relax(s);
            while (_pq.Size > 0)
            {
                Edge e = _pq.DeleteMin();
                if (_marked[e.To])
                    continue;

                _edgeTo[e.To] = e;
                DistTo[e.To] = DistTo[e.From] + e.Weight;
                Relax(e.To);
            }
        }

        protected void Relax(int v)
        {
            _marked[v] = true;
            foreach (Edge e in _g.Adj(v))
            {
                _pq.Insert(e);
            }
        }

        class EdgeComparer : IComparer<Edge>
        {
            protected double[] _distTo;

            public EdgeComparer(double[] distTo)
            {
                _distTo = distTo;
            }

            public int Compare(Edge x, Edge y)
            {
                double distToX = _distTo[x.From] + x.Weight;
                double distToY = _distTo[y.From] + y.Weight;
                if (distToX < distToY)
                    return -1;
                else if (distToX > distToY)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
