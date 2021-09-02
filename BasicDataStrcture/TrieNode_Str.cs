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
        }

        public V GetValue()
        {
            V value = _values[_digit];
            _digit = 0;
            return value;
        }

        public ITrieNode<V> GetNext(int index, bool resetDigit = true)
        {
            if (_digit == _values.Length)
                _digit = 0;

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
            else
                _digit++;
            return result;
        }

        public ITrieNode<V> SetNext(int index, ITrieNode<V> node, int R)
        {
            _digit--;
            if (node is TrieNode_Char<V> c)
            {
                _characters[_digit] = index;
                _nextMultiple = c;
            }
            else if (node is TrieNode_Str<V> s)
                if (_characters[_digit] == EMPTY_INDEX)
                {
                    _characters[_digit] = index;
                    MergeChild(s);
                    _nextMultiple = s._nextMultiple;
                }
                else if (_characters[_digit] != index)
                    return Split(index, node, R);
                else if (_digit == _characters.Length - 1)
                    MergeChild(s);
            return this;
        }

        public bool NeedToShrink()
        {
            int index = _values.Length - 1;
            return !_values[_digit].Equals(default(V)) && IsFinalEmpty();
        }

        public bool IsFinalEmpty()
        {
            int index = _values.Length - 1;
            return _characters[index] == EMPTY_INDEX && _values[index].Equals(default(V));
        }

        public void Shrink()
        {
            int length = _digit + 1;

            int[] newCharacters = new int[length];
            Array.Copy(_characters, newCharacters, length - 1);
            newCharacters[length - 1] = EMPTY_INDEX;
            _characters = newCharacters;

            V[] newValues = new V[length];
            Array.Copy(_values, newValues, length);
            _values = newValues;
        }

        protected void MergeChild(TrieNode_Str<V> s)
        {
            int thisL = _values.Length;
            int newL = s._values.Length;

            int[] newCharacters = new int[thisL + newL];
            Array.Copy(_characters, newCharacters, thisL);
            Array.Copy(s._characters, 0, newCharacters, thisL, newL);
            _characters = newCharacters;

            V[] newValues = new V[thisL + newL];
            Array.Copy(_values, newValues, thisL);
            Array.Copy(s._values, 0, newValues, thisL, newL);
            _values = newValues;

            _nextMultiple = s._nextMultiple;
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

                _nextMultiple = cNode;
            }
            return cNode;
        }
    }
}
