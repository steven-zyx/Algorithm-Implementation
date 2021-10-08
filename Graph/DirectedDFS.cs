using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class DirectedDFS
    {
        public bool[] Marked { get; protected set; }

        protected Graph _g;

        public DirectedDFS(Graph g, IEnumerable<int> sources)
        {
            _g = g;
            Marked = new bool[g.V];
            foreach (int v in sources)
                if (!Marked[v])
                    DFS(v);
        }

        protected void DFS(int v)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                    DFS(w);
        }
    }
}
