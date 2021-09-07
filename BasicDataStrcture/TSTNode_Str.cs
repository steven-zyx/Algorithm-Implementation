using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TSTNode_Str<V> : ITSTNode<V>
    {
        public string Characters { get; set; }

        public char C => Characters[Digit];

        public V[] Values { get; set; }

        public int Digit { get; set; }

        public V Value
        {
            get => Values[Digit];
            set => Values[Digit] = value;
        }

        public TSTNode_3<V> NextTST { get; set; }

        internal TSTNode_Str(int length)
        {
            Values = new V[length];
        }

        public TSTNode_Str(char c) : this(1)
        {
            Characters = c.ToString();
        }

        public ITSTNode<V> GetNext(char c)
        {
            if (Characters[Digit].Equals(c))
                if (Digit + 1 == Characters.Length)
                    return NextTST;
                else
                    return this;
            else
                return null;
        }

        public ITSTNode<V> SetNext(char c, ITSTNode<V> node)
        {
            if (node is TSTNode_3<V> t)
                NextTST = t;
            else if (node is TSTNode_Str<V> s)
                if (Characters[Digit].Equals(c))
                {
                    if (Digit + 1 == Characters.Length)
                        MergeChild(s);
                }
                else
                    return Split(s);
            return this;
        }

        public void MergeChild(TSTNode_Str<V> s)
        {
            Characters = Characters + s.Characters;
            NextTST = s.NextTST;

            int thisL = Values.Length;
            int childL = s.Values.Length;
            V[] newValues = new V[thisL + childL];
            Array.Copy(Values, newValues, thisL);
            Array.Copy(s.Values, 0, newValues, thisL, childL);
            Values = newValues;
        }

        public TSTNode_3<V> Split(TSTNode_Str<V> s)
        {
            ITSTNode<V> end = NextTST;
            if (Digit < Characters.Length - 1)
            {
                int length = Characters.Length - Digit - 1;
                TSTNode_Str<V> temp = new TSTNode_Str<V>(length);
                temp.Characters = Characters.Substring(Digit + 1);
                temp.NextTST = NextTST;
                Array.Copy(Values, Digit + 1, temp.Values, 0, length);
                end = temp;
            }

            TSTNode_3<V> t = new TSTNode_3<V>(Characters[Digit]);
            t.Mid = end;
            t.Value = Values[Digit];
            if (s.C < t.C)
                t.Left = s;
            else
                t.Right = s;

            if (Digit > 0)
            {
                NextTST = t;
                Characters = Characters.Substring(0, Digit);
                V[] newValues = new V[Digit];
                Array.Copy(Values, newValues, Digit);
                Values = newValues;
            }
            return t;
        }

        public bool IsFinalEmpty() => NextTST is null && Values[Values.Length - 1].Equals(default(V));

        public void Shrink()
        {
            int length = Digit + 1;
            Characters = Characters.Substring(0, length);
            V[] newValues = new V[length];
            Array.Copy(Values, newValues, length);
            Values = newValues;
        }
    }
}
