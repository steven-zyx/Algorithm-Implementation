using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SymbolWeightedGraph<T>
    {
        public EdgeWeightedGraph G { get; protected set; }

        public Dictionary<T, int> Index { get; protected set; }

        public Dictionary<int, T> Names { get; protected set; }

        public SymbolWeightedGraph(int v)
        {
            G = new EdgeWeightedGraph(v);
            Index = new Dictionary<T, int>(v);
            Names = new Dictionary<int, T>(v);
        }

        public void AddEdge(T v, T w, double weight)
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
            G.AddEdge(Index[v], Index[w], weight);
        }

    }
}
