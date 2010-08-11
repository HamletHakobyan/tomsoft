using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class Problem58 : IEulerProblem
    {
        public object GetSolution()
        {
            int n = 1;
            int nPrime = 0;
            int current = 1;
            int step = 2;
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    current += step;
                    n++;
                    if (i != 3 && Util.IsPrime(current))
                        nPrime++;
                }
                double ratio = (double)nPrime / n;
                if (ratio < 0.1)
                    return step + 1;
                step += 2;
            }
        }
    }
}
