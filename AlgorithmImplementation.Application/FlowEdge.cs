using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Application
{
    public class FlowEdge
    {
        public int From { get; }

        public int To { get; }

        public double Capacity { get; }

        public double Flow { get; protected set; }

        public FlowEdge(int v, int w, double cap)
        {
            From = v;
            To = w;
            Capacity = cap;
            Flow = 0;
        }

        public int Other(int v)
        {
            if (v == From)
                return To;
            else if (v == To)
                return From;
            else
                throw new InvalidOperationException();
        }

        public double ResidualCapacityTo(int v)
        {
            if (v == To)
                return Capacity - Flow;
            else if (v == From)
                return Flow;
            else
                throw new InvalidOperationException();
        }

        public void AddFlowTo(int v, double delta)
        {
            if (v == To)
                Flow += delta;
            else if (v == From)
                Flow -= delta;
            else
                throw new InvalidOperationException();
        }
    }
}
