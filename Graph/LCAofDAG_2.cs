using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LCAofDAG_2
    {
        public int LCA { get; }

        public LCAofDAG_2(Digraph g, int v1, int v2)
        {
            Digraph reverseG = g.Reverse();
            int[] tp1 = (new Topological(reverseG, v1)).Order().ToArray();
            int[] tp2 = (new Topological(reverseG, v2)).Order().ToArray();

            int n1 = tp1.Length - 1, n2 = tp2.Length - 1;
            if (tp1[n1] != tp2[n2])
            {
                LCA = -1;
                return;
            }

            for (; n1 >= 0 && n2 >= 0; n1--, n2--)
                if (tp1[n1] != tp2[n2])
                {
                    LCA = tp1[n1 + 1];
                    return;
                }

            LCA = -1;
        }
    }
}
