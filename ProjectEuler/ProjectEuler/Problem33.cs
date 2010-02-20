using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem33 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var specialFractions =
                from a in Enumerable.Range(10, 89)
                from b in Enumerable.Range(a + 1, 100 - a)
                where !(a % 10 == 0 && b % 10 == 0)
                && IsSpecialFraction(a, b)
                select new { a, b };

            var product = specialFractions.Aggregate(
                new { a = 1, b = 1 },
                (p, x) => new { a = p.a * x.a, b = p.b * x.b });

            int gcd = (int)Util.GCD(product.a, product.b);
            var reducedProduct = new
            {
                a = product.a / gcd,
                b = product.b / gcd
            };

            return reducedProduct.b;
        }

        #endregion

        private bool IsSpecialFraction(int numerator, int denominator)
        {

            var numeratorDigits = numerator.GetDigits();
            var denominatorDigits = denominator.GetDigits();
            var commonDigits = numeratorDigits.Intersect(denominatorDigits);
            return commonDigits.Any(d =>
                {
                    int num = numeratorDigits.ExcludeOnce(d).MakeNumber();
                    int den = denominatorDigits.ExcludeOnce(d).MakeNumber();
                    double p1 = (double)numerator / denominator;
                    double p2 = (double)num / den;
                    return p1 == p2;
                });
        }
    }
}
