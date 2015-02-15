using System;
using System.Collections.Generic;
using System.Threading;

namespace _03ManyThreadsThreadPool
{
    class Program
    {
        const int Max = int.MaxValue / 2;
        private static int _taskCount;

        static void Main()
        {
            Console.WriteLine("Main program thread.");
            Console.WriteLine("Queuing up a bunch of threads.");
            for (var x = 0; x < 100; x += 1)
            {
                ThreadPool.QueueUserWorkItem(Sequence);
                Thread.Sleep(10);
            }
            Console.WriteLine("Waiting for threads to finish...");
            while (_taskCount > 0)
            {
                Thread.Sleep(100);
            }
            Console.WriteLine("We're done.");
            Console.ReadLine();
        }

        private static void Sequence(Object state)
        {
            Interlocked.Increment(ref _taskCount);
            var fibonacci = new List<int>();
            Console.WriteLine("Computing sequences...");
            var x1 = 1;
            var x2 = 1;
            fibonacci.Add(x1);
            for (var x = 0; x < int.MaxValue && x2 < Max; x++)
            {
                var temp = x1;
                x1 = x2;
                x2 = x1 + temp;
                fibonacci.Add(x1);
                Thread.Sleep(10);
            }
            Console.WriteLine("Computed {0} values.", fibonacci.Count);
            Interlocked.Decrement(ref _taskCount);
        }
    }
}
