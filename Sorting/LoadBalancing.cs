using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class TaskList : IComparable
    {
        private List<int> _taskList = new List<int>();
        private long _processingTime = 0;

        public void Add(int t)
        {
            _taskList.Add(t);
            _processingTime += t;
        }

        public long ProcessingTime => _processingTime;

        public int CompareTo(object obj)
        {
            long result = _processingTime - (obj as TaskList)._processingTime;
            if (result < 0)
                return -1;
            else if (result == 0)
                return 0;
            else return 1;
        }
    }

    public class LoadBalancing
    {
        private int[] _tasks;
        private int _pCount;
        private MinPQ<TaskList> _processor;
        private QuickSort _sortClient = new QuickSort();

        public LoadBalancing(int[] tasks, int processors)
        {
            _tasks = tasks;
            _pCount = processors;
            _processor = new MinPQ<TaskList>(_pCount);

            for (int i = 0; i < _pCount; i++)
                _processor.Insert(new TaskList());

            _sortClient.Sort(_tasks);
        }

        public void Arrange()
        {
            for (int i = _tasks.Length - 1; i >= 0; i--)
            {
                TaskList tl = _processor.DeleteMin();
                tl.Add(_tasks[i]);
                _processor.Insert(tl);
            }
        }

        public Tuple<long, long> ShowMinMax()
        {
            long min = _processor.DeleteMin().ProcessingTime;
            long max = min;
            while (!_processor.IsEmpty)
                max = _processor.DeleteMin().ProcessingTime;

            return new Tuple<long, long>(min, max);
        }
    }
}
