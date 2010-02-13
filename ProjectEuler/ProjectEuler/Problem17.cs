using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem17 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            //var numbers = Enumerable.Range(1, 1000)
            //    .Select(n => NumberToString(n));
            //foreach (var s in numbers)
            //{
            //    Console.WriteLine(s);
            //}

            return Enumerable.Range(1, 1000)
                .Select(n => NumberToString(n))
                .Sum(s => s.Where(c => c != '-' && c != ' ').Count());
        }

        #endregion

        private static string[] underTwenty = new[]
            {
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen"
            };

        private static string[] tens = new[]
            {
                "ten",
                "twenty",
                "thirty",
                "forty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
            };

        private static string NumberToString(int n)
        {
            if (n < 0 || n > 1000)
                throw new ArgumentOutOfRangeException("n");

            if (n == 0)
                return underTwenty[n];

            if (n == 1000)
                return "one thousand";

            StringBuilder sb = new StringBuilder();

            int h = n / 100;
            if (h > 0)
                sb.AppendFormat("{0} hundred", underTwenty[h]);

            n = n % 100;
            if (n > 0)
            {
                if (h > 0)
                    sb.Append(" and ");

                if (n < 20)
                {
                    sb.Append(underTwenty[n]);
                }
                else
                {
                    int t = n / 10;
                    n = n % 10;

                    sb.Append(tens[t - 1]);
                    if (n > 0)
                        sb.AppendFormat("-{0}", underTwenty[n]);
                }
            }
            return sb.ToString();
        }
    }
}
