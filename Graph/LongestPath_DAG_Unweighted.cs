using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LongestPath_DAG_Unweighted
    {
        protected Digraph _g;
        protected Topological _client;
        protected int[] _height;
        protected Stack<int> _path;

        public LongestPath_DAG_Unweighted(Digraph g, int t) : this(g, -1, t) { }

        public LongestPath_DAG_Unweighted(Digraph g, int s, int t)
        {
            _g = g;
            _height = new int[_g.V];
            _path = new Stack<int>();
            _client = new Topological(_g);

            int[] order = _client.Order().ToArray();
            if (s == -1)
                s = order[0];
            foreach (int v in order)
                foreach (int w in g.Adj(v))
                    _height[w] = Math.Max(_height[w], _height[v] + 1);

            int index = _g.V - 1;
            for (; index >= 0; index--)
                if (order[index] == t)
                    break;

            int currentH = _height[t];
            for (; index >= 0; index--)
                if (_height[order[index]] == currentH)
                {
                    _path.Push(order[index]);
                    currentH--;

                    if (order[index] == s)
                        break;
                }
        }

        public IEnumerable<int> Path => _path;
    }
}
