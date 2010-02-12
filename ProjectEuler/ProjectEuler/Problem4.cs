using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem4 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var query = 
                from x in Enumerable.Range(100, 900)
                from y in Enumerable.Range(100, 900)
                let prod = x * y
                where prod.ToString() == prod.ToString().Reverse()
                select prod;
            return query.Max();
        }

        #endregion
    }
}
