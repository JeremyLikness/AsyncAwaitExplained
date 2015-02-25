using System;
using System.Net.Http;

namespace _08NoAsyncAwait
{
    class Program
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";

        private static void Main()
        {
            Console.WriteLine("Going to read a site.");
            var client = new HttpClient();
            var task = client.GetStringAsync(WebSite);
            task.Wait();
            if (task.IsCanceled)
            {
                Console.WriteLine("Didn't work out.");
            }
            else
            {
                var site = task.Result;
                Console.WriteLine("Read {0} bytes.", site.Length);
            }
            Console.ReadLine();
        }        
    }
}
