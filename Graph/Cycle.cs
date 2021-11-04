using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class Cycle : UndirectedGraphSearch
    {
        public bool HasCycle { get; protected set; }

        public Cycle(Graph g) : base(g, 0) { }

        public override void Process()
        {
            for (int i = 0; i < _g.V; i++)
                if (!Marked[i])
                    DFS(0, 0);
        }

        protected void DFS(int v, int u)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                    DFS(w, v);
                else if (u != w)
                    HasCycle = true;
        }
    }
}
