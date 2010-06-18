using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PkgMaker.Model;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace PkgMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var cArgs = CommandLineArgs.Parse(args);
            if (cArgs == null)
                return;

            string basePath = Path.GetDirectoryName(Path.GetFullPath(cArgs.PackageFileName));
            Package pkg = Package.FromFile(cArgs.PackageFileName);
            BuildPackage(pkg, basePath);
        }

        private static void BuildPackage(Package pkg, string basePath)
        {
            var entries = new List<PackageEntry>();
            PopulateEntries(pkg, basePath, entries);
            string zipFileName = Path.Combine(basePath, pkg.OutputFileName);
            using (var zipFile = new ZipFile(zipFileName))
            {
                zipFile.BeginUpdate();
                foreach (var entry in entries)
                {
                    if (entry.IsDirectory)
                    {
                        zipFile.AddDirectory(entry.EntryName);
                    }
                    else
                    {
                        zipFile.Add(entry.SourcePath, entry.EntryName);
                    }
                }
                zipFile.CommitUpdate();
            }
        }

        private static void PopulateEntries(Package pkg, string basePath, List<PackageEntry> entries)
        {
            throw new NotImplementedException();
        }

        private class PackageEntry
        {
            public string SourcePath { get; set; }
            public string EntryName { get; set; }
            public bool IsDirectory { get; set; }
        }

        class CommandLineArgs
        {
            public string PackageFileName { get; set; }

            public static CommandLineArgs Parse(string[] args)
            {
                if (args.Length == 0)
                {
                    PrintUsage();
                    return null;
                }

                var cArgs = new CommandLineArgs();
                cArgs.PackageFileName = args[0];
                return cArgs;
            }

            private static void PrintUsage()
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  PkgMaker <package filename>");
            }
        }
    }
}
