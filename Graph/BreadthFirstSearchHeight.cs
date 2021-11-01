using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class BreadthFirstSearchHeight : FindingPaths
    {
        public int[] Height { get; protected set; }

        public BreadthFirstSearchHeight(Graph g, int s) : base(g, s)
        {
            Height = new int[g.V];
            for (int i = 0; i < g.V; i++)
                Height[i] = -1;
            Height[s] = 0;
            Search(s);
        }

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
                        Height[w] = Height[v] + 1;
                        vertices.Enqueue(w);
                    }
            }
        }
    }
}
