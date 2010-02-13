using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Developpez.Dotnet.Collections;

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

        public static IEnumerable<long> ProperDivisors(long n)
        {
            return InfiniteSequence(1)
                .TakeWhile(x => x <= n / 2)
                .Where(x => n % x == 0);
        }

        public static IEnumerable<BigInteger> Fibonacci()
        {
            BigInteger f0 = 0;
            BigInteger f1 = 1;
            yield return f0;
            yield return f1;
            while (true)
            {
                BigInteger f = f1 + f0;
                yield return f;
                f0 = f1;
                f1 = f;
            }

        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> source)
        {
            var list = source.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Count > 1)
                {
                    var permutations = GetPermutations(list.Where((item, idx) => idx != i));
                    foreach (var perm in permutations)
                    {
                        yield return new[] { list[i] }.Concat(perm);
                    }
                }
                else
                {
                    yield return new[] { list[0] };
                }
            }
        }
    }
}
