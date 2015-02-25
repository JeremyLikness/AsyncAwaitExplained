using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _07AsyncAwait
{
    class Program
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";

        private static void Main()
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Going to read a website.");
                var client = new HttpClient();
                var site = await client.GetStringAsync(WebSite);
                Console.WriteLine("Read {0} bytes.", site.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("I can't take it, this happened: {0}!", ex.Message);
            }
        }
    }
}
