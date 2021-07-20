using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.IO;
using System.Linq;

namespace Searching
{
    public class BST_23_GenerateAll : BST_23<int, bool>
    {
        private int _height;
        private string _fileName;
        private TreeNode_C<int, bool>[] _heap;
        private int _maxNo;

        public BST_23_GenerateAll(int height, string fileName)
        {
            _height = height;
            int heapSize = (int)Math.Pow(2, _height);
            _heap = new TreeNode_C<int, bool>[heapSize];

            _fileName = fileName;
            File.Delete(_fileName);

            for (int i = 1; i < _heap.Length; i++)
                _heap[i] = new TreeNode_C<int, bool>(0, false, 0, BLACK);
            TestCase();

            _maxNo = (int)Math.Pow(2, _height) - 1;
            GenerateNode(2);
        }


        private void GenerateNode(int no)
        {
            if (no == _maxNo)
            {
                _heap[no].N = -1;
                TestCase();
                return;
            }

            _heap[no].N = 0;

            _heap[no].Color = BLACK;
            GenerateNode(no + 1);

            if (no % 2 == 0)
            {
                _heap[no].Color = RED;
                GenerateNode(no + 1);
            }

            _heap[no].N = -1;
            GenerateNode(no + 1);
        }


        private int _count = 0;
        private void TestCase()
        {
            if (++_count % 10 == 0)
                Console.WriteLine($"Case {_count}");
            GenerateTreeFromBinaryHeap();
            try
            {
                Certificate();
                if (_maxSimpleLevel != _height)
                    return;

                File.AppendAllText(_fileName, DrawTree());
            }
            catch { }
        }

        private void GenerateTreeFromBinaryHeap()
        {
            _root = _heap[1];
            for (int i = 2; i < _heap.Length; i++)
            {
                TreeNode_C<int, bool> h = _heap[i];
                if (h.N == -1)
                    h = null;

                int p = i / 2;
                if (i % 2 == 0)
                    _heap[p].Left = h;
                else
                    _heap[p].Right = h;
            }
        }



        private int _blackLevel = -1;
        private int _maxSimpleLevel = -1;

        public override void Certificate()
        {
            _blackLevel = -1;
            _maxSimpleLevel = -1;
            IsLegal(Root, 0, 0);
        }

        private void IsLegal(TreeNode_C<int, bool> h, int simpleLevel, int blackLevel)
        {
            if (h == null)
            {
                if (_blackLevel == -1)
                    _blackLevel = blackLevel;
                else if (_blackLevel != blackLevel)
                    throw new Exception("Different black level");

                if (_maxSimpleLevel < simpleLevel)
                    _maxSimpleLevel = simpleLevel;

                return;
            }


            if (IsRed(h) && IsRed(h.Left_C))
                throw new Exception("A node connected with 2 red links");

            if (IsRed(h.Right_C))
                throw new Exception("A right-leaing red link");

            IsLegal(h.Left_C, simpleLevel + 1, IsRed(h.Left_C) ? blackLevel : blackLevel + 1);
            IsLegal(h.Right_C, simpleLevel + 1, IsRed(h.Right_C) ? blackLevel : blackLevel + 1);
        }
    }
}

