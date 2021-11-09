using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class BellmanFord_ParentChecking : BellmanFord
    {
        public BellmanFord_ParentChecking(EdgeWeightedDigraph g, int s) : base(g, s) { }

        protected override void Relax(int v)
        {
            if (_edgeTo[v] != null && _freshV.Contains(_edgeTo[v].From))
                return;
            base.Relax(v);
        }
    }
}
