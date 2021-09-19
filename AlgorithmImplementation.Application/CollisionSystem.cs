using System;
using System.Collections.Generic;
using System.Text;
using Sorting;

namespace AlgorithmImplementation.Application
{
    public class CollisionSystem
    {
        protected MinPQ<Event> _events;
        protected Particle[] _pList;

        public CollisionSystem(int count)
        {
            _pList = new Particle[count];
            for (int i = 0; i < count; i++)
                _pList[i] = new Particle();
        }

        public void Simulate()
        {

        }
    }
}
