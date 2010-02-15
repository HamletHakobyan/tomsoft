using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem28 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            // 1x1 square : 1
            // 3x3 square : 3 + 5 + 7 + 9
            // 5x5 square : 13 + 17 + 21 + 25
            // 7x7 square : 31 + 37 + 43 + 49
            // 9x9 square : 57 + 65 + 73 + 81
            //...
            // NxN square : N² + (N² - (N-1)) + (N² - 2*(N-1)) + (N² - 3*(N-1))
            //           = 4*N² - 6 * (N - 1)
            //           = 4*N² - 6N + 6

            return Enumerable.Range(1, 1001)
                .Where(n => n % 2 != 0)
                .Select(n => (n == 1) ? 1 : 4 * n * n - 6 * n + 6)
                .Sum();
        }

        #endregion

    }
}
