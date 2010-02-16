using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem30 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            int p = 5;
            int max = 4 * (int)Math.Pow(9, p);

            return Enumerable.Range(2, max - 1)
                .Where(n => n == SumOfDigitsToPower(n, p))
                .Sum();
        }

        #endregion

        private static int SumOfDigitsToPower(int n, int p)
        {
            int sum = 0;
            while (n != 0)
            {
                int rem;
                n = Math.DivRem(n, 10, out rem);
                sum += (int)Math.Pow(rem, p);
            }
            return sum;
        }
    }
}
