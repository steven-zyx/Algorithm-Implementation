using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class EagerPrim_DenseGraph
    {
        protected EdgeWeightedGraph _g;
        protected bool[] _marked;
        protected double[] _distTo;
        protected Edge[] _edgeTo;
        protected Queue<Edge> _minEdges;

        public EagerPrim_DenseGraph(EdgeWeightedGraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _distTo = new double[g.V];
            for (int v = 0; v < g.V; v++)
                _distTo[v] = double.PositiveInfinity;
            _edgeTo = new Edge[g.V];
            _minEdges = new Queue<Edge>();

        }

        protected int Visit(int v)
        {
            _marked[v] = true;
            foreach (Edge e in _g.Adj(v))
            {
                int w = e.Other(v);
                if (_marked[w])
                    continue;
                if (_distTo[w] > e.Weight)
                {
                    _distTo[w] = e.Weight;
                    _edgeTo[w] = e;
                }
            }

            int next = 0;
            for (int i = 0; i < _g.V; i++)
                if (!_marked[i] && _distTo[i] < _distTo[next])
                    next = i;
            return next;
        }
    }
}
