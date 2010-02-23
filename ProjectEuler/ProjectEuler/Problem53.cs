using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem53 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var q =
                from n in Enumerable.Range(1, 100)
                from r in Enumerable.Range(1, n)
                where MoreThanMCombinations(n, r, 1000000)
                select 1;

            return q.Count();
        }

        #endregion

        private static bool MoreThanMCombinations(int n, int r, int m)
        {
            try
            {
                return Util.FactorialBig(n) / (Util.FactorialBig(r) * Util.FactorialBig(n - r)) > m;
            }
            catch
            {
                throw;
            }
        }
    }
}
