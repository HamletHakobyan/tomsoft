using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    class Problem16 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            BigInteger x = BigInteger.Pow(2, 1000);
            return x.ToString()
                .Select(c => int.Parse(c.ToString()))
                .Sum();
        }

        #endregion
    }
}
