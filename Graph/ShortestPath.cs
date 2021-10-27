using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath : ShortestPath4WeightedDigraph
    {
        protected IndexMinPQ<double> _pq;

        public ShortestPath(EdgeWeightedDigraph g, int s) : base(g, s)
        {
            _pq = new IndexMinPQ<double>(g.V);
            Process(s);
        }

        protected virtual void Process(int s)
        {
            Relax(s);
            while (_pq.Size > 0)
                Relax(_pq.DelMin());
        }

        protected void Relax(int v)
        {
            foreach (DirectedEdge e in _g.Adj(v))
            {
                int w = e.To;
                if (DistTo[w] > DistTo[v] + e.Weight)
                {
                    DistTo[w] = DistTo[v] + e.Weight;
                    _edgeTo[w] = e;

                    if (_pq.Contains(w))
                        _pq.Change(w, DistTo[w]);
                    else
                        _pq.Insert(w, DistTo[w]);
                }
            }
        }
    }
}
