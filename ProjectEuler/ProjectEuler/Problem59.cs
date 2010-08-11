using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectEuler
{
    public class Problem59 : IEulerProblem
    {
        public object GetSolution()
        {
            var inputBytes = GetInputBytes();

            return null;
        }

        static byte[] GetInputBytes()
        {
            string inputFile = @"Data\p59_cipher1.txt";
            string content = File.ReadAllText(inputFile).Trim();
            return content.Split(',').Select(byte.Parse).ToArray();
        }
    }
}
