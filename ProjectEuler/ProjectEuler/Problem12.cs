using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem12 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            // Marche pas... (ou trop long en tous cas)
            return TriangleNumbers().First(n => GetDivisors(n).Count() >= 500);
        }

        #endregion

        public IEnumerable<long> TriangleNumbers()
        {
            return Util.InfiniteSequence(1).SelectAggregate(0L, (i, prev) => prev + i);
        }

        public IEnumerable<long> GetDivisors(long n)
        {
            int s = (n % 2 == 0) ? 1 : 2;
            for (long i = 1; i <= n / 2; i += s)
            {
                if (n % i == 0)
                    yield return i;
            }
            yield return n;
        }
    }
}
