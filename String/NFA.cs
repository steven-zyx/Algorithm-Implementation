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
        protected readonly (int, int)[] _charRange = { (48, 57), (65, 90), (97, 122) };

        public NFA(string regex)
        {
            _regex = regex;

            //Handle specified set: [abc]
            List<char> characters = new List<char>();
            for (int i = 0; i < _regex.Length; i++)
            {
                if (_regex[i] == '[')
                {
                    characters.Add('(');
                    i++;

                    bool complement = false;
                    if (_regex[i] == '^')
                    {
                        complement = true;
                        i++;
                    }

                    bool[] set = new bool[256];
                    while (_regex[i] != ']')
                    {
                        if (_regex[i] == '-')
                            for (int n = _regex[i - 1] + 1; n < _regex[i + 1]; n++)
                                set[n] = true;
                        else
                            set[_regex[i]] = true;
                        i++;
                    }

                    if (complement)
                        foreach (var range in _charRange)
                            for (int n = range.Item1; n <= range.Item2; n++)
                                set[n] = !set[n];
                    foreach (var range in _charRange)
                        for (int n = range.Item1; n <= range.Item2; n++)
                        {
                            if (set[n])
                            {
                                characters.Add((char)n);
                                characters.Add('|');
                            }
                        }
                    characters.RemoveAt(characters.Count - 1);

                    characters.Add(')');
                    continue;
                }
                characters.Add(_regex[i]);
            }
            _regex = new string(characters.ToArray());

            //Handle multiple times: {3},{3,5},{3,}
            characters.Clear();
            int leftP = 0;
            Stack<int> leftBrackets = new Stack<int>();
            for (int i = 0; i < _regex.Length; i++)
            {
                if (_regex[i] == '(')
                    leftBrackets.Push(i);
                else if (_regex[i] == ')')
                    leftP = leftBrackets.Pop();
                else if (_regex[i] == '{')
                {
                    int start = i - 1;
                    if (_regex[i - 1] == ')')
                        start = leftP;
                    int end = i - 1;

                    i++;
                    int nextPosition = _regex.IndexOfAny(new char[] { ',', '}' }, i);
                    int dupRepeat = int.Parse(_regex.Substring(i, nextPosition - i));
                    for (int n = 1; n < dupRepeat; n++)
                        for (int j = start; j <= end; j++)
                            characters.Add(_regex[j]);

                    i = nextPosition;
                    if (_regex[i] != '}')
                    {
                        i++;
                        if (_regex[i] == '}')
                        {
                            for (int j = start; j <= end; j++)
                                characters.Add(_regex[j]);
                            characters.Add('*');
                        }
                        else
                        {
                            nextPosition = _regex.IndexOf('}', i);
                            int defRepeat = int.Parse(_regex.Substring(i, nextPosition - i));
                            for (int n = 0; n < defRepeat - dupRepeat; n++)
                            {
                                for (int j = start; j <= end; j++)
                                    characters.Add(_regex[j]);
                                characters.Add('?');
                            }
                            i = nextPosition;
                        }
                    }
                    continue;
                }
                characters.Add(_regex[i]);
            }
            _regex = new string(characters.ToArray());

            if (_regex[0] != '(' || _regex[_regex.Length - 1] != ')')
                _regex = '(' + _regex + ')';

            int l = _regex.Length;
            _g = new Digraph(l + 1);

            Stack_N<int> op = new Stack_N<int>();
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
                    case '+':
                        _g.AddEdge(i, leftP);
                        _g.AddEdge(i, i + 1);
                        break;
                    case '?':
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
                    if (_MSC.Marked[i] && (_regex[i] == c || _regex[i] == '.'))
                        source.Add(i + 1);
                if (source.Count == 0)
                    return false;
            }
            _MSC = new DirectedDFS(_g, source);
            return _MSC.Marked[_g.V - 1];
        }
    }
}
