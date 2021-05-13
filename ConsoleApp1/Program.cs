using System;
using System.Diagnostics;
using BasicDataStrcture;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestListResize();
            Console.ReadKey();
        }

        public static void TestListResize()
        {
            List1<int> numbers = new List1<int>();
            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < 1000; i++)
            {
                watch.Restart();
                numbers.Add(i);
                Console.WriteLine(watch.ElapsedTicks);
            }
        }
    }
}
