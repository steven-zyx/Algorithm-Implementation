using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Josephus
    {
        private Queue_N<int> _diedPeople = new Queue_N<int>();

        public Josephus(int count, int indexToDie)
        {
            Queue_N<int> people = new Queue_N<int>();
            for (int i = 0; i < count; i++)
                people.Enqueue(i);

            int n = 1;
            while (people.Length > 1)
            {
                int person = people.Dequeue();
                if (n % indexToDie == 0)
                    _diedPeople.Enqueue(person);
                else
                    people.Enqueue(person);
                n++;
            }
            _diedPeople.Enqueue(people.Dequeue());
        }

        public Queue_N<int> DiedPeople => _diedPeople;
    }
}
