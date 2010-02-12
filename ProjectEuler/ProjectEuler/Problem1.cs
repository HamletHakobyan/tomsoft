using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class Problem1 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(0, 1000)
                .Where(x => (x % 3 == 0) || (x % 5 == 0))
                .Sum();
        }

        #endregion
    }
}
