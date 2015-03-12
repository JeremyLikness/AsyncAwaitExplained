using System;
using System.Collections.Generic;
using System.Threading;

namespace _01Threads
{
    class Program
    {
        const int Max = int.MaxValue / 2;
            
        static void Main()
        {
            Console.WriteLine("Main program thread {0}.", Thread.CurrentThread.ManagedThreadId);
            var thread = new Thread(Sequence);
            thread.Start();
            Console.WriteLine("Going to sleep on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("Waiting for thread to stop.");
            thread.Join();
            Console.WriteLine("Done on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }

        private static void Sequence(Object state)
        {
            var fibonacci = new List<int>();
            Console.WriteLine("Computing sequences on thread {0}...", Thread.CurrentThread.ManagedThreadId);
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
        }
    }
}
