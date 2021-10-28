using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class NegetiveCycleDetection
    {
        protected EdgeWeightedDigraph _g;
        protected bool[] _onStack;
        protected DirectedEdge[] _edgeTo;
        protected double[] _distTo;

        public Stack<int> Cycle { get; protected set; }

        public bool HasCycle => Cycle != null;

        public NegetiveCycleDetection(EdgeWeightedDigraph g)
        {
            _g = g;
            _onStack = new bool[g.V];
            _edgeTo = new DirectedEdge[g.V];
            _distTo = new double[g.V];
            for (int i = 0; i < g.V; i++)
                _distTo[i] = double.PositiveInfinity;

            for (int v = 0; v < g.V; v++)
                if (_distTo[v] == double.PositiveInfinity)
                {
                    _distTo[v] = 0;
                    DFS(v);
                }
        }

        protected void DFS(int v)
        {
            _onStack[v] = true;
            foreach (DirectedEdge e in _g.Adj(v))
            {
                int w = e.To;
                if (HasCycle)
                    return;
                if (_distTo[w] > _distTo[v] + e.Weight)
                    if (_onStack[w])
                    {
                        Cycle = new Stack<int>();
                        Cycle.Push(w);
                        for (int i = v; i != w; i = _edgeTo[i].From)
                            Cycle.Push(i);
                        Cycle.Push(w);
                    }
                    else
                    {
                        _distTo[w] = _distTo[v] + e.Weight;
                        _edgeTo[w] = e;
                        DFS(w);
                    }
            }
            _onStack[v] = false;
        }

    }
}
