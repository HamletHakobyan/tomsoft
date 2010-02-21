using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem50 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var primes = Util.PrimeNumbers().TakeWhile(p => p < 1000000).ToArray();

            var q =
                from p in primes
                let terms = MaxNumberOfPrimeTerms(p, primes)
                where terms != 0
                select new { Prime = p, Terms = terms };

            var max = q.WithMax(p => p.Terms);
            return string.Format("{0} ({1} terms)", max.Prime, max.Terms);
        }

        #endregion

        private int MaxNumberOfPrimeTerms(long total, long[] primes)
        {
            int longest = 1;
            for (int start = 0; start < primes.Length && primes[start] <= total / 2; start++)
            {
                long sum = 0;
                int i = 0;
                while (sum < total && start + i < primes.Length)
                {
                    sum += primes[start + i];
                    i++;
                }
                if (sum == total && i > longest)
                {
                    longest = i;
                }
            }
            return longest;
        }
    }
}
