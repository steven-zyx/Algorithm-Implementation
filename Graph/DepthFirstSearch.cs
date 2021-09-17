using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class DepthFirstSearch : UndirectedGraphSearch
    {
        public int CountInComponent { get; protected set; }

        public DepthFirstSearch(Graph g, int s) : base(g, s) { }

        protected override void Search(int v)
        {
            CountInComponent++;
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                    Search(w);
        }
    }
}
