using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class MultisourceShortestPath : ShortestPath4WeightedDigraph
    {
        protected IndexMinPQ<double> _pq;

        public MultisourceShortestPath(EdgeWeightedDigraph g, IEnumerable<int> sources) : base(g)
        {
            _pq = new IndexMinPQ<double>(g.V);
            foreach (int source in sources)
            {
                DistTo[source] = 0;
                _pq.Insert(source, 0);
            }

            do
                Relax(_pq.DelMin());
            while (_pq.Size > 0);
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
