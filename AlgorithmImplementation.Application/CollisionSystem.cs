using System;
using System.Collections.Generic;
using System.Text;
using Sorting;
using System.Drawing;

namespace AlgorithmImplementation.Application
{
    public class CollisionSystem
    {
        protected MinPQ<Event> _events;
        protected Particle[] _pList;
        protected double _t;
        protected Graphics _g;

        public CollisionSystem(int count)
        {
            _pList = new Particle[count];
            for (int i = 0; i < count; i++)
                _pList[i] = new Particle();
            _events = new MinPQ<Event>();
            _t = 0;
        }

        public void Simulate(int limit)
        {
            Redraw();
            foreach (Particle p in _pList)
                PredictCollisions(p, limit);
            while (_events.Size > 0)
            {
                Event e = _events.DeleteMin();
                if (!e.IsValid())
                    continue;

                foreach (Particle p in _pList)
                    p.Move(e.Time - _t);
                _t = e.Time;
                Redraw();

                if (e.A != null && e.B != null)
                    e.A.BounceOff(e.B);
                else if (e.A != null && e.B == null)
                    e.A.BounceOffVerticalWall();
                else if (e.A == null && e.B != null)
                    e.B.BounceOffHorizontalWall();

                if (e.A != null)
                    PredictCollisions(e.A, limit);
                if (e.B != null)
                    PredictCollisions(e.B, limit);
            }
        }

        public void PredictCollisions(Particle a, double limit)
        {
            foreach (Particle b in _pList)
            {
                double collisionT = a.TimeToHit(b);
                if (_t + collisionT <= limit)
                    _events.Insert(new Event(_t + collisionT, a, b));
            }

            double hitVWallT = a.TimeToHitVerticalWall();
            if (_t + hitVWallT <= limit)
                _events.Insert(new Event(_t + hitVWallT, a, null));

            double hitHWallT = a.TimeToHitHorizontalWall();
            if (_t + hitHWallT <= limit)
                _events.Insert(new Event(_t + hitHWallT, null, a));
        }

        public void Redraw()
        {
            foreach (Particle p in _pList)
                p.Draw(_g);
        }
    }
}
