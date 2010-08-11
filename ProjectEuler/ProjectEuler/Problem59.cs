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

        static IEnumerable<byte[]> CandidateKeys()
        {
            var letterBytes = Enumerable.Range(97, 26).Select(i => (byte)i);
            return
                from a in letterBytes
                from b in letterBytes
                from c in letterBytes
                select new[] { a, b, c };
        }

        static byte[] GetInputBytes()
        {
            string inputFile = @"Data\p59_cipher1.txt";
            string content = File.ReadAllText(inputFile).Trim();
            return content.Split(',').Select(byte.Parse).ToArray();
        }
    }
}
