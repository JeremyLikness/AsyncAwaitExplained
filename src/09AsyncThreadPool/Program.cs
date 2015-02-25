using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _09AsyncThreadPool
{
    class Program
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";
        private const int ThreadsToRun = 200;

        static void Main()
        {
            Console.WriteLine("Going to read a book.");
            var client = new HttpClient();
            var task = client.GetStringAsync(WebSite);

            while (!task.IsCanceled && !task.IsCompleted)
            {
                Console.WriteLine("Doing stuff in the meantime...");
                Thread.Sleep(1000);
            }

            if (task.IsCanceled)
            {
                Console.WriteLine("We had an oops!");
            }
            else
            {
                var book = task.Result;
                Console.WriteLine("Downloaded {0} bytes.", book.Length);
            }

            Console.WriteLine("Hit ENTER for {0} Threads.", ThreadsToRun);
            Console.ReadLine();

            Threads();

            Console.WriteLine("Hit ENTER for {0} Tasks.", ThreadsToRun);
            Console.ReadLine();

            Tasks();
            Console.ReadLine();
        }

        private static int _threadCount = ThreadsToRun;
        private static long _totalLength;

        private static void Tasks()
        {
            var tasks = new Task<int>[ThreadsToRun];
            var start = DateTime.Now;
            for (var x = 0; x < ThreadsToRun; x += 1)
            {
                tasks[x] = Task.Run(async () => await TaskRead());
                Thread.Sleep(10);
            }
            Console.WriteLine("Waiting for tasks to finish...");
            Task.WaitAll(tasks);
            var end = DateTime.Now;
            Console.WriteLine("We're done: {0} in {1}", 
                tasks.Sum(t => t.Result), end.Ticks - start.Ticks);
        }

        private static async Task<int> TaskRead()
        {
            var client = new HttpClient();
            var str = await client.GetStringAsync(WebSite);
            return str.Length;
        }

        private static void ThreadRead()
        {
            var client = new HttpClient();
            var strTask = client.GetStringAsync(WebSite);
            strTask.Wait();
            var str = strTask.Result;
            Interlocked.Add(ref _totalLength, str.Length);
            Interlocked.Decrement(ref _threadCount);
        }

        private static void Threads()
        {
            var start = DateTime.Now;
            for (var x = 0; x < ThreadsToRun; x += 1)
            {
                var newThread = new Thread(ThreadRead);
                newThread.Start();
                Thread.Sleep(10);
            }
            Console.WriteLine("Waiting for threads to finish...");
            while (_threadCount > 0)
            {
                Thread.Sleep(10);
            }
            var end = DateTime.Now;
            Console.WriteLine("We're done: {0} in {1}", _totalLength, end.Ticks - start.Ticks);
        }
    }
}
