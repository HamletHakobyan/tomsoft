using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem47 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            int lenSeq = 4;
            var numFact = new Func<long, int>(NumberOfDistinctPrimeFactors).AsSlideCached(lenSeq);
            return Util.InfiniteSequence()
                    .Where(n => Enumerable.Range((int)n, lenSeq).All(x => numFact(x) == lenSeq))
                    .First();
        }

        #endregion

        PrimeSieve _sieve = new PrimeSieve();
        private int NumberOfDistinctPrimeFactors(long n)
        {
            return _sieve.GetPrimes(n / 2)
                .Where(x => n % x == 0)
                .Count();
        }
    }
}
