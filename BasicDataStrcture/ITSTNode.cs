using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public interface ITSTNode<V>
    {
        V Value { get; set; }

        char C { get; }
    }
}
