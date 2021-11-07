using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class WeightedDijkstra_AllPairs
    {
        protected EdgeWeightedDigraph _g;
        protected WeightedDijkstra[] _clients;

        public WeightedDijkstra_AllPairs(EdgeWeightedDigraph g)
        {
            _g = g;
            _clients = new WeightedDijkstra[g.V];
            for (int i = 0; i < _g.V; i++)
                _clients[i] = new WeightedDijkstra(g, i);
        }

        public IEnumerable<Edge> Path(int s, int t) => _clients[s].PathTo(t);

        public double Dist(int s, int t) => _clients[s].DistTo[t];
    }
}
