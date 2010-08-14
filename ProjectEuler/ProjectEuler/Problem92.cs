using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class Problem92 : IEulerProblem
    {
        public object GetSolution()
        {
            int exclusiveUpperBound = 10000000;
            return Enumerable
                .Range(1, exclusiveUpperBound - 1)
                .Where(n => GenerateSequence(n).Last() == 89)
                .Count();
        }

        private IEnumerable<long> GenerateSequence(long seed)
        {
            long tmp = seed;
            while(true)
            {
                yield return tmp;
                if (tmp == 1 || tmp == 89)
                    break;
                tmp = GetNextInSequence(tmp);
            }
        }

        private long GetNextInSequence(long n)
        {
            long sum = 0;
            while (n != 0)
            {
                long d = n % 10;
                sum += d * d;
                n /= 10;
            }
            return sum;
        }
    }
}
