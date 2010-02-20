using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem39 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            // ABC triangle, rectangle in B
            // Hypothenuse = AC
            // AC >= AB >= BC
            // P = AB + BC + AC <= 1000
            var solutionsPerPerimeter =
                from ac in Enumerable.Range(1, 998)
                from ab in Enumerable.Range(1, Math.Min(ac, 999 - ac))
                from bc in Enumerable.Range(1, Math.Min(ab, 1000 - (ab + ac)))
                where IsRectangleTriangle(ac, ab, bc)
                group 1 by ab + bc + ac into g
                select new { Perimeter = g.Key, NumberOfSolutions = g.Count() };

            //foreach (var g in solutionsPerPerimeter)
            //{
            //    Console.WriteLine("{0}\t{1}", g.Perimeter, g.NumberOfSolutions);
            //}

            return solutionsPerPerimeter.WithMax(g => g.NumberOfSolutions).Perimeter;
        }

        #endregion

        bool IsRectangleTriangle(int hypo, int s1, int s2)
        {
            double x = Math.Sqrt(s1 * s1 + s2 * s2);
            return x == hypo;
        }

    }
}
