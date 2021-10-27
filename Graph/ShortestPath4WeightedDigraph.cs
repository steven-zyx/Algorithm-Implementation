using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public abstract class ShortestPath4WeightedDigraph
    {
        public double[] DistTo;

        protected DirectedEdge[] _edgeTo;

        protected EdgeWeightedDigraph _g;

        public ShortestPath4WeightedDigraph(EdgeWeightedDigraph g, int s)
        {
            _g = g;

            DistTo = new double[g.V];
            for (int i = 0; i < g.V; i++)
                DistTo[i] = double.PositiveInfinity;
            DistTo[s] = 0;

            _edgeTo = new DirectedEdge[g.V];
        }

        public bool HashPathTo(int v) => DistTo[v] != double.PositiveInfinity;

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            Stack<DirectedEdge> route = new Stack<DirectedEdge>();
            for (; _edgeTo[v] != null; v = _edgeTo[v].From)
                route.Push(_edgeTo[v]);
            return route;
        }
    }
}
