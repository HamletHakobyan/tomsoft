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
            Func<long, long, long, long> fibonacci = (n, prevFib1, prevFib2) => n <= 1 ? n : prevFib1 + prevFib2;

            var sequence =
                Util.InfiniteSequence()
                .SelectAggregate(fibonacci)
                .TakeWhile(x => x <= 4000000)
                .Where(x => x % 2 == 0);
            return sequence.Sum();
        }
    }
}
