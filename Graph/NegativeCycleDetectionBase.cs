using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public abstract class NegativeCycleDetectionBase
    {
        protected bool[] _onStack;
        protected DirectedEdge[] _edgeTo;
        protected double[] _distTo;
        protected EdgeWeightedDigraph _g;

        public Stack<int> Cycle { get; protected set; }

        public bool HasCycle => Cycle != null;

        protected void Initialize(EdgeWeightedDigraph g)
        {
            _g = g;
            _onStack = new bool[_g.V];
            _edgeTo = new DirectedEdge[_g.V];
            _distTo = new double[_g.V];
        }
    }
}
