using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class WeightedDijkstra : ShortestPath4WeightedDigraph
    {
        protected IndexMinPQ<double> _pq;

        public WeightedDijkstra(EdgeWeightedDigraph g, int s) : base(g)
        {
            DistTo[s] = 0;
            _pq = new IndexMinPQ<double>(g.V);
            Process(s);
        }

        protected virtual void Process(int s)
        {
            Relax(s);
            while (_pq.Size > 0)
                Relax(_pq.DelMin());
        }

        protected virtual void Relax(int v)
        {
            foreach (Edge e in _g.Adj(v))
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
