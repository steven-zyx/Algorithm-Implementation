using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgorithmImplementation.Graph
{
    public class EulerianCycle
    {
        protected Graph _g;
        public bool IsEulerianCycle { get; protected set; }

        public EulerianCycle(Graph g)
        {
            _g = g;
            IsEulerianCycle = true;
        }

        public void Process()
        {
            for (int i = 0; i < _g.V; i++)
                if (_g.Adj(i).Count() % 2 == 1)
                    IsEulerianCycle = false;
        }
    }
}
