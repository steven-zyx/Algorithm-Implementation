using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class DepthFirstSearch
    {
        protected Graph _g;
        protected bool[] _marked;
        protected int _count;
        protected int[] _edgeTo;
        protected int _s;

        public DepthFirstSearch(Graph g, int s)
        {
            _g = g;
            _marked = new bool[_g.V];
            _count = 0;
            _edgeTo = new int[_g.V];
            _s = s;
            DFS(s);
        }

        protected void DFS(int v)
        {
            _marked[v] = true;
            _count++;
            foreach (int w in _g.Adj(v))
                if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    DFS(w);
                }
        }

        public bool Marked(int v) => _marked[v];

        public int Count() => _count;

        public IEnumerable<int> PathTo(int v)
        {
            Stack<int> route = new Stack<int>();
            for (; v != _s; v = _edgeTo[v])
                route.Push(v);
            route.Push(_s);
            return route;
        }
    }
}
