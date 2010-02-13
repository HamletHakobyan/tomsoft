using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    class Problem20 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            BigInteger f100 = Factorial(100);
            return f100.ToString()
                .Select(c => int.Parse(c.ToString()))
                .Sum();
        }

        private BigInteger Factorial(int n)
        {
            BigInteger f = 1;
            for (int i = 2; i <= n; i++)
            {
                f *= i;
            }
            return f;
        }

        #endregion
    }
}
