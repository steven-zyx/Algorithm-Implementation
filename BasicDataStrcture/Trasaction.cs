using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Trasaction
    {
        private readonly string _who;
        private readonly DateTime _when;
        private readonly double _amount;

        public Trasaction(string who, DateTime when, double amount)
        {
            _who = who;
            _when = when;
            _amount = amount;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + _who.GetHashCode();
            hash = hash * 31 + _when.GetHashCode();
            hash = hash * 31 + _amount.GetHashCode();
            return hash;
        }


    }
}
