using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    public class Problem97 : IEulerProblem
    {
        public object GetSolution()
        {
            // Last 10 digits of 28433 * 2^7830457 + 1

            // Cheating (4+ minutes)
            //BigInteger b = 28433 * BigInteger.Pow(2, 7830457) + 1;
            //string s = b.ToString();
            //return s.Substring(s.Length - 10, 10);

            long res = 28433;
            long mod = (long)Math.Pow(10, 10);
            for (int i = 0; i < 7830457; i++)
            {
                res = (res << 1) % mod;
            }
            res += 1;

            return res;
        }
    }
}
