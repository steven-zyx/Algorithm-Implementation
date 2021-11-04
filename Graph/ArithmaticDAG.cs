using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ArithmaticDAG
    {
        protected Digraph _g;
        protected string[] _values;
        protected readonly HashSet<string> OPERATOR;

        public double Result { get; }

        public ArithmaticDAG(Digraph g, string[] values)
        {
            OPERATOR = new HashSet<string>() { "+", "-", "/", "*" };
            _g = g;
            _values = values;
            Result = DFS(0);
        }

        public double DFS(int v)
        {
            if (!OPERATOR.Contains(_values[v]))
                return double.Parse(_values[v]);

            string op = _values[v];
            double result = double.PositiveInfinity;
            foreach (int w in _g.Adj(v))
            {
                if (result.Equals(double.PositiveInfinity))
                    result = DFS(w);
                else if (op == "+")
                    result += DFS(w);
                else if (op == "-")
                    result -= DFS(w);
                else if (op == "*")
                    result *= DFS(w);
                else if (op == "/")
                    result /= DFS(w);
                else
                    throw new InvalidOperationException();
            }
            return result;
        }
    }
}
