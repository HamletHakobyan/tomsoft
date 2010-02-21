using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem49 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var distinctNumbers =
                Enumerable.Range(1000, 9000)
                .Select(n => n.GetDigits().OrderBy(x => x).MakeInt32())
                .Distinct();

            var q =
                from n in distinctNumbers
                let seq = ExtractArithmeticSequence(GetPrimePermutations(n))
                where seq.Count() >= 3
                select seq.Select(p => p.ToString().PadLeft(4, '0')).Join(" ");

            return q.Distinct().Join(Environment.NewLine);
        }

        #endregion

        PrimeSieve _sieve = new PrimeSieve();
        public IEnumerable<int> GetPrimePermutations(int n)
        {
            var perm = n.GetDigits()
                .GetPermutations()
                .Select(p => p.MakeInt32())
                .Where(x => _sieve.IsPrime(x))
                .Distinct();
            return perm;
        }

        public IEnumerable<int> ExtractArithmeticSequence(IEnumerable<int> numbers)
        {
            var array = numbers.OrderBy(n => n).ToArray();
            if (array.Length < 3)
                return Enumerable.Empty<int>();

            for (int i = 0; i < array.Length - 1; i++)
            {
                int first = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    int step = array[j] - first;
                    var seq = array.Where(n => (n - first) % step == 0).ToArray();
                    if (seq.Length >= 3 && Enumerable.Range(0, seq.Length).Select(n => first + n * step).SequenceEqual(seq))
                        return seq;
                }
                
            }
            return Enumerable.Empty<int>();
        }
    }
}
