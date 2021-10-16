using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class DirectedDFSEdgeTo
    {
        public bool[] Marked { get; protected set; }

        protected Graph _g;

        protected int[] _edgeTo;

        public DirectedDFSEdgeTo(Digraph g, IEnumerable<int> sources)
        {
            _g = g;
            Marked = new bool[g.V];
            _edgeTo = new int[g.V];
            for (int i = 0; i < g.V; i++)
                _edgeTo[i] = -1;

            foreach (int v in sources)
                if (!Marked[v])
                    DFS(v);
        }

        protected void DFS(int v)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                {
                    _edgeTo[w] = v;
                    DFS(w);
                }
        }

        public IEnumerable<int> PathTo(int w)
        {
            Stack<int> path = new Stack<int>();
            for (; w != -1; w = _edgeTo[w])
                path.Push(w);
            return path;
        }
    }
}
