using System;

namespace AlgorithmImplementation.Application
{
    public class Event : IComparable<Event>
    {
        protected double _time;
        protected Particle _a;
        protected Particle _b;
        protected int _countA;
        protected int _countB;

        public Event(double time, Particle a, Particle b)
        {
            _time = time;
            _a = a;
            _b = b;

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
            if (_time < other._time)
                return -1;
            else if (_time > other._time)
                return 1;
            else
                return 0;
        }

        public bool IsValid()
        {
            if (_a != null && _a.Count != _countA)
                return false;
            if (_b != null && _b.Count != _countB)
                return false;
            return true;
        }
    }
}
