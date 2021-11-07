using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public abstract class ShortestPath4WeightedDigraph : IShortestPath4WeightedDigraph
    {
        public double[] DistTo { get; protected set; }

        protected Edge[] _edgeTo;

        protected EdgeWeightedDigraph _g;

        public ShortestPath4WeightedDigraph(EdgeWeightedDigraph g)
        {
            Init(g);
        }

        protected void Init(EdgeWeightedDigraph g)
        {
            _g = g;
            DistTo = new double[g.V];
            for (int i = 0; i < g.V; i++)
                DistTo[i] = double.PositiveInfinity;
            _edgeTo = new Edge[g.V];
        }

        public bool HashPathTo(int v) => DistTo[v] != double.PositiveInfinity;

        public IEnumerable<Edge> PathTo(int v)
        {
            Stack<Edge> route = new Stack<Edge>();
            for (; _edgeTo[v] != null; v = _edgeTo[v].From)
                route.Push(_edgeTo[v]);
            return route;
        }
    }
}
