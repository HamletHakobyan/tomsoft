using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PkgMaker.Core;

namespace PkgMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener { UseColors = true });
            var cArgs = CommandLineArgs.Parse(args);
            if (cArgs == null)
            {
                PrintUsage();
                return;
            }

            var builder = new PackageBuilder
            {
                CreateFileList = cArgs.CreateFileList
            };
            foreach (var inputFile in cArgs.InputFiles)
            {
                string basePath = Path.GetDirectoryName(Path.GetFullPath(inputFile));
                Package pkg = Package.FromFile(inputFile);
                builder.BuildPackage(pkg, basePath);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  PkgMaker [/filelist] inputFile1 [inputFile2, inputFile3 ...]");
            Console.WriteLine("    inputFileX    Package definition file");
            Console.WriteLine("    /filelist     Write list of files in the package in a file");
            Console.WriteLine("");
        }

        private class CommandLineArgs
        {
            protected CommandLineArgs()
            {
                InputFiles = new List<string>();
            }

            public List<string> InputFiles { get; protected set; }
            public bool CreateFileList { get; protected set; }

            public static CommandLineArgs Parse(string[] args)
            {
                CommandLineArgs cArgs = new CommandLineArgs();
                foreach (var arg in args)
                {
                    if (arg.StartsWith("/"))
                    {
                        switch (arg.ToLower())
                        {
                            case "/filelist":
                                cArgs.CreateFileList = true;
                                break;
                            default:
                                Console.WriteLine("Error : unknown switch '{0}'", arg);
                                return null;
                        }
                    }
                    else
                    {
                        cArgs.InputFiles.Add(arg);
                    }
                }

                if (cArgs.InputFiles.Count == 0)
                {
                    Console.WriteLine("Error : no input files");
                    return null;
                }

                return cArgs;
            }
        }
    }
}
