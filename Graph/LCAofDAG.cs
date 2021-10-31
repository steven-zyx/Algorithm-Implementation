using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LCAofDAG
    {
        public int LCA { get; }

        public LCAofDAG(Digraph g, int v1, int v2)
        {
            LongestPath_DAG_Unweighted longClient = new LongestPath_DAG_Unweighted(g, v1);
            int[] lp1 = longClient.Path.ToArray();
            longClient = new LongestPath_DAG_Unweighted(g, v2);
            int[] lp2 = longClient.Path.ToArray();

            if (lp1[0] != lp2[0])
            {
                LCA = -1;
                return;
            }

            for (int i = 1; i < g.V; i++)
                if (lp1[i] != lp2[i])
                {
                    LCA = lp1[i - 1];
                    return;
                }
        }
    }
}
