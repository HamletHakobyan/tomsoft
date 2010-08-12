using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectEuler
{
    public class Problem79 : IEulerProblem
    {

        public object GetSolution()
        {
            // Assumption : no duplicates

            var attempts = GetAttempts();
            var distinctDigits = attempts.SelectMany(a => a.GetDigits()).Distinct().ToArray();
            var permutations = Util.GetPermutations(distinctDigits)
                                   .Select(p => p.ToArray().MakeInt64())
                                   .ToArray();
            var solutions =
                permutations
                    .Where(p => attempts.All(a => IsMatch(p, a)));

            return solutions.FirstOrDefault();
        }

        static long[] GetAttempts()
        {
            return File.ReadAllLines(@"Data\p79_keylog.txt")
                       .Select(long.Parse)
                       .ToArray();
        }

        static bool IsMatch(long n, long extract)
        {
            return IsMatch(n.GetDigits().ToArray(), extract.GetDigits().ToArray());
        }

        static bool IsMatch(int[] nDigits, int[] extractDigits)
        {
            int pn = 0;
            for (int px = 0; px < extractDigits.Length; px++)
            {
                while (pn < nDigits.Length && nDigits[pn] != extractDigits[px])
                {
                    pn++;
                }
                if (pn >= nDigits.Length)
                    return false;
                pn++;
            }
            return true;
        }
    }
}
