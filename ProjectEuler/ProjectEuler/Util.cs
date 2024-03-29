﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Developpez.Dotnet.Collections;
using Developpez.Dotnet;

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

        public static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;
            if (n < 9)
                return true;
            if (n % 3 == 0)
                return false;
            double sqrt = Math.Sqrt(n);
            int f = 5;
            while (f <= sqrt)
            {
                if (n % f == 0)
                    return false;
                if (n % (f + 2) == 0)
                    return false;
                f += 6;
            }
            return true;
        }

        public static bool IsPrime(long n)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;
            if (n < 9)
                return true;
            if (n % 3 == 0)
                return false;
            double sqrt = Math.Sqrt(n);
            long f = 5;
            while (f <= sqrt)
            {
                if (n % f == 0)
                    return false;
                if (n % (f + 2) == 0)
                    return false;
                f += 6;
            }
            return true;
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

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> source)
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

        public static bool AreCoPrime(long a, long b)
        {
            return GCD(a, b) == 1;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger remainder = a % b;
            while (remainder != 0)
            {
                a = b;
                b = remainder;
                remainder = a % b;
            }
            return b;
        }

        public static BigInteger LCM(BigInteger a, BigInteger b)
        {
            return a * b / GCD(a, b);
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

        static Func<long, long> _factorialCached;
        public static long Factorial(long n)
        {
            if (_factorialCached == null)
            {
                Func<long, long> factorial = x => (x < 2) ? 1 : x * _factorialCached(x - 1);
                _factorialCached = factorial.AsCached();
            }
            return _factorialCached(n);
        }

        static Func<long, BigInteger> _factorialBigCached;
        public static BigInteger FactorialBig(long n)
        {
            if (_factorialBigCached == null)
            {
                Func<long, BigInteger> factorialBig = x => (x < 2) ? 1 : x * _factorialBigCached(x - 1);
                _factorialBigCached = factorialBig.AsCached();
            }
            return _factorialBigCached(n);
        }

        public static IEnumerable<int> GetDigits(this int n)
        {
            return n.GetDigitsFromEnd().Reverse();
        }

        public static IEnumerable<int> GetDigits(this int n, int inBase)
        {
            return n.GetDigitsFromEnd(inBase).Reverse();
        }

        public static IEnumerable<int> GetDigitsFromEnd(this int n)
        {
            return n.GetDigitsFromEnd(10);
        }

        public static IEnumerable<int> GetDigitsFromEnd(this int n, int inBase)
        {
            do
            {
                int rem;
                n = Math.DivRem(n, inBase, out rem);
                yield return rem;
            } while (n != 0);
        }

        public static IEnumerable<int> GetDigits(this long n)
        {
            return n.GetDigitsFromEnd().Reverse();
        }

        public static IEnumerable<int> GetDigits(this long n, int inBase)
        {
            return n.GetDigitsFromEnd(inBase).Reverse();
        }

        public static IEnumerable<int> GetDigitsFromEnd(this long n)
        {
            return n.GetDigitsFromEnd(10);
        }

        public static IEnumerable<int> GetDigitsFromEnd(this long n, int inBase)
        {
            do
            {
                long rem;
                n = Math.DivRem(n, inBase, out rem);
                yield return (int)rem;
            } while (n != 0);
        }

        public static int MakeInt32(this IEnumerable<int> digits)
        {
            int n = 0;
            foreach (var d in digits)
            {
                n *= 10;
                n += d;
            }
            return n;
        }

        public static long MakeInt64(this IEnumerable<int> digits)
        {
            long n = 0;
            foreach (var d in digits)
            {
                n *= 10;
                n += d;
            }
            return n;
        }

        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> source, T item)
        {
            Func<T, bool> predicate;
            if (item == null)
                predicate = i => i != null;
            else
                predicate = i => !item.Equals(i);

            return source.Where(predicate);
        }

        public static IEnumerable<T> ExcludeOnce<T>(this IEnumerable<T> source, T item)
        {
            bool excluded = false;
            Func<T, bool> predicate;
            if (item == null)
                predicate = i => i != null;
            else
                predicate = i => !item.Equals(i);

            foreach (var it in source)
            {
                if (excluded || predicate(it))
                    yield return it;
                else
                    excluded = true;
            }
        }

        public static IEnumerable<long> GetRotations(this long n)
        {
            int log10 = (int)Math.Log10(n);
            long mult = 1;
            for (int i = 1; i <= log10; i++) mult *= 10;

            long tmp = n;
            do
            {
                yield return tmp;
                long rem;
                tmp = Math.DivRem(tmp, 10, out rem) + rem * mult;
            } while (tmp != n);
        }

        public static Func<T, TResult> AsSlideCached<T, TResult>(this Func<T, TResult> function, int maxCache)
        {
            var cachedResults = new SortedList<T, TResult>();
            return (argument) =>
            {
                TResult result;
                lock (cachedResults)
                {
                    if (!cachedResults.TryGetValue(argument, out result))
                    {
                        result = function(argument);
                        cachedResults.Add(argument, result);
                        if (cachedResults.Count > maxCache)
                            cachedResults.RemoveAt(0);
                    }
                }
                return result;
            };
        }

        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> selector)
        {
            TSource previous = default(TSource);
            bool hasPrevious = false;
            foreach (var item in source)
            {
                if (hasPrevious)
                    yield return selector(item, previous);
                previous = item;
                hasPrevious = true;
            }
        }

        public static bool IsPalindromic(this int n)
        {
            return n.IsPalindromic(10);
        }

        public static bool IsPalindromic(this int n, int inBase)
        {
            return n.GetDigitsFromEnd(inBase).SequenceEqual(n.GetDigits(inBase));
        }

        public static int SumDigits(this int n)
        {
            return n.GetDigitsFromEnd().Sum();
        }

        public static int Reverse(this int n)
        {
            return n.GetDigitsFromEnd().MakeInt32();
        }

        public static BigInteger Reverse(this BigInteger n)
        {
            return BigInteger.Parse(n.ToString().Reverse());
        }

        public static bool IsPalindromic(this BigInteger n)
        {
            return n.ToString() == n.ToString().Reverse();
        }

        public static int SumDigits(this BigInteger n)
        {
            return n.GetDigitsFromEnd().Sum();
        }

        public static IEnumerable<int> GetDigitsFromEnd(this BigInteger n)
        {
            do
            {
                BigInteger rem;
                n = BigInteger.DivRem(n, 10, out rem);
                yield return (int)rem;
            } while (n != 0);
        }

        public static IEnumerable<int> GetDigits(this BigInteger n)
        {
            return n.GetDigitsFromEnd().Reverse();
        }
    }
}
