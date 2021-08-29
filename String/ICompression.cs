using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public interface ICompression
    {
        void Compress(string sourceFile, string compressedFile);

        void Expand(string compressedFile, string expandedFile);
    }
}
