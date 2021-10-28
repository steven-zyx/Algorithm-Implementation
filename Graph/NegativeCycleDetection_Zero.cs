using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class NegativeCycleDetection_Zero : NegativeCycleDetectionBase
    {
        public NegativeCycleDetection_Zero(EdgeWeightedDigraph g)
        {
            Initialize(g);
            for (int v = 0; v < g.V && !HasCycle; v++)
                DFS(v);
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
