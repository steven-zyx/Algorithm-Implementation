using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class WeightedDijkstra_Matrix : IShortestPath4WeightedDigraph
    {
        public double[] DistTo { get; protected set; }
        protected bool[] _marked;
        protected int[] _edgeTo;
        protected readonly int _s;
        protected readonly EdgeWeightedDigraph_Matrix _g;


        public WeightedDijkstra_Matrix(EdgeWeightedDigraph_Matrix g, int s)
        {
            DistTo = new double[g.V];
            for (int i = 0; i < g.V; i++)
                DistTo[i] = double.PositiveInfinity;
            DistTo[s] = 0;

            _edgeTo = new int[g.V];
            _marked = new bool[g.V];
            _s = s;
            _g = g;

            int next = _s;
            for (int i = 0; i < _g.V - 1; i++)
                next = Relax(next);
        }

        protected int Relax(int v)
        {
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
            {
                double weight = _g.Weight(v, w);
                if (DistTo[w] > DistTo[v] + weight)
                {
                    DistTo[w] = DistTo[v] + weight;
                    _edgeTo[w] = v;
                }
            }

            int next = -1;
            double minDist = double.PositiveInfinity;
            for (int i = 0; i < _g.V; i++)
                if (!_marked[i] && DistTo[i] < minDist)
                {
                    next = i;
                    minDist = DistTo[i];
                }
            return next;
        }

        public bool HashPathTo(int v) => _marked[v];

        public IEnumerable<Edge> PathTo(int v)
        {
            Stack<Edge> path = new Stack<Edge>();
            for (; v != _s; v = _edgeTo[v])
            {
                int f = _edgeTo[v];
                path.Push(new Edge(f, v, _g.Weight(f, v)));
            }
            return path;
        }
    }
}
