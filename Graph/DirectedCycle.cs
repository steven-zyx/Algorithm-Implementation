using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class DirectedCycle
    {
        protected Digraph _g;
        protected bool[] _marked;
        protected bool[] _onStack;
        protected int[] _edgeTo;
        public Stack_N<int> Cycle { get; protected set; }

        public DirectedCycle(Digraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _onStack = new bool[g.V];
            _edgeTo = new int[g.V];
            for (int v = 0; v < g.V; v++)
                if (!_marked[v])
                    DFS(v);
        }

        protected void DFS(int v)
        {
            _marked[v] = true;
            _onStack[v] = true;
            foreach (int w in _g.Adj(v))
                if (HasCycle)
                    return;
                else if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    DFS(w);
                }
                else if (_onStack[w])
                {
                    Cycle = new Stack_N<int>();
                    Cycle.Push(w);
                    for (int i = v; i != w; i = _edgeTo[i])
                        Cycle.Push(i);
                    Cycle.Push(w);
                }
            _onStack[v] = false;
        }

        public bool HasCycle => Cycle != null;
    }
}
