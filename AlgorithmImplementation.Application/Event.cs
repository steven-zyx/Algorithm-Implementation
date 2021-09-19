using System;

namespace AlgorithmImplementation.Application
{
    public class Event : IComparable<Event>
    {
        public double Time { get; }
        public Particle A;
        public Particle B;
        protected int _countA;
        protected int _countB;

        public Event(double time, Particle a, Particle b)
        {
            Time = time;
            A = a;
            B = b;

            if (a != null)
                _countA = a.Count;
            else
                _countA = -1;

            if (b != null)
                _countB = b.Count;
            else
                _countB = -1;

        }

        public int CompareTo(Event other)
        {
            if (Time < other.Time)
                return -1;
            else if (Time > other.Time)
                return 1;
            else
                return 0;
        }

        public bool IsValid()
        {
            if (A != null && A.Count != _countA)
                return false;
            if (B != null && B.Count != _countB)
                return false;
            return true;
        }
    }
}
