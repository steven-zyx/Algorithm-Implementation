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

        public NFA(string regex)
        {
            if (regex[0] != '(' || regex[regex.Length - 1] != ')')
                regex = '(' + regex + ')';

            int l = regex.Length;
            _g = new Digraph(l + 1);

            Stack_N<int> op = new Stack_N<int>();
            int leftP = 0;
            for (int i = 0; i < l; i++)
            {
                switch (regex[i])
                {
                    case '(':
                        op.Push(i);
                        break;
                    case ')':
                        if (op.Peek() == '|')
                            _g.AddEdge(op.Pop(), i);
                        leftP = op.Pop();
                        break;
                    case '|':
                        _g.AddEdge(op.Peek(), i + 1);
                        op.Push(i);
                        break;
                    case '*':
                        _g.AddEdge(i, leftP);
                        break;
                    default:
                        leftP = i;
                        break;
                }
                if (regex[i] != '|')
                    _g.AddEdge(i, i + 1);
            }
        }

        public bool Recognize(string text)
        {
            throw new NotImplementedException();
        }
    }
}
