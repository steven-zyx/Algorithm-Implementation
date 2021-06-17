using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class Email : IComparable
    {
        private string _address;
        private string _domain;

        public Email(string address)
        {
            _address = address;
            _domain = address.Substring(address.IndexOf('@') + 1);
        }

        public string Address => _address;

        public string Domain => _domain;

        public int CompareTo(object obj)
        {
            return _domain.CompareTo((obj as Email)._domain);
        }
    }

    public class SpamCampaign
    {
        private QuickSort_G<Email> _sortClient = new QuickSort_G<Email>();

        public SpamCampaign(string[] emailList)
        {
            Email[] source = emailList.Select(x => new Email(x)).ToArray();
            _sortClient.Sort(source);

            Email sender = source[0];
            for (int i = 0; i < source.Length; i++)
            {
                if (sender.Domain != source[i].Domain)
                    sender = source[i];

                if (sender.Address == source[i].Address)
                {
                    if (i + 1 < source.Length && source[i + 1].Domain == source[i].Domain)
                        Console.WriteLine($"TO: {source[i].Address} FROM: {source[i + 1].Address}");
                    else
                        Console.WriteLine($"TO: {source[i].Address} FROM : single domain");
                }
                else
                    Console.WriteLine($"TO: {source[i].Address} FROM: {sender.Address}");
            }
        }
    }
}
