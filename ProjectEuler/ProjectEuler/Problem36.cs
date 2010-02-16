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
                    .Where(IsPalindromic)
                    .Sum();
        }

        #endregion

        bool IsPalindromic(int n)
        {
            string dec = Convert.ToString(n, 10);
            if (dec != dec.Reverse()) return false;
            string bin = Convert.ToString(n, 2);
            if (bin != bin.Reverse()) return false;
            return true;
        }
    }
}
