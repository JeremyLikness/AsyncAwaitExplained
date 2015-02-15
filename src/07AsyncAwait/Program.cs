using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _07AsyncAwait
{
    class Program
    {
        private const string PrideAndPrejudice = "http://www.gutenberg.org/cache/epub/1342/pg1342.txt";

        private static void Main()
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Going to read a book.");
                var client = new HttpClient();
                var book = await client.GetStringAsync(PrideAndPrejudice);
                Console.WriteLine("Read {0} bytes.", book.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("I can't take it, this happened: {0}!", ex.Message);
            }
        }
    }
}
