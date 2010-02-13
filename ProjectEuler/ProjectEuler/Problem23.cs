using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem23 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.InfiniteSequence(1)
                        .Take(28123)
                        .Where(n => !CanBeWrittenAsSumOfAbundants(n))
                        .Sum();
        }

        #endregion

        bool IsAbundant(long n)
        {
            long ndiv = Util.ProperDivisors(n).Sum();
            return (ndiv > n);
        }

        Func<long, bool> _isAbundant;
        bool CanBeWrittenAsSumOfAbundants(long n)
        {
            if (_isAbundant == null)
            {
                _isAbundant = new Func<long, bool>(IsAbundant).AsCached();
            }

            if (n > 28123)
                return true;

            for (int i = 1; i <= n / 2; i++)
            {
                if (_isAbundant(i) && _isAbundant(n - i))
                    return true;
            }

            return false;
        }
    }
}
