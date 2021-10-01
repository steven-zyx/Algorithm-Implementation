using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Digraph : Graph
    {
        public Digraph(int v) : base(v) { }

        public Digraph(int[] data) : base(data) { }

        public override void AddEdge(int v, int w)
        {
            _adjacencyList[v].Add(w);
            E++;
        }

        public Digraph Reverse()
        {
            Digraph g = new Digraph(V);
            for (int i = 0; i < V; i++)
                foreach (int w in Adj(i))
                    g.AddEdge(w, i);
            return g;
        }
    }
}
