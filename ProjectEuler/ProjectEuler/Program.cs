using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            while (true)
            {
                Console.Write("Enter problem number, or 'q' to exit : ");
                line = Console.ReadLine();
                if (line.IsNullOrEmpty())
                    continue;
                if (line.ToLower() == "q")
                    break;
                int problemNumber;
                if (int.TryParse(line, out problemNumber))
                {
                    RunProblem(problemNumber);
                }
                else
                {
                    WriteError("Incorrect number");
                }
            }
        }

        private static void WriteError(string text, params object[] args)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text, args);
            Console.ForegroundColor = originalColor;
        }

        private static void RunProblem(int problemNumber)
        {
            Console.WriteLine();
            string className = "ProjectEuler.Problem" + problemNumber;
            Type type = Type.GetType(className);
            if (type != null)
            {
                IEulerProblem problem = null;
                try
                {
                    object o = Activator.CreateInstance(type);
                    problem = o as IEulerProblem;
                }
                catch(Exception ex)
                {
                    WriteError("Unable to create instance of {0}\n{1}", className, ex);
                }

                if (problem != null)
                {
                    try
                    {
                        Console.WriteLine("Running problem {0}...", problemNumber);
                        Stopwatch sw = new Stopwatch();
                        var originalColor = Console.ForegroundColor;
                        object result = null;
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            sw.Start();
                            result = problem.GetSolution();
                        }
                        finally
                        {
                            sw.Stop();
                            Console.ForegroundColor = originalColor;
                        }
                        Console.WriteLine("The solution of problem {0} is :\n{1}", problemNumber, result);
                        Console.WriteLine("Elapsed time : {0}", sw.Elapsed);
                    }
                    catch (Exception ex)
                    {
                        WriteError("Error executing problem {0}\n{1}", problemNumber, ex);
                    }
                }
                else
                {
                    WriteError("Type {0} doesn't implement IEulerProblem", className);
                }
            }
            else
            {
                WriteError("Problem {0} is not implemented", problemNumber);
            }
            Console.WriteLine();
        }
    }
}
