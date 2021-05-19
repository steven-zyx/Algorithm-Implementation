using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class MoveToFront : IEnumerable
    {
        private Node<char> _entrance;

        public void Add(char c)
        {
            if (_entrance == null)
            {
                _entrance = new Node<char>(c);
            }
            else if (_entrance.Value != c)
            {
                for (Node<char> current = _entrance; current.Next != null; current = current.Next)
                {
                    if (current.Next.Value == c)
                    {
                        current.Next = current.Next.Next;
                        break;
                    }
                }
                Node<char> newEntrance = new Node<char>(c);
                newEntrance.Next = _entrance;
                _entrance = newEntrance;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (Node<char> current = _entrance; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }
    }
}
