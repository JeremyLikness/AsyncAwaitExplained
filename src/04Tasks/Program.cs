using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _04Tasks
{
    class Program
    {
        const int Max = int.MaxValue / 2;
        
        static void Main()
        {
            Console.WriteLine("Main program thread.");
            Console.WriteLine("Queuing up a bunch of tasks.");
            var tasks = new Task[100];

            for (var x = 0; x < 100; x += 1)
            {
                tasks[x] = Task<int>.Factory.StartNew(Sequence);
                Thread.Sleep(10);
            }
            Console.WriteLine("Waiting for tasks to finish...");
            Task.WaitAll(tasks);
            foreach (var task in tasks.Cast<Task<int>>())
            {
                Console.WriteLine("Task #{0} Computed {1} values", task.Id, task.Result);
            }
            Console.WriteLine("We're done.");
            Console.ReadLine();
        }

        private static int Sequence()
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
            return fibonacci.Count;
        }
    }
}
