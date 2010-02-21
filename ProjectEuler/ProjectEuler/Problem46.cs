using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem46 : IEulerProblem
    {
        #region IEulerProblem Members

        PrimeSieve _sieve = new PrimeSieve();
        public object GetSolution()
        {
            return Util.InfiniteSequence(1)
                    .Select(n => n * 2 + 1) // Odd
                    .Where(n => !_sieve.IsPrime(n)) // Composite (not prime)
                    .FirstOrDefault(n => !VerifiesPredicate(n));
        }

        #endregion

        private bool VerifiesPredicate(long n)
        {
            foreach (var prime in _sieve.GetPrimes(n).Reverse())
            {
                long rem = n - prime;
                double sqrt = Math.Sqrt(rem / 2.0);
                if (sqrt == (long)sqrt)
                    return true;
            }
            return false;
        }
    }
}
