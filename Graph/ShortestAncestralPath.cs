using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestAncestralPath
    {
        protected List<List<int>> _shortestAncestralPaths;

        public ShortestAncestralPath(Digraph g, int v1, int v2)
        {
            Topological client = new Topological(g, v1);
            HashSet<int> successor1 = client.Order().ToHashSet();
            client = new Topological(g, v2);
            IEnumerable<int> allSuccessors = successor1.Intersect(client.Order());

            BreadthFirstSearchHeight bfs1 = new BreadthFirstSearchHeight(g, v1);
            BreadthFirstSearchHeight bfs2 = new BreadthFirstSearchHeight(g, v2);
            int[] commonSuccessors = allSuccessors.Where(x => bfs1.Height[x] > 0 && bfs2.Height[x] > 0).ToArray();

            int minHeight = int.MaxValue;
            foreach (int v in commonSuccessors)
                minHeight = Math.Min(minHeight, bfs1.Height[v] + bfs2.Height[v]);
            List<int> ancesters = new List<int>();
            foreach (int v in commonSuccessors)
                if (bfs1.Height[v] + bfs2.Height[v] == minHeight)
                    ancesters.Add(v);

            _shortestAncestralPaths = new List<List<int>>(ancesters.Count);
            foreach (int ancester in ancesters)
            {
                List<int> path = new List<int>(minHeight + 1);
                path.AddRange(bfs1.PathTo(ancester));
                path.RemoveAt(path.Count - 1);
                path.AddRange(bfs2.PathTo(ancester).Reverse());
                _shortestAncestralPaths.Add(path);
            }
        }

        public IEnumerable<List<int>> ShortestAncestralPaths() => _shortestAncestralPaths;
    }
}
