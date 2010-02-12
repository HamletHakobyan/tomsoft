using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem10 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.PrimeNumbers().TakeWhile(p => p < 2000000).Sum();
        }

        #endregion
    }
}