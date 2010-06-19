using System.Diagnostics;
using System.IO;
using PkgMaker.Model;

namespace PkgMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener { UseColors = true });

            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            var builder = new PackageBuilder();
            foreach (var inputFile in args)
            {
                string basePath = Path.GetDirectoryName(Path.GetFullPath(inputFile));
                Package pkg = Package.FromFile(inputFile);
                builder.BuildPackage(pkg, basePath);
            }
        }

        static void PrintUsage()
        {
            Trace.WriteLine("Usage:");
            Trace.WriteLine("  PkgMaker <package filename>");
        }
    }
}
