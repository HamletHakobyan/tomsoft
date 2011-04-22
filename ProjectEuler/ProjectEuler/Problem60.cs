using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    [Unsolved]
    public class Problem60 : IEulerProblem
    {
        public object GetSolution()
        {
            var sieve = new PrimeSieve();
            var primes = sieve.GetPrimes(9999).ToArray();

            var tmp = new[] { new long[0] } as IEnumerable<long[]>;
            for (int i = 0; i < 5; i++)
            {
                tmp = MakeSolutions(tmp, primes, sieve).ToArray();
            }
            
            return tmp.Select(v => v.Sum()).FirstOrDefault();
        }

        private static IEnumerable<long[]> MakeSolutions(IEnumerable<long[]> prevSolution, long[] primes, PrimeSieve sieve)
        {
            return from px in prevSolution
                   from p in primes
                   where p > px.DefaultIfEmpty().Max()
                   let x = px.Append(p).ToArray()
                   where IsSolution(x, sieve)
                   select x;
        }

        static bool IsSolution(long[] primes, PrimeSieve sieve)
        {
            var q =
                from a in primes
                from b in primes
                where a != b
                select long.Parse(a.ToString() + b.ToString());
            return q.All(sieve.IsPrime);
        }
    }
}
