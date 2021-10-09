using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class LazyPrim
    {
        protected EdgeWeightedGraph _g;
        protected bool[] _marked;
        protected MinPQ<Edge> _pq;
        protected Queue_N<Edge> _mstEdges;

        public double Weight { get; protected set; }


        public LazyPrim(EdgeWeightedGraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _pq = new MinPQ<Edge>(g.E);
            _mstEdges = new Queue_N<Edge>();

            Visit(0);
            while (!_pq.IsEmpty)
            {
                Edge minEdge = _pq.DeleteMin();
                int v = minEdge.Either(), w = minEdge.Other(v);
                if (_marked[v] && _marked[w])
                    continue;
                if (_marked[v])
                    Visit(w);
                else
                    Visit(v);
                _mstEdges.Enqueue(minEdge);
                Weight += minEdge.Weight;
            }
        }

        protected void Visit(int v)
        {
            _marked[v] = true;
            foreach (Edge e in _g.Adj(v))
                if (!_marked[e.Other(v)])
                    _pq.Insert(e);
        }

        public IEnumerable<Edge> Edges() => _mstEdges;
    }
}
