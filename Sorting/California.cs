using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sorting
{
    public class Name : IComparable
    {
        private string _name;

        public Name(string name)
        {
            _name = name;
        }

        public string Text => _name;

        public int CompareTo(object obj)
        {
            string otherName = (obj as Name)._name;
            int i = 0;
            while (_name[i] == otherName[i])
            {
                i++;
                if (i >= _name.Length)
                    return -1;
                else if (i >= otherName.Length)
                    return 1;
            }
            return California.CharOrder[_name[i]] - California.CharOrder[otherName[i]];
        }
    }

    public class California
    {
        public static readonly Dictionary<char, int> CharOrder;
        static California()
        {
            string order = "rwqojmvahbsgzxntciekupdyfl";

            CharOrder = new Dictionary<char, int>();
            for (int i = 0; i < order.Length; i++)
                CharOrder[order[i]] = i;
        }

        private QuickSort_G<Name> _sortClient = new QuickSort_G<Name>();
        public IEnumerable Sort(string[] nameList)
        {
            Name[] source = nameList.Select(x => new Name(x)).ToArray();
            _sortClient.Sort(source);
            foreach (Name item in source)
                yield return item.Text;
        }
    }
}
