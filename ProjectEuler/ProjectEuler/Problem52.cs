using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem52 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.InfiniteSequence(1).First(Predicate);
        }

        #endregion

        bool Predicate(long n)
        {
            string s = n.ToString().OrderBy(c => c).Join();
            for (int i = 2; i <= 6; i++)
            {
                string s2 = (i * n).ToString().OrderBy(c => c).Join();
                if (s != s2)
                    return false;
            }
            return true;
        }
    }
}
