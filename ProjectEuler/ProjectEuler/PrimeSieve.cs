using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class PrimeSieve
    {
        private SortedSet<long> _primes;

        public PrimeSieve()
        {
            _primes = new SortedSet<long>();
            _primes.Add(2);
        }

        public bool IsPrime(long n)
        {
            CalculatePrimes(n);
            return _primes.Contains(n);
        }

        public IEnumerable<long> GetPrimes(long max)
        {
            CalculatePrimes(max);
            foreach (var p in _primes)
            {
                yield return p;
            }
        }

        private void CalculatePrimes(long max)
        {
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
