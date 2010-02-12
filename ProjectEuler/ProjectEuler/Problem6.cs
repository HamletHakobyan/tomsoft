using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem6 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var range = Enumerable.Range(1, 100).Select(i => (long)i).ToList();
            var sumOfSquares = range.Sum(i => i * i);
            Console.WriteLine(sumOfSquares);
            var sum = range.Sum();
            var squareOfSum = sum * sum;
            Console.WriteLine(squareOfSum);
            return Math.Abs(squareOfSum - sumOfSquares);
        }

        #endregion
    }
}
