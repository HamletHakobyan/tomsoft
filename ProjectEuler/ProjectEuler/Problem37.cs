using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem37 : IEulerProblem
    {
        #region IEulerProblem Members

        PrimeSieve _sieve;

        public object GetSolution()
        {
            _sieve = new PrimeSieve();
            return _sieve.GetPrimes(1000000).SkipWhile(n => n < 10).Where(n => IsTruncatable(n)).Take(11).Sum();
        }

        private bool IsTruncatable(long n)
        {
            // Right
            long tmp = n;
            while (tmp > 10)
            {
                tmp /= 10;
                if (!_sieve.IsPrime(tmp))
                    return false;
            }

            // Left
            tmp = n;
            while (tmp > 10)
            {
                tmp = long.Parse(tmp.ToString().Substring(1));
                if (!_sieve.IsPrime(tmp))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
