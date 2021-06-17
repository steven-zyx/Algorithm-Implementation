using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Sorting
{
    public class Domain : IComparable
    {
        private string _domain;

        public Domain(string domain)
        {
            _domain = domain.ToLower();
        }

        public string Text => _domain;

        public int CompareTo(object obj)
        {
            string otherDomain = (obj as Domain)._domain;

            int leftIndex = _domain.Length - 1;
            int rightIndex = otherDomain.Length - 1;
            while (_domain[leftIndex] == otherDomain[rightIndex])
            {
                leftIndex--;
                rightIndex--;

                if (leftIndex < 0)
                    return -1;
                if (rightIndex < 0)
                    return 1;
            }

            if (_domain[leftIndex] < otherDomain[rightIndex])
                return -1;
            else
                return 1;
        }
    }

    public class SortByReverseDomain
    {
        private QuickSort_G<Domain> _sortClient = new QuickSort_G<Domain>();

        public IEnumerable Sort(string[] domainList)
        {
            Domain[] source = domainList.Select(x => new Domain(x)).ToArray();
            _sortClient.Sort(source);
            foreach (var domain in source)
                yield return domain.Text;
        }
    }
}
