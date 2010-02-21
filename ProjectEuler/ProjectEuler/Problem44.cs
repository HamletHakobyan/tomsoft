using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem44 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var pentagonals = new HashSet<long>(Enumerable.Range(1, 3000).Select(n => Pentagonal(n)));


            var pairs =
                from p1 in pentagonals
                from p2 in pentagonals
                where p2 > p1
                && pentagonals.Contains(p2 - p1)
                && pentagonals.Contains(p2 + p1)
                select new { P1 = p1, P2 = p2 };

            return pairs.Min(p => p.P2 - p.P1);
        }

        #endregion

        long Pentagonal(long n)
        {
            return n * (3 * n - 1) / 2;
        }

        bool IsPentagonal(long n)
        {
            double a = 1.5;
            double b = -0.5;
            double c = -n;

            return HasIntegerRoots(a, b, c);
        }

        private bool HasIntegerRoots(double a, double b, double c)
        {
            var roots = GetRoots(a, b, c)
                .Where(r => r > 0)
                .Select(r => (long)Math.Round(r, 0))
                .Where(r => a * r * r + b * r + c == 0)
                .ToArray();

            return roots.Any();
        }

        private double[] GetRoots(double a, double b, double c)
        {
            double delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                return new double[] { };
            }
            else if (delta > 0)
            {
                return new double[] { (-b - Math.Sqrt(delta)) / (2 * a), (-b + Math.Sqrt(delta)) / (2 * a) };
            }
            else
            {
                return new double[] { -b / (2 * a) };
            }
        }
    }
}
