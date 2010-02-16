using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    class Problem29 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return
                Enumerable.Range(2, 99)
                .SelectMany(a => Enumerable.Range(2, 99),
                            (a, b) => BigInteger.Pow(a, b))
                .Distinct()
                .Count();
        }

        #endregion


    }
}
