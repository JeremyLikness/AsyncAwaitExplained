using System;
using System.Net.Http;
using System.Threading;

namespace _06Asynchronous
{
    class Program
    {
        private const string PrideAndPrejudice = "http://www.gutenberg.org/cache/epub/1342/pg1342.txt";

        static void Main()
        {
            Console.WriteLine("Going to read a book.");
            var client = new HttpClient();
            var task = client.GetStringAsync(PrideAndPrejudice);
            
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
                var book = task.Result;
                Console.WriteLine("Downloaded {0} bytes.", book.Length);
            }

            Console.ReadLine();
        }
    }
}
