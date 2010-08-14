using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem51 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            PrimeSieve sieve = new PrimeSieve();
            Func<long, bool> predicate = n => PrimesFamilyCount(n, sieve) == 8;
            var q =
                from families in sieve.GetPrimes(1000000).Select(p => GenerateFamilies(p))
                from f in families
                let np = f.Count(p => sieve.IsPrime(p))
                where np == 8
                select f.Min();

            return q.First();
        }

        #endregion

        private int PrimesFamilyCount(long n, PrimeSieve sieve)
        {
            return GenerateFamilies(n)
                    .Select(f => f.Count(p => sieve.IsPrime(p)))
                    .DefaultIfEmpty()
                    .Max();
        }

        IEnumerable<IEnumerable<long>> GenerateFamilies(long n)
        {
            int[] digits = n.GetDigits().ToArray();
            for (int digitsToChange = 1; digitsToChange < digits.Length; digitsToChange++)
            {
                foreach (var positionsToChange in GetCombinations(digits.Length, digitsToChange))
                {
                    yield return GenerateFamily(digits, positionsToChange).Select(p => p.MakeInt64());
                }
            }
        }

        IEnumerable<int[]> GenerateFamily(int[] digits, IEnumerable<int> positionsToChange)
        {
            for (int d = 0; d < 10; d++)
            {
                var copy = digits.ToArray();
                foreach (var pos in positionsToChange)
                {
                    if (pos == 0 && d == 0)
                    {
                        copy = null;
                        break;
                    }
                    copy[pos] = d;
                }
                if (copy != null)
                    yield return copy;
            }
        }

        public IEnumerable<IEnumerable<int>> GetCombinations(int n, int k)
        {
            var tmp = (new[] { Enumerable.Empty<int>() }).AsEnumerable();
            for (int i = 0; i < k; i++)
            {
                tmp =
                    from t in tmp
                    let m = t.Any() ? t.Max() : -1
                    from d in Enumerable.Range(m + 1, n - (m + 1))
                    select t.Concat(new[] { d });
            }
            return tmp;
        }
    }
}
