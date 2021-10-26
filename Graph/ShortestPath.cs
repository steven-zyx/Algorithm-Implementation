using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath
    {
        public double[] DistTo { get; protected set; }
        protected DirectedEdge[] _edgeTo { get; set; }

        protected IndexMinPQ<double> _pq;

        protected EdgeWeightedDigraph _g;


        public ShortestPath(EdgeWeightedDigraph g, int s)
        {
            DistTo = new double[g.V];
            for (int i = 0; i < g.V; i++)
                DistTo[i] = double.PositiveInfinity;
            DistTo[s] = 0;

            _edgeTo = new DirectedEdge[g.V];
            _pq = new IndexMinPQ<double>(g.V);
            _g = g;

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

        public bool HasPathTo(int v) => DistTo[v] != double.PositiveInfinity;

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            Stack<DirectedEdge> route = new Stack<DirectedEdge>();
            for (; _edgeTo[v] != null; v = _edgeTo[v].From)
                route.Push(_edgeTo[v]);
            return route;
        }
    }
}
