using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem27 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var sieve = new PrimeSieve();

            var ab =
                from a in Enumerable.Range(-999, 1999)
                from b in Enumerable.Range(-999, 1999)
                select new { a, b };

            long nmax = 0;
            long amax = 0;
            long bmax = 0;
            foreach (var terms in ab)
            {
                int nprimes =
                    Util.InfiniteSequence()
                    .Select(n => n * n + terms.a * n + terms.b)
                    .TakeWhile(p => sieve.IsPrime(p))
                    .Count();
                if (nprimes > nmax)
                {
                    nmax = nprimes;
                    amax = terms.a;
                    bmax = terms.b;
                }
            }
            Console.WriteLine("a = {0}", amax);
            Console.WriteLine("b = {0}", bmax);
            Console.WriteLine("{0} primes", nmax);
            return amax * bmax;
        }

        #endregion
    }
}
