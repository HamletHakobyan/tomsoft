using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectEuler
{
    class Problem43 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Enumerable.Range(0, 10)
                    .GetPermutations()
                    .Select(p => p.MakeInt64())
                    .Where(n => AreSubStringsDivisible(n))
                    .Sum();

            //long n = 1406357289;
            //return IsPandigital(n) && AreSubStringsDivisible(n);
        }

        #endregion

        //private Regex _regexRepeat = new Regex(@"(\d)(?=\d*\1)", RegexOptions.Compiled);
        //private Regex _regexAllDigits = new Regex(@"^[0-9]{10}$", RegexOptions.Compiled);
        //private bool IsPandigital(long n)
        //{
        //    string s = n.ToString();
        //    return _regexAllDigits.IsMatch(s) && !_regexRepeat.IsMatch(s);
        //}

        int[] _firstPrimes = new[] { 2, 3, 5, 7, 11, 13, 17 };
        private bool AreSubStringsDivisible(long n)
        {
            string s = n.ToString().PadLeft(10, '0');
            for (int i = 2; i <= 8; i++)
            {
                string sub = s.Substring(i - 1, 3);
                int subN = int.Parse(sub);
                if (subN % _firstPrimes[i - 2] != 0)
                    return false;
            }
            return true;
        }
    }
}
