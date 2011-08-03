using System;
using System.Text;
using PasteBinSharp;

namespace ConsoleTests
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string apiKey = "ac1d4a832f23db1de8e57ed4fcc5a184";
            var client = new PasteBinClient(apiKey);
            const string userName = "tomlev";
            Console.Write("Password: ");
            string password = ReadPassword();
            client.Login(userName, password);

            TestListing(client);

            Console.ReadLine();
        }

        private static void TestUserDetails(PasteBinClient client)
        {
            var details = client.GetUserDetails();
            details.Dump();
        }

        static void Dump<T>(this T obj)
        {
            Console.WriteLine("*** {0} ***", typeof(T).FullName);
            foreach (var prop in typeof(T).GetProperties())
            {
                Console.WriteLine("{0}: {1}", prop.Name, prop.GetValue(obj, null));
            }
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
                entry.Dump();
            }
        }

        static string ReadPassword()
        {
            ConsoleKeyInfo cki = default(ConsoleKeyInfo);
            StringBuilder sb = new StringBuilder();
            while (cki.Key != ConsoleKey.Enter)
            {
                cki = Console.ReadKey(true);
                if (!char.IsControl(cki.KeyChar))
                {
                    Console.Write("*");
                    sb.Append(cki.KeyChar);
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
