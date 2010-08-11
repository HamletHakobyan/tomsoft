using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    class Problem55 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(0, 10000)
                .Where(IsLychrelNumber)
                .Count();
        }

        #endregion

        static bool IsLychrelNumber(int n)
        {
            BigInteger tmp = n;
            for (int i = 1; i < 50; i++)
            {
                tmp = tmp + tmp.Reverse();
                if (tmp.IsPalindromic())
                    return false;
            }
            return true;
        }
    }
}
