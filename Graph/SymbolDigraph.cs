using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SymbolDigraph<T> : SymbolGraph2<T>
    {
        public new Digraph G => base.G as Digraph;

        public SymbolDigraph(int v) : base(v)
        {
            base.G = new Digraph(v);
        }
    }
}
