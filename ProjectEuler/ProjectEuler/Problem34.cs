using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem34 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            // Asumption : not higher than D * 9! = 19 * 9! = 6894720
            // where D = number of digits in Int64.MaxValue

            var q = Util.InfiniteSequence()
                .SkipWhile(n => n <= 2)
                .TakeWhile(n => n <= 6894720)
                .Where(n => n == SumOfDigitFactorials(n));

            return q.Sum();
        }

        #endregion

        private long SumOfDigitFactorials(long n)
        {
            long sum = 0;
            while (n > 0)
            {
                long rem;
                n = Math.DivRem(n, 10, out rem);
                sum += Util.Factorial(rem);
            }
            return sum;
        }
    }
}
