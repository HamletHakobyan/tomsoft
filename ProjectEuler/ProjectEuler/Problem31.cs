using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem31 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            // Trop long, et OutOfMemoryException
            var coinValues = new[] { 1, 2, 5, 10, 20, 50, 100, 200 };
            int total = 200;
            var comparer = new SequenceEqualityComparer<Tuple<int, int>>();
            return GetCombinations(coinValues, total).Distinct(comparer).Count();
        }

        private class SequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            #region IEqualityComparer<IEnumerable<T>> Members

            public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IEnumerable<T> obj)
            {
                int hash = 691;
                foreach (var item in obj)
                {
                    hash += 397 * item.GetHashCode();
                }
                return hash;
            }

            #endregion
        }


        private IEnumerable<IEnumerable<Tuple<int, int>>> GetCombinations(IEnumerable<int> coinValues, int total)
        {
            if (total == 0)
                yield break;

            foreach (var coin in coinValues)
            {
                var otherCoins = coinValues.Where(c => c != coin);
                if (otherCoins.Count() == 0)
                {
                    if (total % coin == 0)
                        yield return new[] { Tuple.Create(total / coin, coin) };
                }
                else
                {
                    for (int i = 0; i <= total / coin; i++)
                    {
                        foreach (var comb in GetCombinations(otherCoins, total - i * coin))
                        {
                            yield return comb.Prepend(Tuple.Create(i, coin));
                        }
                    }
                }
            }
        }

        #endregion
    }
}
