using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public struct Trasaction
    {
        public string Who { get; }
        public DateTime When { get; }
        public double Amount { get; }

        private readonly int _hash;

        public Trasaction(string who, DateTime when, double amount)
        {
            Who = who;
            When = when;
            Amount = amount;
            _hash = 0;
            _hash = Hash();
        }

        private int Hash()
        {
            int hash = 17;
            hash = hash * 31 + Who.GetHashCode();
            hash = hash * 31 + When.GetHashCode();
            hash = hash * 31 + Amount.GetHashCode();
            return hash;
        }

        public override int GetHashCode() => _hash;

        public override bool Equals(object obj)
        {
            Trasaction o = (Trasaction)obj;
            return o.Who.Equals(Who) && o.When.Equals(When) && o.Amount.Equals(Amount);
        }
    }
}
