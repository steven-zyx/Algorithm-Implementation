using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class CycleDetection_WeightedGraph
    {
        protected EdgeWeightedGraph _g;
        protected Stack<Edge> _cycle;
        protected bool[] _marked;
        protected Edge[] _edgeTo;

        public CycleDetection_WeightedGraph(EdgeWeightedGraph g)
        {
            _g = g;
            _marked = new bool[g.V];
            _edgeTo = new Edge[g.V];
            DFS(0, 0);
        }

        protected void DFS(int u, int v)
        {
            _marked[v] = true;
            foreach (Edge e in _g.Adj(v))
            {
                if (HasCycle)
                    return;

                int w = e.Other(v);
                if (!_marked[w])
                {
                    _edgeTo[w] = e;
                    DFS(v, w);
                }
                else if (u != w)
                {
                    _cycle = new Stack<Edge>();
                    _cycle.Push(e);
                    for (int x = v; x != w; x = _edgeTo[x].Other(x))
                        _cycle.Push(_edgeTo[x]);
                }
            }
        }

        public bool HasCycle => _cycle != null;

        public IEnumerable<Edge> Cycle => _cycle;
    }
}
