using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public partial class TST_Ordered<V> : TST<V>
    {
        protected bool _isToAdd;
        protected bool _isToDelete;

        protected override TSTNode<V> Put(TSTNode<V> node, string key, int digit, V value)
        {
            if (node == null)
                node = new TSTNode_N<V>(key[digit]);

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = Put(node.Left, key, digit, value);
            else if (result > 0)
                node.Right = Put(node.Right, key, digit, value);
            else if (digit + 1 == key.Length)
            {
                _isToAdd = node.Value.Equals(default(V));
                node.Value = value;
            }
            else
                node.Mid = Put(node.Mid, key, digit + 1, value);

            if (_isToAdd)
                (node as TSTNode_N<V>).N++;

            return node;
        }

        protected override int Size(TSTNode<V> x)
        {
            TSTNode_N<V> node = x as TSTNode_N<V>;
            if (node == null)
                return 0;
            else
                return node.N;
        }

        protected override TSTNode<V> Delete(TSTNode<V> node, string key, int digit)
        {
            if (node == null)
            {
                _isToDelete = false;
                return null;
            }

            int result = key[digit].CompareTo(node.C);
            if (result < 0)
                node.Left = Delete(node.Left, key, digit);
            else if (result > 0)
                node.Right = Delete(node.Right, key, digit);
            else if (digit + 1 == key.Length)
            {
                _isToDelete = true;
                node.Value = default(V);
            }
            else
                node.Mid = Delete(node.Mid, key, digit + 1);

            if (_isToDelete)
                (node as TSTNode_N<V>).N--;

            if (node.Value.Equals(default(V)) && node.Mid == null)
                return DeleteNode(node);
            else
                return node;
        }

        protected override TSTNode<V> DeleteNode(TSTNode<V> node)
        {
            if (node.Right == null)
                return node.Left;
            else if (node.Left == null)
                return node.Right;
            else
            {
                TSTNode<V> substitute = Min(node.Right);
                substitute.Right = DeleteMin(node.Right);
                substitute.Left = node.Left;
                (substitute as TSTNode_N<V>).N = (node as TSTNode_N<V>).N;
                return substitute;
            }
        }

        protected override TSTNode<V> DeleteMin(TSTNode<V> node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = DeleteMin(node.Left);
            UpdateN(node);
            return node;
        }

        protected void UpdateN(TSTNode<V> node)
        {
            int size = Size(node.Left) + Size(node.Mid) + Size(node.Right);
            if (!node.Value.Equals(default(V)))
                size++;
            (node as TSTNode_N<int>).N = size;
        }
    }

    //Functions for certification
    public partial class TST_Ordered<V>
    {
        protected override void RecrusiveCert(TSTNode<V> node)
        {
            if (node == null)
                return;

            //check order
            if (node.Left != null && node.Left.C.CompareTo(node.C) >= 0)
                throw new Exception("left greater than or equal to parent");
            else if (node.Right != null && node.Right.C.CompareTo(node.C) <= 0)
                throw new Exception("right less than or equal to parent");

            //check N
            int size = Size(node.Left) + Size(node.Right) + Size(node.Mid);
            if (!node.Value.Equals(default(V)))
                size++;
            if ((node as TSTNode_N<V>).N != size)
                throw new Exception("Incorrect size");

            RecrusiveCert(node.Left);
            RecrusiveCert(node.Mid);
            RecrusiveCert(node.Right);
        }
    }
}
