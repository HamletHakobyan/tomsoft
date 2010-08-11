using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem51 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            PrimeSieve sieve = new PrimeSieve();
            Func<long, bool> predicate = n => IsPartOfPrimesFamily(n, sieve, 8);
            return sieve.GetPrimes(1000000)
                .Where(predicate)
                .First();
        }

        #endregion

        private bool IsPartOfPrimesFamily(long n, PrimeSieve sieve, int count)
        {
            return GetSubstitutions(n)
                    .Where(s => sieve.IsPrime(s))
                    .Count() >= count;
        }

        private IEnumerable<long> GetSubstitutions(long n)
        {
            // TODO

            var digits = n.GetDigits().ToArray();
            for (int nd = 1; nd <= digits.Length; nd++)
            {
                for (int d = 0; d < 10; d++)
                {
                    for (int p = 0; p <= digits.Length - nd; p++)
                    {
                        int prev = p;

                    }
                }
            }

            yield break;
        }

        private T[] CopyArray<T>(T[] source)
        {
            T[] copy = new T[source.Length];
            Array.Copy(source, 0, copy, 0, source.Length);
            return copy;
        }
    }
}
