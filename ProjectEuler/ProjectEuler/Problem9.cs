using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem9 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            /* Hypothèses de départ :
             * a, b et c entiers naturels
             * a² + b² = c²
             * a + b + c = 1000
             * 
             * Donc :
             * b = (1000 a - 500000)/(a - 1000)
             * c = (- a² + 1000 a - 500000)/(a - 1000)
             */

            var q =
                from a in Enumerable.Range(0, 1000)
                let b = (1000 * a - 500000) / (a - 1000)
                let c = (-(a * a) + 1000 * a - 500000) / (a - 1000)
                where a < b && b < c
                && a + b + c == 1000
                && a * a + b * b == c * c
                select new { a, b, c };

            foreach(var item in q) Console.WriteLine("{0}, {1}, {2}", item.a, item.b, item.c);
            return q.Select(x => x.a * x.b * x.c).FirstOrDefault();
        }

        #endregion
    }
}
