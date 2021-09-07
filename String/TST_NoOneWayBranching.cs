using BasicDataStrcture;
using Searching;
using System;
using System.Collections.Generic;

namespace String
{
    public partial class TST_NoOneWayBranching<V> : IStringSymbolTable<V>
    {
        protected ITSTNode<V> _root;

        public void Put(string key, V value)
        {
            _root = Put(_root, key, 0, value);
        }

        protected ITSTNode<V> Put(ITSTNode<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TSTNode_Str<V>(key[digit]);

            if (key[digit].Equals(node.C) && digit + 1 == key.Length)
            {
                node.Value = value;
                return node;
            }

            int status = key[digit].CompareTo(node.C);
            if (node is TSTNode_3<V> t)
            {
                if (status < 0)
                    t.Left = Put(t.Left, key, digit, value);
                else if (status > 0)
                    t.Right = Put(t.Right, key, digit, value);
                else
                    t.Mid = Put(t.Mid, key, digit + 1, value);
                return t;
            }
            else
            {
                TSTNode_Str<V> s = node as TSTNode_Str<V>;
                ITSTNode<V> next = s.GetNext(key[digit]);
                s.Digit++;
                ITSTNode<V> result = status == 0 ?
                    Put(next, key, digit + 1, value) :
                    Put(next, key, digit, value);
                s.Digit--;
                return s.SetNext(key[digit], result);
            }
        }

        public V Get(string key)
        {
            var result = Get(_root, key, 0);
            if (result.node is TSTNode_3<V> t)
                return t.Value;
            else if (result.node is TSTNode_Str<V> s)
                return s.Values[result.digit];
            else
                return default(V);
        }

        protected (ITSTNode<V> node, int digit) Get(ITSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return (null, -1);

            if (node is TSTNode_3<V> t)
            {
                int status = key[digit].CompareTo(t.C);
                if (status < 0)
                    return Get(t.Left, key, digit);
                else if (status > 0)
                    return Get(t.Right, key, digit);
                else if (digit + 1 == key.Length)
                    return (t, -1);
                else
                    return Get(t.Mid, key, digit + 1);
            }
            else
            {
                TSTNode_Str<V> s = node as TSTNode_Str<V>;
                if (node.C.Equals(key[digit]) && digit + 1 == key.Length)
                    return (s, s.Digit);
                else
                {
                    ITSTNode<V> next = s.GetNext(key[digit]);
                    s.Digit++;
                    var result = Get(next, key, digit + 1);
                    s.Digit--;
                    return result;
                }
            }
        }

        public bool Delete(string key)
        {
            _root = Delete(_root, key, 0);
            if (_root is TSTNode_Str<V> s && s.IsFinalEmpty())
                _root = null;
            return true;
        }

        protected ITSTNode<V> Delete(ITSTNode<V> node, string key, int digit)
        {
            if (node == null)
                return null;

            int status = key[digit].CompareTo(node.C);
            if (status == 0 && digit + 1 == key.Length)
            {
                node.Value = default(V);
                return node;
            }

            if (node is TSTNode_3<V> t)
            {
                if (status < 0)
                {
                    t.Left = Delete(t.Left, key, digit);
                    if (t.Left is TSTNode_Str<V> s && s.IsFinalEmpty())
                        t.Left = null;
                }
                else if (status > 0)
                {
                    t.Right = Delete(t.Right, key, digit);
                    if (t.Right is TSTNode_Str<V> s && s.IsFinalEmpty())
                        t.Right = null;
                }
                else
                {
                    t.Mid = Delete(t.Mid, key, digit + 1);
                    if (t.Mid is TSTNode_Str<V> s && s.IsFinalEmpty())
                        t.Mid = null;
                }

                if (t.Mid == null && t.Value.Equals(default(V)))
                {
                    if (t.Left == null)
                        return t.Right;
                    else if (t.Right == null)
                        return t.Left;
                }
                else if (t.Left == null && t.Right == null)
                    return t.MergeChild(t.Mid);

                return t;
            }
            else
            {
                TSTNode_Str<V> s = node as TSTNode_Str<V>;
                ITSTNode<V> next = s.GetNext(key[digit]);
                s.Digit++;
                ITSTNode<V> result = Delete(next, key, digit + 1);
                s.Digit--;
                s.SetNext(key[digit], result);

                if (!s.Value.Equals(default(V)) && s.IsFinalEmpty())
                    s.Shrink();
                return s;
            }
        }

        public bool IsEmpty => _root == null;

        public bool Contains(string key)
        {
            var result = Get(_root, key, 0);
            return result.node is TSTNode_Str<V> s && !s.Values[result.digit].Equals(default(V)) ||
                result.node is TSTNode_3<V> t && !t.Value.Equals(default(V));
        }

        public IEnumerable<string> Keys()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysWithPrefix(string s)
        {
            throw new NotImplementedException();
        }

        public string LongestPrefixOf(string s)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }

    public partial class TST_NoOneWayBranching<V> : ICertificate
    {
        public void Certificate()
        {
            throw new NotImplementedException();
        }
    }
}
