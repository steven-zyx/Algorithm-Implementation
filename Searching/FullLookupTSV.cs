using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Searching
{
    public class FullLookupTSV
    {
        private FileInfo _file;
        private ISymbolTable<string, List<long>>[] _st;
        private Action<int> _reportProgress;
        private ISymbolTable<string, int> _headers;

        public FullLookupTSV(string fileName, int column, Action<int> reportProgress)
        {
            _file = new FileInfo(fileName);
            _st = new ISymbolTable<string, List<long>>[column];
            for (int i = 0; i < _st.Length; i++)
                _st[i] = new SeperateChainingHashST<string, List<long>>();
            _reportProgress = reportProgress;
        }


        public void BuildHeader()
        {
            _headers = new SeperateChainingHashST<string, int>();
            using (FileStream fs = _file.OpenRead())
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    string[] columns = r.ReadLine().Split('\t');
                    for (int i = 0; i < columns.Length; i++)
                        _headers.Put(columns[i], i);
                }
            }
        }

        public void BuildIndex()
        {

            int lineNumber = 0;
            using (FileStream fs = _file.OpenRead())
            {
                int c = 0;
                long position = 0;
                List<byte> line = new List<byte>();
                while (c != -1)
                {
                    c = fs.ReadByte();
                    line.Add((byte)c);

                    if (c == 10)
                    {
                        string[] words = Encoding.UTF8.GetString(line.ToArray()).Split('\t');
                        for (int i = 0; i < _st.Length; i++)
                        {
                            if (words[i] == "")
                                continue;
                            if (_st[i].Get(words[i]) == null)
                                _st[i].Put(words[i], new List<long>() { position });
                            else
                                _st[i].Get(words[i]).Add(position);
                        }
                        line.Clear();
                        position = fs.Position;
                        _reportProgress(++lineNumber);
                    }
                }
            }
        }

        public IEnumerable<string> Search(string column, string key) => Search(_headers.Get(column), key);

        public IEnumerable<string> Search(int column, string key)
        {
            List<long> positions = _st[column].Get(key);
            if (positions == null)
                yield break;
            using (FileStream fs = _file.OpenRead())
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    foreach (long pos in positions)
                    {
                        fs.Seek(pos, SeekOrigin.Begin);
                        yield return r.ReadLine();
                    }
                }
            }
        }
    }
}
