using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Linq;

namespace Searching
{
    //Common symbol table functions
    public partial class BST<K, V> : IOrderedSymbolTable<K, V> where K : IComparable
    {
        protected TreeNode<K, V> _root;

        public BST() => Init();

        public void Init()
        {
            _root = null;
        }

        public V this[K key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public virtual V Get(K key)
        {
            var node = Get(_root, key);
            if (node == null)
                return default(V);
            else
                return node.Value;
        }

        protected virtual TreeNode<K, V> Get(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;

            int result = key.CompareTo(x.Key);
            if (result < 0)
                return Get(x.Left, key);
            else if (result > 0)
                return Get(x.Right, key);
            else
                return x;
        }

        public virtual void Put(K key, V value)
        {
            _root = Put(_root, key, value);
        }

        protected virtual TreeNode<K, V> Put(TreeNode<K, V> x, K key, V value)
        {
            if (x == null)
                return new TreeNode<K, V>(key, value, 1);

            int result = key.CompareTo(x.Key);
            if (result < 0)
                x.Left = Put(x.Left, key, value);
            else if (result > 0)
                x.Right = Put(x.Right, key, value);
            else
                x.Value = value;

            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public bool IsEmpty => _root == null;

        public int Size() => Size(_root);

        protected int Size(TreeNode<K, V> node)
        {
            if (node == null)
                return 0;
            else
                return node.N;
        }

        public virtual bool Contains(K key) => Get(_root, key) != null;

        public virtual bool Delete(K key)
        {
            if (_root == null)
                return false;

            int n = _root.N;
            _root = Delete(_root, key);
            return _root == null || _root.N != n;
        }

        protected virtual TreeNode<K, V> Delete(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;
            int result = key.CompareTo(x.Key);
            if (result < 0)
                x.Left = Delete(x.Left, key);
            else if (result > 0)
                x.Right = Delete(x.Right, key);
            else
            {
                if (x.Right == null)
                    return x.Left;
                else if (x.Left == null)
                    return x.Right;
                else
                {
                    TreeNode<K, V> t = x;
                    x = Min(x.Right);
                    x.Right = DeleteMin(t.Right);
                    x.Left = t.Left;
                }
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public virtual IEnumerable<K> Keys()
        {
            List<K> output = new List<K>();
            Print(_root, output);
            return output;
        }

        protected void Print(TreeNode<K, V> x, List<K> output)
        {
            if (x == null) return;
            Print(x.Left, output);
            output.Add(x.Key);
            Print(x.Right, output);
        }
    }

    //Ordered symbol table functions
    public partial class BST<K, V>
    {
        public K Min()
        {
            if (_root == null)
                return default(K);
            else
                return Min(_root).Key;
        }

        protected TreeNode<K, V> Min(TreeNode<K, V> x)
        {
            if (x.Left == null)
                return x;
            else
                return Min(x.Left);
        }

        public K Max()
        {
            if (_root == null)
                return default(K);
            else
                return Max(_root).Key;
        }

        protected TreeNode<K, V> Max(TreeNode<K, V> x)
        {
            if (x.Right == null)
                return x;
            else
                return Max(x.Right);
        }

        public virtual void DeleteMax()
        {
            _root = DeleteMax(_root);
        }

        protected virtual TreeNode<K, V> DeleteMax(TreeNode<K, V> x)
        {
            if (x.Right == null)
                return x.Left;
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public virtual void DeleteMin()
        {
            _root = DeleteMin(_root);
        }

        protected virtual TreeNode<K, V> DeleteMin(TreeNode<K, V> x)
        {
            if (x.Left == null)
                return x.Right;
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public virtual K Floor(K key)
        {
            var node = Floor(_root, key);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Floor(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;

            int result = key.CompareTo(x.Key);
            if (result == 0)
                return x;
            else if (result < 0)
                return Floor(x.Left, key);
            else
            {
                var node = Floor(x.Right, key);
                if (node == null)
                    return x;
                else
                    return node;
            }
        }

        public virtual K Ceiling(K key)
        {
            var node = Ceiling(_root, key);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Ceiling(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return null;

            int result = key.CompareTo(x.Key);
            if (result == 0)
                return x;
            else if (result > 0)
                return Ceiling(x.Right, key);
            else
            {
                var node = Ceiling(x.Left, key);
                if (node == null)
                    return x;
                else
                    return node;
            }
        }

        public virtual IEnumerable<K> Keys(K lo, K hi)
        {
            List<K> output = new List<K>();
            Print(_root, lo, hi, output);
            return output;
        }

        protected void Print(TreeNode<K, V> x, K lo, K hi, List<K> output)
        {
            if (x == null)
                return;
            int loResult = x.Key.CompareTo(lo);
            int hiResult = x.Key.CompareTo(hi);
            if (loResult > 0)
                Print(x.Left, lo, hi, output);
            if (loResult >= 0 && hiResult <= 0)
                output.Add(x.Key);
            if (hiResult < 0)
                Print(x.Right, lo, hi, output);
        }

        public int Rank(K key) => Rank(_root, key);

        protected virtual int Rank(TreeNode<K, V> x, K key)
        {
            if (x == null)
                return 0;

            int result = key.CompareTo(x.Key);
            if (result == 0)
                return Size(x.Left);
            else if (result < 0)
                return Rank(x.Left, key);
            else
                return Size(x.Left) + 1 + Rank(x.Right, key);
        }

        public virtual K Select(int k)
        {
            var node = Select(_root, k);
            if (node == null)
                return default(K);
            else
                return node.Key;
        }

        protected TreeNode<K, V> Select(TreeNode<K, V> x, int k)
        {
            if (x == null)
                return null;

            int t = Size(x.Left);
            if (t > k)
                return Select(x.Left, k);
            else if (t < k)
                return Select(x.Right, k - t - 1);
            else
                return x;
        }

        public virtual int Size(K lo, K hi)
        {
            int count = 0;
            Size(_root, lo, hi, ref count);
            return count;
        }

        protected void Size(TreeNode<K, V> x, K lo, K hi, ref int count)
        {
            if (x == null) return;
            int loResult = x.Key.CompareTo(lo);
            int hiResult = x.Key.CompareTo(hi);
            if (loResult > 0)
                Size(x.Left, lo, hi, ref count);
            if (loResult >= 0 && hiResult <= 0)
                count++;
            if (hiResult < 0)
                Size(x.Right, lo, hi, ref count);
        }
    }

    //BST dedicated funcions
    public partial class BST<K, V>
    {
        public IEnumerable<K> PrintLevel() => TraverseByLevel().Select(x => x.Node.Key);

        public override bool Equals(object obj)
        {
            BST<K, V> other = obj as BST<K, V>;
            if (other._root.N != _root.N)
                return false;

            var otherIterator = other.TraverseByLevel().GetEnumerator();
            foreach (var n in TraverseByLevel())
            {
                otherIterator.MoveNext();
                var otherN = otherIterator.Current;
                if (n.Node.Key.CompareTo(otherN.Node.Key) != 0 || n.Level != otherN.Level)
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int code = 0;
            foreach (var n in TraverseByLevel())
            {
                code = (code + n.Node.Key.GetHashCode()) * 31;
                code = (code + n.Level) * 31;
            }
            return code;
        }

        public IEnumerable<(TreeNode<K, V> Node, int Level)> TraverseByLevel()
        {
            Queue<(TreeNode<K, V> Node, int level)> nodeQ = new Queue<(TreeNode<K, V> Node, int level)>();
            nodeQ.Enqueue((_root, 1));

            while (nodeQ.Count > 0)
            {
                var n = nodeQ.Dequeue();
                yield return n;
                if (n.Node.Left != null)
                    nodeQ.Enqueue((n.Node.Left, n.level + 1));
                if (n.Node.Right != null)
                    nodeQ.Enqueue((n.Node.Right, n.level + 1));
            }
        }
    }
}
