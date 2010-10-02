using System;
using System.Data.Objects;
using System.Linq;
using Mediatek.Data;
using Mediatek.Data.EntityFramework;
using Mediatek.Entities;

namespace Tests
{
    class Program
    {
        static void Main()
        {
            const string providerName = "System.Data.SqlServerCe.3.5";
            string connectionString = Properties.Settings.Default.ConnectionString;
            using (var context = MediatekContext.GetContext(providerName, connectionString))
            {
                TestMultiThreadLazyLoading(context);
            }
            Console.ReadLine();
        }

        private static void TestMultiThreadLazyLoading(IEntityRepository context)
        {
            foreach (var m in context.Medias)
            {
                Console.WriteLine(m.Title);
                Console.WriteLine("{0} : {1}", m.Title, m.Picture.Data.Bytes.Length);
/*                var copy = m;
                ThreadPool.QueueUserWorkItem(
                    state =>
                    {
                        Console.WriteLine("{0} : {1}", copy.Title, copy.Picture.Data.Bytes.Length);
                    });*/
            }
        }

// ReSharper disable UnusedMember.Local
        private static void Test2(IEntityRepository context)
// ReSharper restore UnusedMember.Local
        {
            var query = from m in context.Medias.OfType<Movie>()
                        where m.Year.HasValue && m.Year > 2005
                        select new
                        {
                            m.Title,
                            Directors = m.Contributions.Where(c => c.Role.Name == "Director"),
                            m.Year
                        };

            var oq = (ObjectQuery)query;
            Console.WriteLine(oq.ToTraceString());
        }

// ReSharper disable UnusedMember.Local
        private static void Test1(IEntityRepository context)
// ReSharper restore UnusedMember.Local
        {
            var movies = context.Medias
                                .OfType<Movie>();

            Console.WriteLine("**********************************");
            Console.WriteLine("* Entity SQL *");
            Console.WriteLine("**********************************");
            Console.WriteLine(((ObjectQuery)movies).CommandText);
            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("* SQL *");
            Console.WriteLine("**********************************");
            Console.WriteLine(((ObjectQuery)movies).ToTraceString());
            Console.WriteLine("**********************************");
            Console.WriteLine();

            foreach (var m in movies)
            {
                Console.WriteLine("{0} ({1})", m.Title, m.Year);
                Console.WriteLine("  * Contributions");
                foreach (var contribution in m.Contributions)
                {
                    Console.WriteLine("    - {0} : {1}", contribution.Role.Name, contribution.Person.DisplayName);
                }
                Console.WriteLine("  * Loans");
                foreach (var loan in m.Loans.OrderBy(l => l.LoanDate))
                {
                    if (loan.ReturnDate.HasValue)
                    {
                        Console.WriteLine("    - Lent to {0} from {1:d} to {2:d}", loan.Person.DisplayName, loan.LoanDate, loan.ReturnDate);
                    }
                    else
                    {
                        var consoleColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("    - Lent to {0} since {1:d}", loan.Person.DisplayName, loan.LoanDate);
                        Console.ForegroundColor = consoleColor;
                    }
                }
                if (Console.ReadLine() == "exit")
                    break;
            }
        }
    }
}
