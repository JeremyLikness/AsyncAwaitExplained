using System;
using System.Net.Http;
using System.Threading;

namespace _06Asynchronous
{
    class Program
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";

        static void Main()
        {
            Console.WriteLine("Going to read a site.");
            var client = new HttpClient();
            var task = client.GetStringAsync(WebSite);
            
            while (!task.IsCanceled && !task.IsCompleted)
            {
                Console.WriteLine("Doing stuff in the meantime...");
                Thread.Sleep(100);
            }

            if (task.IsCanceled)
            {
                Console.WriteLine("We had an oops!");
            }
            else
            {
                var site = task.Result;
                Console.WriteLine("Downloaded {0} bytes.", site.Length);
            }

            Console.ReadLine();
        }
    }
}
