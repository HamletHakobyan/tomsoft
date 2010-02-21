using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem40 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            StringBuilder sb = new StringBuilder();
            int n = 1;
            while (sb.Length < 1000000)
            {
                sb.Append(n);
                n++;
            }

            int prod = 1;
            int pos = 1;
            for (int i = 0; i < 7; i++)
            {
                prod *= sb[pos - 1] - '0';
                pos *= 10;
            }
            return prod;
        }

        #endregion
    }
}
