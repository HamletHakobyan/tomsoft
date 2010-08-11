using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    class Problem56 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var query =
                from a in Enumerable.Range(1, 100)
                from b in Enumerable.Range(1, 100)
                select BigInteger.Pow(a, b).SumDigits();
            
            return query.Max();
        }

        #endregion
    }
}
