using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgorithmImplementation.Graph
{
    public class ParallelEdgeDetection : UndirectedGraphSearch
    {
        public int ParallelEdge { get; protected set; }

        public ParallelEdgeDetection(Graph g) : base(g, 0)
        {
            ParallelEdge = 0;
        }

        public override void Process()
        {
            for (int i = 0; i < _g.V; i++)
                ParallelEdge += _g.Adj(i).Count() - _g.Adj(i).Distinct().Count();
            ParallelEdge /= 2;
        }
    }
}
