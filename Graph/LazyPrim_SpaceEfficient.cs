using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class LazyPrim_SpaceEfficient : IMST
    {
        protected EdgeWeightedGraph_Matrix _g;
        protected bool[] _marked;
        protected MinPQ<(int v, int w)> _pq;
        protected Queue<(int v, int w)> _mstEdges;

        public LazyPrim_SpaceEfficient(EdgeWeightedGraph_Matrix g)
        {
            _g = g;
            _marked = new bool[g.V];
            _pq = new MinPQ<(int, int)>(_g.E, _g);
            _mstEdges = new Queue<(int, int)>(g.V - 1);

            Visit(0);
            while (!_pq.IsEmpty)
            {
                var e = _pq.DeleteMin();
                if (_marked[e.v] && _marked[e.w])
                    continue;

                _mstEdges.Enqueue((e.v, e.w));
                if (_marked[e.v])
                    Visit(e.w);
                else
                    Visit(e.v);
            }
        }


        protected void Visit(int v)
        {
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!_marked[w])
                    _pq.Insert((v, w));
        }

        public double Weight => _mstEdges.Sum(e => _g.Weight(e.v, e.w));

        public IEnumerable<Edge> Edges() => _mstEdges.Select(e => new Edge(e.v, e.w, _g.Weight(e.v, e.w)));
    }
}
