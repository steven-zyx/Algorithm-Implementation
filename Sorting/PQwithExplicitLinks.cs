using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Diagnostics;

namespace Sorting
{
    public class PQwithExplicitLinks
    {
        private TriplyNode _root;
        private int _count = 0;

        public PQwithExplicitLinks() { }

        public void Insert(int item)
        {
            _count++;
            TriplyNode current = new TriplyNode(item);
            if (_root == null)
                _root = current;
            else
            {
                TriplyNode parent = GetCurrentParent();
                if (parent.leftSub == null)
                    parent.leftSub = current;
                else
                    parent.RightSub = current;
                current.Parent = parent;
                Swim(current);
            }
        }

        public int DeleteMin()
        {
            int item = _root.Value;
            if (_count == 1)
            {
                _root = null;
            }
            else
            {
                TriplyNode p = GetCurrentParent();
                if (p.RightSub != null)
                {
                    Exchange(_root, p.RightSub);
                    p.RightSub = null;
                }
                else
                {
                    Exchange(_root, p.leftSub);
                    p.leftSub = null;
                }
                Sink(_root);
            }
            _count--;
            return item;
        }

        private TriplyNode GetCurrentParent()
        {
            List<int> directions = new List<int>();
            int dir = _count / 2;
            while (dir > 1)
            {
                directions.Add(dir);
                dir /= 2;
            }

            TriplyNode parent = _root;
            directions.Reverse();
            foreach (int d in directions)
            {
                if (d % 2 == 0)
                    parent = parent.leftSub;
                else
                    parent = parent.RightSub;
            }
            return parent;
        }

        private void Swim(TriplyNode c)
        {
            while (c.Parent != null && Less(c, c.Parent))
            {
                Exchange(c, c.Parent);
                c = c.Parent;
            }
        }

        private void Sink(TriplyNode c)
        {
            while (c.leftSub != null)
            {
                TriplyNode sub = c.leftSub;
                if (c.RightSub != null && Less(c.RightSub, sub))
                    sub = c.RightSub;

                if (Less(sub, c))
                {
                    Exchange(sub, c);
                    c = sub;
                }
                else
                    break;
            }
        }

        private void Exchange(TriplyNode a, TriplyNode b)
        {
            int temp = a.Value;
            a.Value = b.Value;
            b.Value = temp;
        }

        private bool Less(TriplyNode a, TriplyNode b) => a.Value < b.Value;



        #region Code for test
        private int _countForVerify;
        public bool IsConsistant()
        {
            _countForVerify = 0;
            return IsConsistant(_root) && _countForVerify == _count;
        }

        private bool IsConsistant(TriplyNode c)
        {
            if (c == null)
                return true;

            _countForVerify++;
            if (LessForVerify(c, c.leftSub) && LessForVerify(c, c.RightSub))
            {
                return IsConsistant(c.leftSub) && IsConsistant(c.RightSub);
            }
            else
            {
                Trace.WriteLine("error with:\t" + c.Value);
                Trace.WriteLine("left sub:\t" + c.leftSub?.Value);
                Trace.WriteLine("right sub:\t" + c.RightSub?.Value);
                return false;
            }
        }

        private bool LessForVerify(TriplyNode a, TriplyNode b)
        {
            if (a == null || b == null)
                return true;
            else
                return a.Value < b.Value;
        }
        #endregion
    }
}
