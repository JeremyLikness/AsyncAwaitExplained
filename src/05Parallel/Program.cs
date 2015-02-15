using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _05Parallel
{
    class Program
    {
        const int Max = int.MaxValue / 2;

        static void Main()
        {
            Console.WriteLine("Main program thread.");
            Console.WriteLine("Queuing up a bunch of tasks.");
            var tasks = Enumerable.Repeat<Action>(Sequence, 100).ToArray();
            Parallel.Invoke(tasks);
            Console.WriteLine("We're done.");
            Console.ReadLine();
        }

        private static void Sequence()
        {
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
        }
    }
}
