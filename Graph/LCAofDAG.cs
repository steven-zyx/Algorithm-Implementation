using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LCAofDAG
    {
        protected HashSet<int> _LCAs;

        public LCAofDAG(Digraph g, int v1, int v2)
        {
            Func<int, HashSet<int>> fGetAncesters = v =>
             {
                 Topological client = new Topological(g);
                 HashSet<int> ancesters = new HashSet<int>();
                 foreach (int vertex in client.Order())
                     if (vertex != v)
                         ancesters.Add(vertex);
                     else
                         break;
                 return ancesters;
             };

            Digraph reverseG = g.Reverse();
            DirectedDFS dfs1 = new DirectedDFS(reverseG, v1);
            DirectedDFS dfs2 = new DirectedDFS(reverseG, v2);

            int[] commonAncesters = fGetAncesters(v1).Intersect(fGetAncesters(v2)).Where(x => dfs1.Marked[x] && dfs2.Marked[x]).ToArray();
            int maxHeigth1 = 0, maxHeight2 = 0;
            LongestPath_DAG_Unweighted client1 = new LongestPath_DAG_Unweighted(g, v1);
            LongestPath_DAG_Unweighted client2 = new LongestPath_DAG_Unweighted(g, v2);
            foreach (int v in commonAncesters)
            {
                maxHeigth1 = Math.Max(maxHeigth1, client1.Height[v]);
                maxHeight2 = Math.Max(maxHeight2, client2.Height[v]);
            }

            _LCAs = new HashSet<int>();
            foreach (int v in commonAncesters)
            {
                if (client1.Height[v] == maxHeigth1)
                    _LCAs.Add(v);
                if (client2.Height[v] == maxHeight2)
                    _LCAs.Add(v);
            }
        }

        public IEnumerable<int> LCAs() => _LCAs;
    }
}
