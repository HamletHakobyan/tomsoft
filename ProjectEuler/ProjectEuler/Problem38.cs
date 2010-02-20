using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem38 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return
                Enumerable.Range(1, 9)
                .GetPermutations()
                .Select(digits => digits.MakeNumber())
                .OrderByDescending(n => n)
                .Where(n => IsMadeOfConsecutiveProducts(n))
                .Max();
        }

        #endregion

        private bool IsMadeOfConsecutiveProducts(int n)
        {
            for (int len = 4; len > 0; len--)
            {
                string s = n.ToString();
                string curr = s.Substring(0, len);
                int first = int.Parse(curr);
                int prev = 0;
                bool ok = true;
                while (s.Length > 0)
                {
                    prev += first;
                    curr = prev.ToString();
                    if (!s.StartsWith(curr))
                    {
                        ok = false;
                        break;
                    }
                    s = s.Substring(curr.Length);
                }
                if (ok)
                    return true;
            }
            return false;
        }

    }
}
