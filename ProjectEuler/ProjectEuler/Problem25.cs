using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem25 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            string sTarget = new string('9', 999);
            BigInteger target = BigInteger.Parse(sTarget);

            return Util.Fibonacci()
                .AsIndexed()
                .First(x => x.Value > target)
                .Index;
        }

        #endregion
    }
}
