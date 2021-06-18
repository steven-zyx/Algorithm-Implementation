using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class IdleTime
    {
        public Tuple<int, int>[] GenerateJob(int count, int totalDuration, int averageDuration)
        {
            Random r = new Random(DateTime.Now.Second);
            Tuple<int, int>[] tasks = new Tuple<int, int>[count];
            for (int i = 0; i < count; i++)
            {
                int start = r.Next(0, totalDuration);
                int end = start + r.Next(1, averageDuration * 2);
                tasks[i] = new Tuple<int, int>(start, end);
            }
            return tasks;
        }

        public Tuple<int, int> MaxIdleAndBusyDuration(Tuple<int, int>[] tasks)
        {
            MinPQ<int> startTimes = new MinPQ<int>(tasks.Length);
            MinPQ<int> endTimes = new MinPQ<int>(tasks.Length);
            foreach (var task in tasks)
            {
                startTimes.Insert(task.Item1);
                endTimes.Insert(task.Item2);
            }

            //----------------------------------------------------------------------------
            int busyTime = 0, idleTime = 0;
            int busyStart = startTimes.DeleteMin(), idleStart = -1;
            int taskCount = 1;

            while (!startTimes.IsEmpty)
            {
                if (startTimes.Min < endTimes.Min)      //A task begins
                {
                    int timeStamp = startTimes.DeleteMin();
                    taskCount++;
                    if (taskCount == 1)
                    {
                        //ending of idle
                        int duration = timeStamp - idleStart;
                        if (duration > idleTime)
                            idleTime = duration;

                        //begining of busy
                        busyStart = timeStamp;
                    }
                }
                else if (startTimes.Min == endTimes.Min)    //2 tasks begin and end at the same moment.
                {
                    startTimes.DeleteMin();
                    endTimes.DeleteMin();
                }
                else        //A task ends
                {
                    int timestamp = endTimes.DeleteMin();
                    taskCount--;
                    if (taskCount == 0)
                    {
                        //begining of idle
                        idleStart = timestamp;

                        //ending of busy
                        int duration = timestamp - busyStart;
                        if (duration > busyTime)
                            busyTime = duration;
                    }
                }
            }

            //the last running tasks
            int finish = 0;
            while (!endTimes.IsEmpty)
                finish = endTimes.DeleteMin();

            int lastDuration = finish - busyStart;
            if (lastDuration > busyTime)
                busyTime = lastDuration;

            return new Tuple<int, int>(idleTime, busyTime);
        }
    }
}
