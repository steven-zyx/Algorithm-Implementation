using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath_DAG
    {
        protected EdgeWeightedDigraph _g;
        public double[] DistTo { get; protected set; }

        public DirectedEdge[] _edgeTo;

        public bool IsDAG { get; }

        public ShortestPath_DAG(EdgeWeightedDigraph g, int s)
        {
            _g = g;
            Topological client = new Topological(_g.Convert());
            IsDAG = client.IsDAG();

            if (IsDAG)
            {
                DistTo = new double[_g.V];
                for (int i = 0; i < _g.V; i++)
                    DistTo[i] = double.PositiveInfinity;
                DistTo[s] = 0;
                _edgeTo = new DirectedEdge[_g.V];

                int[] order = client.Order().ToArray();
                int start = 0;
                for (; start < _g.V; start++)
                    if (order[start] == s)
                        break;
                for (; start < _g.V; start++)
                    Relax(order[start]);
            }
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
                }
            }
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
