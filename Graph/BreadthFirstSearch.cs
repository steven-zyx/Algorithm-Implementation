using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class BreadthFirstSearch : FindingPaths
    {

        public BreadthFirstSearch(Graph g, int s) : base(g, s) { }

        protected override void Search(int s)
        {
            Marked[s] = true;
            Queue<int> vertices = new Queue<int>();
            vertices.Enqueue(s);
            while (vertices.Count > 0)
            {
                int v = vertices.Dequeue();
                foreach (int w in _g.Adj(v))
                    if (!Marked[w])
                    {
                        Marked[w] = true;
                        _edgeTo[w] = v;
                        vertices.Enqueue(w);
                    }
            }
        }
    }
}
