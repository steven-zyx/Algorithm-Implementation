using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Neighbors : WeightedDijkstra
    {
        protected double _distance;

        public Neighbors(EdgeWeightedDigraph g, int s, double distance) : base(g, s)
        {
            _distance = distance;
        }

        protected override void Relax(int v)
        {
            foreach (Edge e in _g.Adj(v))
            {
                int w = e.To;
                double weight = DistTo[v] + e.Weight;
                if (weight < DistTo[w] && weight <= _distance)
                {
                    DistTo[w] = weight;
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
