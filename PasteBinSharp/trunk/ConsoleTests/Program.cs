using System;
using PasteBinSharp;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            const string apiKey = "ac1d4a832f23db1de8e57ed4fcc5a184";
            var client = new PasteBinClient(apiKey);
            const string userName = "tomlev";
            Console.Write("Password: ");
            string password = Console.ReadLine();
            client.Login(userName, password);

            TestListing(client);
            Console.ReadLine();
        }

        static void TestPasteAndDelete(PasteBinClient client)
        {
            var entry = new PasteBinEntry
            {
                Title = "test PasteBin client",
                Text = "Console.WriteLine(\"Hello PasteBin\");",
                Expiration = PasteBinExpiration.OneDay,
                Private = true,
                Format = "csharp"
            };
            string url = client.Paste(entry);
            Console.WriteLine(url);

            Console.Write("Press enter to delete");
            Console.ReadLine();
            client.Delete(entry.Key);
            Console.WriteLine("Paste removed");
        }

        static void TestListing(PasteBinClient client)
        {
            foreach (var entry in client.GetEntries())
            {
                Console.WriteLine(entry.Title);
            }
        }
    }
}
