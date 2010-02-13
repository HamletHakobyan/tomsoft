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
            return Util.InfiniteSequence(1)
                .Select(n => n * (n + 1) / 2)
                .Where(n => GetNumberOfDivisors(n) > 500)
                .First();
        }

        #endregion

        private long _max = 0;
        public long GetNumberOfDivisors(long n)
        {
            long nDiv = 0;
            int s = (n % 2 == 0) ? 1 : 2;
            long sqrt = (long)Math.Floor(Math.Sqrt(n));
            for (long i = 1; i <= sqrt; i += s)
            {
                if (n % i == 0)
                    nDiv += 2;
            }
            if (nDiv > _max)
            {
                _max = nDiv;
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", n, nDiv));
            }
            return nDiv;
        }
    }
}
