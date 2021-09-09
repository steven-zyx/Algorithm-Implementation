using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class UniqueSubString
    {
        protected string _content;

        public UniqueSubString(string content)
        {
            _content = content;
        }

        public IEnumerable<string> SubStringOfLength(int length)
        {
            TST<bool> client = new TST<bool>();
            for (int i = 0; i < _content.Length - length; i++)
                client.Put(_content.Substring(i, length), true);
            return client.Keys();
        }
    }
}
