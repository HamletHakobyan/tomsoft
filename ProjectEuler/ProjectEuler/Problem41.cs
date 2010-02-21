using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectEuler
{
    class Problem41 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var sieve = new PrimeSieve();

            var solutions =
                Enumerable.Range(1, 9)
                .SelectMany(
                        n => Enumerable.Range(1, n)
                                .GetPermutations()
                                .Select(digits => digits.MakeInt32()))
                .Where(n => sieve.IsPrime(n, false));

            return solutions.Max();
        }

        #endregion
    }
}
