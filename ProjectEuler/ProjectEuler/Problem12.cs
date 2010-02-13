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
            // return TriangleNumbers().First(n => GetDivisors(n).Count() >= 500);
            
            // Toujours trop long...
            return Util.InfiniteSequence(1)
                .Select(n => n * (n + 1) / 2)
                .Where(n => GetNumberOfDivisors(n) >= 500)
                .First();
        }

        #endregion

        public IEnumerable<long> TriangleNumbers()
        {
            return Util.InfiniteSequence(1).SelectAggregate(0L, (i, prev) => prev + i);
        }

        public long GetNumberOfDivisors(long n)
        {
            long nDiv = 1;
            int s = (n % 2 == 0) ? 1 : 2;
            for (long i = 1; i <= n / 2; i += s)
            {
                if (n % i == 0)
                    nDiv++;
            }
            return nDiv;
        }
    }
}
