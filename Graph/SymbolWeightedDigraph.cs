using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SymbolWeightedDigraph<T> : SymbolWeightedGraph<T>
    {
        public new EdgeWeightedDigraph G => base.G as EdgeWeightedDigraph;

        public SymbolWeightedDigraph(int v) : base(v)
        {
            base.G = new EdgeWeightedDigraph(v);
        }
    }
}
