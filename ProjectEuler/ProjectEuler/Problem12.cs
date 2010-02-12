using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem12 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var q = TriangleNumbers().Take(50);
            // TODO
            return null;
        }

        #endregion

        public IEnumerable<long> TriangleNumbers()
        {
            return Util.InfiniteSequence(1).SelectAggregate(0L, (i, prev) => prev + i);
        }
    }
}
