using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem2 : IEulerProblem
    {
        public object GetSolution()
        {
            var sequence =
                Util.Fibonacci()
                .TakeWhile(x => x <= 4000000)
                .Where(x => x % 2 == 0);
            return sequence.Sum();
        }
    }
}
