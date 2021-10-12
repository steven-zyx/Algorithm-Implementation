using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String
{
    public class LongestRepeatedSubstring
    {
        public static string Find(string text)
        {
            SuffixArray client = new SuffixArray(text);
            string repeatS = string.Empty;
            int repeatL = 0;
            for (int i = 1; i < text.Length; i++)
            {
                int lcp = client.LCP(i);
                if (lcp > repeatL)
                {
                    repeatL = lcp;
                    repeatS = client.Select(i).Substring(0, lcp);
                }
            }
            return repeatS;
        }

    }
}
