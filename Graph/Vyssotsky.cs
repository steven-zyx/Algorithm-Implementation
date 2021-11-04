using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class Vyssotsky : IMST
    {
        protected EdgeWeightedGraph _g;
        protected EdgeWeightedGraph _putitiveG;

        public Vyssotsky(EdgeWeightedGraph g)
        {
            _g = g;
            CycleDetection_WeightedGraph client;
            _putitiveG = new EdgeWeightedGraph(g.V);
            foreach (Edge e in g.Edges())
            {
                _putitiveG.AddEdge(e);
                client = new CycleDetection_WeightedGraph(_putitiveG);
                if (client.HasCycle)
                {
                    Edge maxE = new Edge(-1, -1, 0);
                    foreach (Edge cycleE in client.Cycle)
                        if (cycleE.Weight > maxE.Weight)
                            maxE = cycleE;
                    _putitiveG.RemoveEdge(maxE);
                }
            }
        }

        public IEnumerable<Edge> Edges() => _putitiveG.Edges();

        public double Weight => Edges().Sum(x => x.Weight);
    }
}
