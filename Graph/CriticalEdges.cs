using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class CriticalEdges
    {
        protected EdgeWeightedGraph _g;
        protected bool[] _marked;
        protected MinPQ<Edge> _pq;
        protected Queue<Edge> _criticalEdges;

        public CriticalEdges(EdgeWeightedGraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _pq = new MinPQ<Edge>(g.E);
            _criticalEdges = new Queue<Edge>();

            Visit(0);
            while (!_pq.IsEmpty)
            {
                Edge minEdge = _pq.DeleteMin();
                if (Valid(minEdge))
                {
                    Edge secondE = _pq.Min;
                    if (Valid(secondE) && secondE.Weight > minEdge.Weight)
                        _criticalEdges.Enqueue(minEdge);

                    int v = minEdge.Either(), w = minEdge.Other(v);
                    if (_marked[v])
                        Visit(w);
                    else
                        Visit(v);
                }
            }
        }

        protected void Visit(int v)
        {
            _marked[v] = true;
            foreach (Edge e in _g.Adj(v))
                if (!_marked[e.Other(v)])
                    _pq.Insert(e);
        }

        protected bool Valid(Edge e)
        {
            int v = e.Either(), w = e.Other(v);
            return !_marked[v] || !_marked[w];
        }

        public IEnumerable<Edge> Critical() => _criticalEdges;
    }
}
