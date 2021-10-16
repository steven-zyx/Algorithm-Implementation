using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmImplementation.Graph;

namespace String
{
    public class NFAProof : NFA
    {
        protected List<List<int>>[] _stepPathsList;
        protected DirectedDFSEdgeTo _MSCE;

        public NFAProof(string regex) : base(regex) { }

        public override bool Recognize(string text)
        {
            _stepPathsList = new List<List<int>>[text.Length + 1];
            for (int i = 0; i <= text.Length; i++)
                _stepPathsList[i] = new List<List<int>>();

            List<int> source = new List<int>(_g.V) { 0 };
            for (int n = 0; n < text.Length; n++)
            {
                _MSCE = new DirectedDFSEdgeTo(_g, source);
                source.Clear();
                for (int i = 0; i < _g.V - 1; i++)
                    if (_MSCE.Marked[i] && (_regex[i] == text[n] || _regex[i] == '.'))
                    {
                        _stepPathsList[n].Add(_MSCE.PathTo(i).ToList());
                        source.Add(i + 1);
                    }
                if (source.Count == 0)
                    return false;
            }
            _MSCE = new DirectedDFSEdgeTo(_g, source);
            _stepPathsList[text.Length].Add(_MSCE.PathTo(_g.V - 1).ToList());
            return _MSCE.Marked[_g.V - 1];
        }

        public IEnumerable<int> ShowProof(string text)
        {
            if (!Recognize(text))
                return null;

            Stack<int> path = new Stack<int>();
            int pStart = _g.V - 1;
            for (int i = text.Length; i >= 0; i--)
                foreach (List<int> paths in _stepPathsList[i])
                    if (paths[paths.Count - 1] == pStart)
                    {
                        for (int j = paths.Count - 1; j >= 0; j--)
                            path.Push(paths[j]);
                        pStart = paths[0] - 1;
                    }
            return path;
        }

        public IEnumerable<char> ShowProofCharacters(string text)
        {
            List<int> stages = ShowProof(text).ToList();
            for (int i = 0; i < stages.Count - 1; i++)
                yield return _regex[stages[i]];
        }
    }
}
