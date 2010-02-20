using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem32 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            Func<int, IEnumerable<int>> rangeOfB =
                aa =>
                {
                    int da = NumberOfDigits(aa);
                    int mindb = Math.Max((int)Math.Round((9 - 2 * da) / 2.0, 0), 1);
                    int maxdb = Math.Max(5 - da, 1);
                    int minb = (int)Math.Pow(10, mindb - 1);
                    int maxb = (int)Math.Pow(10, maxdb) - 1;
                    return Enumerable.Range(minb, maxb - minb + 1);
                };

            var q =
                from a in Enumerable.Range(1, 9999)
                from b in rangeOfB(a)
                let p = a * b
                where IsPandigitalIdentity(1, 9, a, b, p)
                select p;
            return q.Distinct().Sum();
        }

        private bool IsPandigitalIdentity(int from, int to, params int[] numbers)
        {
            bool[] digits = new bool[10];
            foreach (int n in numbers)
            {
                int tmp = n;

                do
                {
                    int rem;
                    tmp = Math.DivRem(tmp, 10, out rem);
                    if (digits[rem]) // digit already present
                        return false;
                    digits[rem] = true;
                } while (tmp != 0);
            }

            for (int i = 0; i < 10; i++)
            {
                if (i >= from && i <= to)
                {
                    if (!digits[i])
                        return false;
                }
                else
                {
                    if (digits[i])
                        return false;
                }
            }
            return true;
        }

        #endregion

        public int NumberOfDigits(params int[] numbers)
        {
            int nDigits = 0;
            foreach (int n in numbers)
            {
                int tmp = n;
                do
                {
                    nDigits++;
                    tmp /= 10;
                } while (tmp != 0);
            }
            return nDigits;
        }
    }
}
