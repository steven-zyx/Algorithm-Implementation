using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class EagerPrim
    {
        protected EdgeWeightedGraph _g;
        //protected IndexMinPQ

        public double Weight { get; protected set; }

        public EagerPrim(EdgeWeightedGraph g)
        {
            _g = g;
        }


        //public IEnumerable<Edge> Edges() => ;
    }
}
