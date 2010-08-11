using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectEuler
{
    public class Problem63 : IEulerProblem
    {
        public object GetSolution()
        {
            int maxExponent = Enumerable.Range(1, 1000)
                                        .TakeWhile(i => BigInteger.Pow(9, i) > BigInteger.Pow(10, i - 1))
                                        .Last();
            var q =
                from n in Enumerable.Range(1, maxExponent)
                let min = BigInteger.Pow(10, n - 1)
                let max = BigInteger.Pow(10, n)
                from p in Enumerable.Range(1, 9)
                let N = (BigInteger)Math.Pow(p, n)
                where N >= min && N < max
                select N;

            return q.Count();
        }


    }
}
