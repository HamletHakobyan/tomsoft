using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem14 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(1, 1000000)
                .MaxBy(i => GetSequence(i).Count());
        }

        #endregion

        public IEnumerable<long> GetSequence(int startValue)
        {
            long n = startValue;
            while (n != 1)
            {
                yield return n;
                if (n % 2 == 0)
                    n = n / 2;
                else
                    n = 3 * n + 1;
            }
        }
    }
}
