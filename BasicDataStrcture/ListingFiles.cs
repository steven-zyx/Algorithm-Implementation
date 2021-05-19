using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace BasicDataStrcture
{
    public class ListingFiles
    {
        public static void List()
        {
            DirectoryInfo dir = new DirectoryInfo(Console.ReadLine());
            IQueue<object> queue = PopulateToQueue(dir);
            Console.WriteLine(dir.Name);
            Output(queue, "\t");
        }

        private static IQueue<object> PopulateToQueue(DirectoryInfo dir)
        {
            var queue = new Queue_A<object>();
            foreach (FileInfo file in dir.GetFiles())
            {
                queue.Enqueue(file.Name);
            }
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                queue.Enqueue(subDir.Name);
                queue.Enqueue(PopulateToQueue(subDir));
            }
            return queue;
        }

        private static void Output(IQueue<object> queue, string indent)
        {
            foreach (var obj in (IEnumerable)queue)
            {
                if (obj is string)
                    Console.WriteLine(indent + obj);
                else
                    Output((IQueue<object>)obj, indent + "\t");
            }
        }
    }
}
