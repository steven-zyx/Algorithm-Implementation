﻿using System;
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
        protected const int EMPTY_INDEX = -1;

        public TrieNode_Str() : this(1)
        {
            _characters[0] = EMPTY_INDEX;
        }

        protected TrieNode_Str(int length)
        {
            _digit = 0;
            _characters = new int[length];
            _values = new V[length];
            _nextMultiple = null;
        }

        public void SetValue(V value)
        {
            _values[_digit] = value;
            DecrementDigit();
        }

        public V GetValue()
        {
            V value = _values[_digit];
            _digit = 0;
            return value;
        }

        public ITrieNode<V> GetNext(int index, bool resetDigit = true)
        {
            ITrieNode<V> result = null;
            if (_characters[_digit] == index)
                if (_digit + 1 == _characters.Length)
                    result = _nextMultiple;
                else
                {
                    _digit++;
                    return this;
                }

            if (resetDigit)
                _digit = 0;
            return result;
        }

        public ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R)
        {
            if (node is TrieNode_Char<V> c)
                _nextMultiple = c;
            else
            {
                TrieNode_Str<V> s = node as TrieNode_Str<V>;
                if (_characters[_digit] == EMPTY_INDEX)
                {
                    _characters[_digit] = index;
                    MergeNew(s);
                }
                else if (_characters[_digit] != index)
                    return Split(index, node, R);
            }
            DecrementDigit();
            return this;
        }

        protected void MergeNew(TrieNode_Str<V> s)
        {
            int thisLength = _values.Length;
            int newLength = s._values.Length;

            int[] newCharacters = new int[thisLength + newLength];
            Array.Copy(_characters, newCharacters, thisLength);
            Array.Copy(s._characters, 0, newCharacters, thisLength, newLength);
            _characters = newCharacters;

            V[] newValues = new V[thisLength + newLength];
            Array.Copy(_values, newValues, thisLength);
            Array.Copy(s._values, 0, newValues, thisLength, newLength);
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

        protected void DecrementDigit()
        {
            if (_digit > 0)
                _digit--;
        }
    }
}
