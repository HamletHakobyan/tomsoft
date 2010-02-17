using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem45 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            throw new NotImplementedException();
        }

        bool IsTriangle(long n)
        {
            double a = 0.5;
            double b = 0.5;
            double c = -n;

            var roots = GetRoots(a, b, c);

            return false;
        }

        bool IsPentagonal(long n)
        {
            double a = 1.5;
            double b = -0.5;
            double c = -n;
            
            return false;
        }

        bool IsHexagonal(long n)
        {
            double a = 2.0;
            double b = -1.0;
            double c = -n;

            return false;
        }

        public void TestGetRoots()
        {
            var roots = GetRoots(0.5, 0.5, 40755);
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

        #endregion
    }
}
