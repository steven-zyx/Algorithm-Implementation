using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class EagerPrim : IMST
    {
        protected EdgeWeightedGraph _g;
        protected IndexMinPQ<Edge> _pq;
        protected bool[] _marked;
        protected Queue_N<Edge> _minEdges;

        public double Weight { get; protected set; }

        public EagerPrim(EdgeWeightedGraph g)
        {
            _g = g;
            _pq = new IndexMinPQ<Edge>(g.V);
            _marked = new bool[g.V];
            _minEdges = new Queue_N<Edge>();

            _marked[0] = true;
            foreach (Edge startE in _g.Adj(0))
                _pq.Insert(startE.Other(0), startE);

            while (!_pq.IsEmpty)
            {
                Edge minEdge = _pq.Min;
                int v = _pq.DelMin();
                _marked[v] = true;
                foreach (Edge e in _g.Adj(v))
                {
                    int w = e.Other(v);
                    if (_marked[w])
                        continue;
                    if (_pq.Contains(w))
                    {
                        if (_pq.Get(w).Weight > e.Weight)
                            _pq.Change(w, e);
                    }
                    else
                        _pq.Insert(w, e);
                }
                _minEdges.Enqueue(minEdge);
                Weight += minEdge.Weight;
            }
        }


        public IEnumerable<Edge> Edges() => _minEdges;
    }
}
