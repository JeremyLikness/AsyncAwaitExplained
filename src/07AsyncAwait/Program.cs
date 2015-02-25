using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _07AsyncAwait
{
    class Program
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";

        private static void Main()
        {
            Console.WriteLine("Starting MainAsync from thread {0}.", Thread.CurrentThread.ManagedThreadId);
            MainAsync().Wait();
            Console.WriteLine("Returned from MainAsync to thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Hit ENTER to view re-entrant method demonstration.");
            Console.ReadLine();
            Console.WriteLine("Demonstrating re-entrant method.");
            Run();
        }

        private static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Going to read a website from thread {0}.", Thread.CurrentThread.ManagedThreadId);
                var client = new HttpClient();
                var site = await client.GetStringAsync(WebSite);
                Console.WriteLine("Read {0} bytes from thread {1}.", site.Length, Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("I can't take it, this happened: {0}!", ex.Message);
            }
        }

        private static void Run()
        {
            foreach (var item in ReentrantMethod())
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        private static IEnumerable<int> ReentrantMethod()
        {
            Console.WriteLine("Entered method.");
            var x1 = 0;
            var x2 = 1;
            const int max = int.MaxValue / 2;
            while (x1 < max)
            {
                yield return x2; // this will exit the method 
                Console.WriteLine("Re-entered method with state preserved.");
                var temp = x1;
                x1 = x2;
                x2 = x1 + temp;
            }
            Console.WriteLine("Exited method for final time.");
        }
    }
}
