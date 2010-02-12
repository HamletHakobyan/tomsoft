using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem3 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.PrimeDivisors(600851475143).Max();
        }

        #endregion
    }
}
