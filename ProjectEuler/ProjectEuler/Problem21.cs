using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem21 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            long sum = 0;
            for (int i = 1; i <= 10000; i++)
            {
                if (IsAmicable(i))
                    sum += i;
            }
            return sum;
        }

        private Func<int, int> _sumOfDivisors;
        private bool IsAmicable(int n)
        {
            if (_sumOfDivisors == null)
            {
                _sumOfDivisors = new Func<int, int>(GetSumOfDivisors).AsCached();
            }

            int sumDiv = _sumOfDivisors(n);
            if (sumDiv != n)
            {
                int sumDiv2 = _sumOfDivisors(sumDiv);
                return sumDiv2 == n;
            }
            return false;
        }

        #endregion

        public int GetSumOfDivisors(int n)
        {
            int sum = 0;
            int s = (n % 2 == 0) ? 1 : 2;
            for (int i = 1; i <= n / 2; i += s)
            {
                if (n % i == 0)
                    sum += i;
            }
            return sum;
        }
    }
}
