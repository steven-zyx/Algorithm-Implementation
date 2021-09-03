using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace BasicDataStrcture
{
    public class TrieNode_Str<V> : ITrieNode<V>
    {
        public int[] Characters { get; set; }
        public V[] _values;
        public V Value
        {
            get => _values[Digit];
            set => _values[Digit] = value;
        }

        public int Digit { get; set; }
        protected TrieNode_Char<V> _nextMultiple;
        protected const int EMPTY_INDEX = -1;

        public TrieNode_Str() : this(1)
        {
            Characters[0] = EMPTY_INDEX;
        }

        protected TrieNode_Str(int length)
        {
            Digit = 0;
            Characters = new int[length];
            _values = new V[length];
            _nextMultiple = null;
        }

        public ITrieNode<V> GetNext(int index)
        {
            if (Characters[Digit] == index)
                if (Digit + 1 == Characters.Length)
                    return _nextMultiple;
                else
                    return this;
            else
                return null;
        }

        public ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R)
        {
            if (node is TrieNode_Char<V> c)
            {
                Characters[Digit] = index;
                _nextMultiple = c;
            }
            else if (node is TrieNode_Str<V> s)
                if (Characters[Digit] == EMPTY_INDEX)
                {
                    Characters[Digit] = index;
                    MergeChild(s);
                    _nextMultiple = s._nextMultiple;
                }
                else if (Characters[Digit] != index)
                    return Split(index, node, R);
                else if (Digit == Characters.Length - 1)
                    MergeChild(s);
            return this;
        }

        public bool IsFinalEmpty()
        {
            int index = _values.Length - 1;
            return Characters[index] == EMPTY_INDEX && _values[index].Equals(default(V));
        }

        public void Shrink()
        {
            int length = Digit + 1;

            int[] newCharacters = new int[length];
            Array.Copy(Characters, newCharacters, length - 1);
            newCharacters[length - 1] = EMPTY_INDEX;
            Characters = newCharacters;

            V[] newValues = new V[length];
            Array.Copy(_values, newValues, length);
            _values = newValues;
        }

        protected void MergeChild(TrieNode_Str<V> s)
        {
            int thisL = _values.Length;
            int newL = s._values.Length;

            int[] newCharacters = new int[thisL + newL];
            Array.Copy(Characters, newCharacters, thisL);
            Array.Copy(s.Characters, 0, newCharacters, thisL, newL);
            Characters = newCharacters;

            V[] newValues = new V[thisL + newL];
            Array.Copy(_values, newValues, thisL);
            Array.Copy(s._values, 0, newValues, thisL, newL);
            _values = newValues;

            _nextMultiple = s._nextMultiple;
        }

        protected TrieNode_Char<V> Split(int index, ITrieNode<V> node, int R)
        {
            ITrieNode<V> after = _nextMultiple;
            if (Digit < _values.Length - 1)
            {
                int length = _values.Length - Digit - 1;
                TrieNode_Str<V> temp = new TrieNode_Str<V>(length);
                Array.Copy(_values, Digit + 1, temp._values, 0, length);
                Array.Copy(Characters, Digit + 1, temp.Characters, 0, length);
                temp._nextMultiple = _nextMultiple;
                after = temp;
            }

            TrieNode_Char<V> cNode = new TrieNode_Char<V>(R);
            cNode.Value = _values[Digit];
            cNode.Next[index] = node;
            cNode.Next[Characters[Digit]] = after;

            if (Digit > 0)
            {
                V[] newValue = new V[Digit];
                Array.Copy(_values, newValue, Digit);
                _values = newValue;

                int[] newCharacters = new int[Digit];
                Array.Copy(Characters, newCharacters, Digit);
                Characters = newCharacters;

                _nextMultiple = cNode;
            }
            return cNode;
        }
    }
}
