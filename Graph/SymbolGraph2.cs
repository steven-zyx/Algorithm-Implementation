using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SymbolGraph2<T>
    {
        public Graph G { get; protected set; }

        public Dictionary<T, int> Index { get; protected set; }

        public Dictionary<int, T> Names { get; protected set; }

        public SymbolGraph2(int v)
        {
            G = new Graph(v);
            Index = new Dictionary<T, int>(v);
            Names = new Dictionary<int, T>(v);
        }

        public void AddEdge(T v, T w)
        {
            if (!Index.ContainsKey(v))
            {
                Index[v] = Index.Count;
                Names[Names.Count] = v;
            }
            if (!Index.ContainsKey(w))
            {
                Index[w] = Index.Count;
                Names[Names.Count] = w;
            }
            G.AddEdge(Index[v], Index[w]);
        }
    }
}
