using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class UndirectedGraphSearch
    {
        protected Graph _g;
        public bool[] Marked { get; protected set; }
        protected int _s;

        public UndirectedGraphSearch(Graph g, int s)
        {
            _g = g;
            Marked = new bool[_g.V];
            _s = s;
        }

        public virtual void Process() => Search(_s);

        protected virtual void Search(int v)
        {
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                    Search(w);
        }
    }
}
