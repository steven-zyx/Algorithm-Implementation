using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class TrieNode_Str<V> : ITrieNode<V>
    {
        protected int[] _characters;
        protected V[] _values;
        protected int _digit;
        protected TrieNode_Char<V> _nextMultiple;

        public TrieNode_Str(string key, int digit) : this(1)
        {
            if (digit < key.Length)
                _characters[0] = key[digit];
            else
                _characters[0] = -1;
        }

        protected TrieNode_Str(int length)
        {
            _digit = 0;
            _characters = new int[length];
            _values = new V[length];
            _nextMultiple = null;
        }


        public void SetValue(V value) => _values[_digit] = value;

        public ITrieNode<V> GetNext(int index)
        {
            if (_characters[_digit] != index)
                return null;
            else if (_digit + 1 < _values.Length)
            {
                _digit++;
                return this;
            }
            else
                return _nextMultiple;
        }

        public ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R)
        {
            if (node is TrieNode_Char<V> c)
                _nextMultiple = c;
            else if (node is TrieNode_Str<V> s)
                if (s._characters[0] == index || s._characters[0] == -1)
                    if (_digit == _values.Length - 1)
                        MergeNew(s);
                    else
                        _digit--;
                else
                    return Split(index, node, R);
            return this;
        }

        protected void MergeNew(TrieNode_Str<V> s)
        {
            int length = _characters.Length;

            int[] newCharacters = new int[length + 1];
            Array.Copy(_characters, newCharacters, length);
            newCharacters[length] = s._characters[0];
            _characters = newCharacters;

            V[] newValues = new V[length + 1];
            Array.Copy(_values, newValues, length);
            newValues[length] = s._values[0];
            _values = newValues;
        }

        protected TrieNode_Char<V> Split(int index, ITrieNode<V> node, int R)
        {
            ITrieNode<V> after = _nextMultiple;
            if (_digit < _values.Length - 1)
            {
                int length = _values.Length - _digit - 1;
                TrieNode_Str<V> temp = new TrieNode_Str<V>(length);
                Array.Copy(_values, _digit + 1, temp._values, 0, length);
                Array.Copy(_characters, _digit + 1, temp._characters, 0, length);
                temp._nextMultiple = _nextMultiple;
                after = temp;
            }

            TrieNode_Char<V> cNode = new TrieNode_Char<V>(R);
            cNode.SetValue(_values[_digit]);
            cNode.SetNext(index, node, R);
            cNode.SetNext(_characters[_digit], after, R);

            if (_digit > 0)
            {
                V[] newValue = new V[_digit];
                Array.Copy(_values, newValue, _digit);
                _values = newValue;

                int[] newCharacters = new int[_digit];
                Array.Copy(_characters, newCharacters, _digit);
                _characters = newCharacters;

                _digit--;
                _nextMultiple = cNode;
            }
            return cNode;
        }
    }
}
