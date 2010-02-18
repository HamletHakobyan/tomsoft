using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProjectEuler
{
    class Problem45 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            long start = 40755;

            double[] coefTri = new[] { 0.5, 0.5 };
            double[] coefPen = new[] { 1.5, -0.5 };
            double[] coefHex = new[] { 2.0, -1.0 };

            Func<double[], long> intRoot =
                coef => GetRoots(coef[0], coef[1], -start)
                        .Where(r => r > 0)
                        .Select(r => (long)Math.Round(r, 0))
                        .First();

            long nTri = intRoot(coefTri);
            long nPen = intRoot(coefPen);
            long nHex = intRoot(coefHex);

            do
            {
                nTri++;
                nPen++;
                nHex++;

                long t = Triangle(nTri);
                if (IsPentagonal(t) && IsHexagonal(t))
                    return t;

                long p = Pentagonal(nPen);
                if (IsTriangle(p) && IsHexagonal(p))
                    return p;

                long h = Hexagonal(nHex);
                if (IsTriangle(h) && IsPentagonal(h))
                    return h;

            } while (true);

            //return null;
        }

        #endregion

        long Triangle(long n)
        {
            return n * (n + 1) / 2;
        }

        long Pentagonal(long n)
        {
            return n * (3 * n - 1) / 2;
        }

        long Hexagonal(long n)
        {
            return n * (2 * n - 1);
        }

        bool IsTriangle(long n)
        {
            double a = 0.5;
            double b = 0.5;
            double c = -n;

            return HasIntegerRoots(a, b, c);
        }

        bool IsPentagonal(long n)
        {
            double a = 1.5;
            double b = -0.5;
            double c = -n;

            return HasIntegerRoots(a, b, c);
        }

        bool IsHexagonal(long n)
        {
            double a = 2.0;
            double b = -1.0;
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
