using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem36 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(1, 999999)
                    .Where(i => i.IsPalindromic(10) && i.IsPalindromic(2))
                    .Sum();
        }

        #endregion
    }
}
