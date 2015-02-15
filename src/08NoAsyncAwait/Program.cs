using System;
using System.Net.Http;

namespace _08NoAsyncAwait
{
    class Program
    {
        private const string PrideAndPrejudice = "http://www.gutenberg.org/cache/epub/1342/pg1342.txt";

        private static void Main()
        {
            Console.WriteLine("Going to read a book.");
            var client = new HttpClient();
            var task = client.GetStringAsync(PrideAndPrejudice);
            task.Wait();
            if (task.IsCanceled)
            {
                Console.WriteLine("Didn't work out.");
            }
            else
            {
                var book = task.Result;
                Console.WriteLine("Read {0} bytes.", book.Length);
            }
            Console.ReadLine();
        }        
    }
}
