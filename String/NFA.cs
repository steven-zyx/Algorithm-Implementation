using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmImplementation.Graph;
using BasicDataStrcture;

namespace String
{
    public class NFA
    {
        protected Digraph _g;
        protected DirectedDFS _MSC;
        protected string _regex;

        public NFA(string regex)
        {
            _regex = regex;
            if (_regex[0] != '(' || _regex[_regex.Length - 1] != ')')
                _regex = '(' + _regex + ')';

            int l = _regex.Length;
            _g = new Digraph(l + 1);

            Stack_N<int> op = new Stack_N<int>();
            int leftP = 0;
            for (int i = 0; i < l; i++)
                switch (_regex[i])
                {
                    case '(':
                        op.Push(i);
                        _g.AddEdge(i, i + 1);
                        break;
                    case ')':
                        List<int> pipes = new List<int>();
                        while (_regex[op.Peek()] == '|')
                            pipes.Add(op.Pop());
                        leftP = op.Pop();
                        foreach (int pipe in pipes)
                        {
                            _g.AddEdge(leftP, pipe + 1);
                            _g.AddEdge(pipe, i);
                        }
                        _g.AddEdge(i, i + 1);
                        break;
                    case '|':
                        op.Push(i);
                        break;
                    case '*':
                        _g.AddEdge(i, leftP);
                        _g.AddEdge(leftP, i);
                        _g.AddEdge(i, i + 1);
                        break;
                    default:
                        leftP = i;
                        break;
                }
        }

        public bool Recognize(string text)
        {
            List<int> source = new List<int>(_g.V) { 0 };
            foreach (char c in text)
            {
                _MSC = new DirectedDFS(_g, source);
                source.Clear();
                for (int i = 0; i < _g.V - 1; i++)
                    if (_MSC.Marked[i] && _regex[i] == c)
                        source.Add(i + 1);
                if (source.Count == 0)
                    return false;
            }
            _MSC = new DirectedDFS(_g, source);
            return _MSC.Marked[_g.V - 1];
        }
    }
}
