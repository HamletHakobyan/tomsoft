using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem24 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return Util.GetPermutations(Enumerable.Range(0, 10))
                    .ElementAt(999999)
                    .ToArray()
                    .FormatAll("{0}", string.Empty);
        }

        #endregion
    }
}
