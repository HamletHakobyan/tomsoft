using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public static class Util
    {
        public static IEnumerable<long> InfiniteSequence(long start)
        {
            long i = start;
            while (true)
            {
                yield return i;
                i++;
            }
        }

        public static IEnumerable<long> InfiniteSequence()
        {
            return InfiniteSequence(0);
        }

        public static IEnumerable<long> PrimeNumbers()
        {
            List<long> primes = new List<long>();
            long i = 2;
            while (true)
            {
                long sqrt = (long)Math.Floor(Math.Sqrt(i));
                bool hasDivisors = false;
                foreach (long p in primes.Where(x => x <= sqrt))
                {
                    if (i % p == 0)
                    {
                        hasDivisors = true;
                        break;
                    }
                }
                if (!hasDivisors)
                {
                    primes.Add(i);
                    yield return i;
                }
                i++;
            }
        }

        public static IEnumerable<long> PrimeDivisors(long n)
        {
            long sqrt = (long)Math.Floor(Math.Sqrt(n));
            return PrimeNumbers()
                .TakeWhile(x => x <= sqrt)
                .Where(x => n % x == 0);
        }
    }
}
