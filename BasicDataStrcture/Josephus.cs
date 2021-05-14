using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Josephus
    {
        private Queue1<int> _diedPeople = new Queue1<int>();

        public Josephus(int count, int indexToDie)
        {
            Queue1<int> people = new Queue1<int>();
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

        public Queue1<int> DiedPeople => _diedPeople;
    }
}
