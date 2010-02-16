using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem48 : IEulerProblem
    {

        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(1, 1000)
                .Select(n => BigInteger.Pow(n, n))
                .Aggregate(BigInteger.Zero, (acc, n) => acc + n)
                .ToString()
                .Right(10);
        }

        #endregion
    }
}
