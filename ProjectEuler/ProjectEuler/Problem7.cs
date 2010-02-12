using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem7 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.PrimeNumbers().ElementAt(10000);
        }

        #endregion
    }
}
