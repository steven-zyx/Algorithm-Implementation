using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SymbolDigraph<T> : SymbolGraph2<T>
    {
        public Digraph DiG => G as Digraph;

        public SymbolDigraph(int v) : base(v)
        {
            G = new Digraph(v);
        }
    }
}
