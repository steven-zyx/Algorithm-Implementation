using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String
{
    public class KeywordInContext
    {
        protected SuffixArray _client;
        protected string _text;

        public KeywordInContext(string text)
        {
            _text = text;
            _client = new SuffixArray(_text);
        }

        public IEnumerable<string> ContextList(string keyword)
        {
            int rank = _client.Rank(keyword);
            while (_client.Select(rank).StartsWith(keyword))
            {
                int position = _client.Index(rank);
                int leftPad = Math.Min(10, position);
                yield return _text.Substring(position - leftPad, leftPad + keyword.Length + 10);
                rank++;
            }
        }
    }
}
