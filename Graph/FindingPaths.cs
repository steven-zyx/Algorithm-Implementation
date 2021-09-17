using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class FindingPaths : UndirectedGraphSearch
    {
        protected int[] _edgeTo;

        public FindingPaths(Graph g, int s) : base(g, s)
        {
            _edgeTo = new int[_g.V];
        }

        protected override void Search(int v)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                {
                    _edgeTo[w] = v;
                    Search(w);
                }
        }

        public IEnumerable<int> PathTo(int v)
        {
            if (!Marked[v])
                return null;

            Stack<int> route = new Stack<int>();
            for (; v != _s; v = _edgeTo[v])
                route.Push(v);
            route.Push(_s);
            return route;
        }
    }
}
