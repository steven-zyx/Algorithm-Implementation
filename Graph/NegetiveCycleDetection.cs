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
        protected bool[] _marked;
        protected bool[] _onStack;
        protected DirectedEdge[] _edgeTo;
        protected double[] _distTo;

        public Stack<int> Cycle { get; protected set; }

        public bool HasCycle => Cycle != null;

        public NegetiveCycleDetection(EdgeWeightedDigraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _onStack = new bool[g.V];
            _edgeTo = new DirectedEdge[g.V];
            _distTo = new double[g.V];

            for (int v = 0; v < g.V; v++)
                if (!_marked[v])
                    DFS(v);
        }

        protected void DFS(int v)
        {
            _marked[v] = true;
            _onStack[v] = true;
            foreach (DirectedEdge e in _g.Adj(v))
            {
                int w = e.To;
                if (HasCycle)
                    return;
                else if (!_marked[w])
                {
                    _edgeTo[w] = e;
                    _distTo[w] = _distTo[v] + e.Weight;
                    DFS(w);
                }
                else if (_onStack[w] && _distTo[v] + e.Weight <= _distTo[w])
                {
                    Cycle = new Stack<int>();
                    Cycle.Push(w);
                    for (int i = v; i != w; i = _edgeTo[i].From)
                        Cycle.Push(i);
                    Cycle.Push(w);
                }
            }
            _onStack[v] = false;
        }

    }
}
