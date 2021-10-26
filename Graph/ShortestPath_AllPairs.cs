using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath_AllPairs
    {
        protected EdgeWeightedDigraph _g;
        protected ShortestPath[] _clients;

        public ShortestPath_AllPairs(EdgeWeightedDigraph g)
        {
            _g = g;
            _clients = new ShortestPath[g.V];
            for (int i = 0; i < _g.V; i++)
                _clients[i] = new ShortestPath(g, i);
        }

        public IEnumerable<DirectedEdge> Path(int s, int t) => _clients[s].PathTo(t);

        public double Dist(int s, int t) => _clients[s].DistTo[t];
    }
}
