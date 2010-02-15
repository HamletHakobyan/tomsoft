﻿using System;
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
                    .TakeWhile(p => IsPrime(p))
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

        private SortedSet<long> _primes = new SortedSet<long>();
        private bool IsPrime(long n)
        {
            CalculatePrimes(n);
            return _primes.Contains(n);
        }

        private void CalculatePrimes(long max)
        {
            if (_primes.Count == 0)
                _primes.Add(2);
            long i = _primes.Max + 1;
            while (i <= max)
            {
                long sqrt = (long)Math.Floor(Math.Sqrt(i));
                bool hasDivisors = false;
                foreach (long p in _primes.TakeWhile(x => x <= sqrt))
                {
                    if (i % p == 0)
                    {
                        hasDivisors = true;
                        break;
                    }
                }
                if (!hasDivisors)
                {
                    _primes.Add(i);
                }
                i++;
            }
        }
    }
}
