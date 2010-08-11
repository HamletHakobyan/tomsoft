using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Developpez.Dotnet;
using System.Text.RegularExpressions;

namespace ProjectEuler
{
    public class Problem59 : IEulerProblem
    {
        public object GetSolution()
        {
            var inputBytes = GetInputBytes();
            var mostCommonWords = GetCommonWords();

            int maxCommonWords = 0;
            byte[] bestKey = null;
            string bestDecodedText = null;
            long bestSum = 0;

            foreach (var key in CandidateKeys())
            {
                var decodedBytes = Xor(inputBytes, key);
                var decodedText = Encoding.ASCII.GetString(decodedBytes);
                int nCommonWords =
                    Regex.Split(decodedText, "[^a-zA-Z]")
                         .Intersect(mostCommonWords, StringComparer.InvariantCultureIgnoreCase)
                         .Count();
                if (nCommonWords > maxCommonWords)
                {
                    maxCommonWords = nCommonWords;
                    bestKey = key;
                    bestDecodedText = decodedText;
                    bestSum = decodedBytes.Sum(b => b);
                }
            }
            Console.WriteLine(Encoding.ASCII.GetString(bestKey));
            Console.WriteLine(bestDecodedText);
            return bestSum;
        }

        private string[] GetCommonWords()
        {
            return File.ReadAllLines(@"Data\most_frequent_english_words.txt");
        }

        private static byte[] Xor(byte[] input, byte[] key)
        {
            var output = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte)(input[i] ^ key[i % key.Length]);
            }
            return output;
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
