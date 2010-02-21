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
            return IsPrime(n, true);
        }

        public bool IsPrime(long n, bool add)
        {
            if (add)
            {
                CalculatePrimes(n);
                return _primes.Contains(n);
            }
            else
            {
                long sqrt = (long)Math.Floor(Math.Sqrt(n));
                CalculatePrimes(sqrt);
                return IsPrimeInternal(n);
            }
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
                if (IsPrimeInternal(i))
                {
                    _primes.Add(i);
                    //System.Diagnostics.Debug.WriteLine(string.Format("{0} is prime", i));
                }
                i++;
            }
        }

        private bool IsPrimeInternal(long n)
        {
            bool hasDivisors = false;
            long sqrt = (long)Math.Floor(Math.Sqrt(n));
            foreach (long p in _primes.TakeWhile(x => x <= sqrt))
            {
                if (n % p == 0)
                {
                    hasDivisors = true;
                    break;
                }
            }
            return !hasDivisors;
        }
    }
}
