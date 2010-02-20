using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem31 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var coinValues = new[] { 1, 2, 5, 10, 20, 50, 100, 200 };
            int total = 200;
            return CountCombinations(coinValues, total);
        }

        #endregion

        private int CountCombinations(int[] coinValues, int total)
        {
            return CountCombinations(coinValues, total, 0);
        }

        private int CountCombinations(int[] coinValues, int total, int i)
        {
            if (total == 0)
                return 1;

            int count = 0;
            int coin = coinValues[i];
            if (i + 1 >= coinValues.Length)
            {
                if (total % coin == 0)
                {
                    count++;
                }
            }
            else
            {
                for (int n = 0; n <= total / coin; n++)
                {
                    int coinTotal = n * coin;
                    count += CountCombinations(coinValues, total - coinTotal, i + 1);
                }
            }

            return count;
        }
    }
}
