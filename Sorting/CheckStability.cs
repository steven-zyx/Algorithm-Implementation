using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class Airline : IComparable
    {
        public string Location { get; set; }
        public long EventTime { get; set; }

        public Airline(string l, long t)
        {
            Location = l;
            EventTime = t;
        }

        public class AirlineLocationC : IComparer<Airline>
        {
            public int Compare(Airline x, Airline y)
            {
                return x.Location.CompareTo(y.Location);
            }
        }

        public class AirlineEventTimeC : IComparer<Airline>
        {
            public int Compare(Airline x, Airline y)
            {
                return x.EventTime.CompareTo(y.EventTime);
            }
        }

        public int CompareTo(object obj)
        {
            return Location.CompareTo((obj as Airline).Location);
        }
    }

    public class CheckStability
    {
        private QuickSort_C<Airline> _sortClient_Q = new QuickSort_C<Airline>();
        private Airline[] _source;

        public CheckStability()
        {
            string[] locations = new string[] { "Shanghai", "Suzhou", "Beijing", "Guangzhou", "Hangzhou" };
            List<Airline> airLines = new List<Airline>();
            foreach (string lo in locations)
                for (int i = 0; i < 20; i++)
                    airLines.Add(new Airline(lo, i));
            _source = airLines.ToArray();
        }

        public bool IsMergeSortStable()
        {
            _sortClient_Q.Sort(_source, new Airline.AirlineEventTimeC());

            MergeSort_C<Airline> client = new MergeSort_C<Airline>();
            client.Sort(_source, new Airline.AirlineLocationC());
            return IsStable();
        }

        public bool IsQuickSortStable()
        {
            _sortClient_Q.Sort(_source, new Airline.AirlineEventTimeC());
            _sortClient_Q.Sort(_source, new Airline.AirlineLocationC());
            return IsStable();
        }

        public bool IsAdjustedPriorityQueueStable()
        {
            _sortClient_Q.Sort(_source, new Airline.AirlineEventTimeC());

            MinPQ_Stable<Airline> client = new MinPQ_Stable<Airline>(_source.Length);
            foreach (Airline a in _source)
                client.Insert(a);
            for (int i = 0; i < _source.Length; i++)
                _source[i] = client.DeleteMin();

            return IsStable();

        }

        public bool IsQuickSortStableByForce()
        {
            _sortClient_Q.Sort(_source, new Airline.AirlineEventTimeC());
            Wrapper4Sort<Airline>[] wrappedList = Wrapper4Sort<Airline>.Wrap(_source);

            QuickSort_G<Wrapper4Sort<Airline>> client = new QuickSort_G<Wrapper4Sort<Airline>>();
            client.Sort(wrappedList);

            for (int i = 0; i < wrappedList.Length; i++)
                _source[i] = wrappedList[i].Obj;
            return IsStable();
        }

        private bool IsStable()
        {
            long smallEventTime = _source[0].EventTime;
            string location = _source[0].Location;
            foreach (Airline a in _source)
            {
                if (a.Location != location)
                {
                    location = a.Location;
                    smallEventTime = a.EventTime;
                    continue;
                }
                if (a.EventTime < smallEventTime)
                    return false;
                else
                    smallEventTime = a.EventTime;
            }
            return true;
        }
    }
}
