using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class TwoColor : UndirectedGraphSearch
    {
        protected bool[] _color;
        public bool IsBipartite { get; protected set; }

        public TwoColor(Graph g) : base(g, 0)
        {
            _color = new bool[_g.V];
            IsBipartite = true;
        }

        public override void Process()
        {
            for (int i = 0; i < _g.V; i++)
                if (!Marked[i])
                    Search(i);
        }

        protected override void Search(int v)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                {
                    _color[w] = !_color[v];
                    Search(w);
                }
                else if (_color[w] == _color[v])
                    IsBipartite = false;
        }
    }
}
