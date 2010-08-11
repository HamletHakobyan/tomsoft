using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem5 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            //var factors = new[] { 2L, 3, 4, 5, 7, 8, 9, 11, 13, 16, 17, 19 };
            //return Util.InfiniteSequence(1)
            //    .Select(x => 20 * x)
            //    .Where(x => !factors.Any(f => x % f != 0))
            //    .First();

            var factors = new[] { 2L, 3, 4, 5, 7, 8, 9, 11, 13, 16, 17, 19, 20 };
            return Util.LCM(factors);
        }

        #endregion
    }
}
