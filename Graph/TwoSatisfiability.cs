using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class TwoSatisfiability
    {
        public bool IsFeasible => Assignment != null;

        public Dictionary<string, bool> Assignment { get; }

        protected SymbolDigraph<string> _symbolG;

        //(a||b)&&(!a||b)&&(a||!b)
        public TwoSatisfiability(string expression, int n)
        {
            _symbolG = new SymbolDigraph<string>(n * 2);
            string[] disjunctives = expression.Split("&&");
            foreach (string clause in disjunctives)
            {
                string[] literals = clause.Substring(1, clause.Length - 2).Split("||");
                string left = literals[0], right = literals[1];
                string negativeL = GetNegative(left), negativeR = GetNegative(right);
                _symbolG.AddEdge(negativeR, left);
                _symbolG.AddEdge(negativeL, right);
            }

            SCC_Extended scc = new SCC_Extended(_symbolG.DiG);
            foreach (string variable in _symbolG.Index.Keys)
                if (IsPositive(variable))
                    if (scc.StronglyConnected(_symbolG.Index[variable], _symbolG.Index[GetNegative(variable)]))
                        return;

            Assignment = new Dictionary<string, bool>();
            Topological topo = new Topological(scc.KernalDAG);
            foreach (int i in topo.Order().Reverse())
            {
                bool assigned = false;
                foreach (int v in scc.IDVertices[i])
                {
                    bool value;
                    if (Assignment.TryGetValue(_symbolG.Names[v], out value))
                    {
                        Assign(scc.IDVertices[i], value);
                        assigned = true;
                    }
                }
                if (!assigned)
                    Assign(scc.IDVertices[i], true);
                if (Assignment.Count == n * 2)
                    break;
            }
        }

        protected string GetNegative(string variable)
        {
            if (variable.StartsWith('!'))
                return variable.Substring(1);
            else
                return '!' + variable;
        }

        protected bool IsPositive(string variable) => !variable.StartsWith('!');

        protected void Assign(IEnumerable<int> variableList, bool value)
        {
            foreach (int v in variableList)
            {
                string variable = _symbolG.Names[v];
                Assignment[variable] = value;
                Assignment[GetNegative(variable)] = !value;
            }
        }

        public StringBuilder ShowAssignment()
        {
            StringBuilder sb = new StringBuilder();
            if (IsFeasible)
            {
                sb.Append("Feasible.\r\n");
                foreach (var pair in Assignment.OrderBy(x => x.Key))
                    sb.Append($"{pair.Key}\t=\t{pair.Value}\r\n");
            }
            else
                sb.Append("Infeasible");
            return sb;
        }
    }
}
