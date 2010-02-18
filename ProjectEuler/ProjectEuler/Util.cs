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
                foreach (long p in primes.TakeWhile(x => x <= sqrt))
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

        public static IEnumerable<BigInteger> FibonacciBig()
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

        public static IEnumerable<long> Fibonacci()
        {
            long f0 = 0;
            long f1 = 1;
            yield return f0;
            yield return f1;
            while (true)
            {
                long f = f1 + f0;
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

        /// <summary>
        /// Returns the greatest common divisor of a and b
        /// </summary>
        /// <param name="a">Number a</param>
        /// <param name="b">Number b</param>
        /// <returns>The GCD of a and b</returns>
        public static long GCD(long a, long b)
        {
            long remainder = a % b;
            while (remainder != 0)
            {
                a = b;
                b = remainder;
                remainder = a % b;
            }
            return b;
        }

        public static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }

        public static long LCM(params long[] terms)
        {
            long lcm = 1;
            for (int i = 0; i < terms.Length; i++)
            {
                lcm = LCM(lcm, terms[i]);
            }
            return lcm;
        }

        public static bool IsTerminatingDecimal(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("The denominator can't be 0", "denominator");

            // 0 / x = 0, which is terminating
            if (numerator == 0)
                return true;

            // Reduce fraction
            long gcd = Util.GCD(numerator, denominator);
            if (gcd > 1)
            {
                numerator /= gcd;
                denominator /= gcd;
            }

            // Try to factorize denominator to (2^n * 5^m)
            while (denominator % 2 == 0) denominator /= 2;
            while (denominator % 5 == 0) denominator /= 5;
            
            // If factorization succeeded, denominator is now 1
            return denominator == 1;
        }

        public static int GetDecimalCycleLength(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("The denominator can't be 0", "denominator");

            if (numerator == 0)
                return 0;

            long q;
            long r = numerator % denominator;
            var remainders = new List<long>();
            
            // Note: since numerator/denominator is a rational number,
            // it will either terminate or have a cycle
            // Only extra precaution : stop before reaching int.MaxValue decimals

            while (r != 0 && remainders.Count < int.MaxValue)
            {
                numerator = r * 10;
                q = numerator / denominator;
                r = numerator % denominator;
                int last = remainders.LastIndexOf(r);
                if (last != -1)
                {
                    return (remainders.Count - last);
                }
                remainders.Add(r);
            }
            if (r == 0)
                return 0;
            else // if (remainders.Count >= int.MaxValue)
                return -1;
        }

        public static long GetAlphaValue(string s)
        {
            return s.Select(c => (long)c - 64).Sum();
        }

        public static string Join(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }
    }
}
