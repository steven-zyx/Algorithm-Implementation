using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath_Source_Sink : ShortestPath
    {
        protected int _t;

        public ShortestPath_Source_Sink(EdgeWeightedDigraph g, int s, int t) : base(g, s)
        {
            _t = t;
        }

        protected override void Process(int s)
        {
            Relax(s);
            while (_pq.Size > 0)
            {
                int w = _pq.DelMin();
                if (w == _t)
                    break;
                else
                    Relax(w);
            }
        }
    }
}
