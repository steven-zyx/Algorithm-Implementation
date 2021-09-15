using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace String
{
    public class RandomPhoneNumbers
    {
        protected static Alphabet _alphabet = new Alphabet("0123456789");

        public static IEnumerable<string> PrintRandomPhoneNumbers(int count)
        {
            TrieST<bool> st = new TrieST<bool>(_alphabet);
            while (st.Size() < count)
            {
                int number = Util.Ran.Next(1_0000_0000, 10_0000_0000);
                st.Put(number.ToString(), true);
            }

            foreach (string text in st.Keys())
                yield return $"13{text[0]}-{text.Substring(1, 4)}-{text.Substring(5, 4)}";
        }
    }
}
