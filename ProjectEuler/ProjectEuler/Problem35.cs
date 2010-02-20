using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem35 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            PrimeSieve sieve = new PrimeSieve();

            return sieve.GetPrimes(999999)
                .Where(p => p.GetRotations().All(r => sieve.IsPrime(r)))
                .Count();
        }

        #endregion
    }
}
