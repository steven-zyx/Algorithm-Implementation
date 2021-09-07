using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TSTNode_3<V> : ITSTNode<V>
    {
        public char C { get; set; }

        public ITSTNode<V> Left { get; set; }

        public ITSTNode<V> Mid { get; set; }

        public ITSTNode<V> Right { get; set; }

        public V Value { get; set; }

        public TSTNode_3(char c)
        {
            C = c;
        }

        public TSTNode_Str<V> MergeChild(ITSTNode<V> child)
        {
            if (child is TSTNode_Str<V> s)
            {
                int childLength = s.Characters.Length;
                TSTNode_Str<V> current = new TSTNode_Str<V>(childLength + 1);
                current.Characters = C + s.Characters;
                current.Values[0] = Value;
                Array.Copy(s.Values, 0, current.Values, 1, childLength);
                current.NextTST = s.NextTST;
                return current;
            }
            else if (child is TSTNode_3<V> t)
            {
                TSTNode_Str<V> current = new TSTNode_Str<V>(C);
                current.Value = Value;
                current.NextTST = t;
                return current;
            }
            else
                throw new InvalidOperationException("Merging a null child");
        }
    }
}
