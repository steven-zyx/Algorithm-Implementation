using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class UniqueSubStringOfLength
    {
        public IEnumerable<string> SubStringOfLength(string content, int length)
        {
            TST<bool> client = new TST<bool>();
            for (int i = 0; i < content.Length - length; i++)
                client.Put(content.Substring(i, length), true);
            return client.Keys();
        }
    }
}
