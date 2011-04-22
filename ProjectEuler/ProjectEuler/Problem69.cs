using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    public class Problem69 : IEulerProblem
    {
        public object GetSolution()
        {
            int max = 1000000;
            //var pairs = GetCoPrimesPairs(max);

            //Func<int, int, bool> areCoPrime =
            //    (a, b) => pairs.Contains(new Pair(a, b));
            //Func<int, int> totient =
            //    n => Enumerable.Range(1, n - 1).Count(i => areCoPrime(n, i));

            return ParallelEnumerable
                .Range(2, max)
                .Select(n => new { N = n, Phi = Totient(n) })
                .MaxBy(x => (double)x.N / x.Phi);
        }

        static long Totient(long n)
        {
            var primeDivisors = Util.PrimeDivisors(n).ToArray();
            return (long)(n * primeDivisors.Aggregate(1.0, (acc, p) => acc * (1 - 1.0 / p)));
        }

        static HashSet<Pair> GetCoPrimesPairs(int max)
        {
            var set = new HashSet<Pair>();
            var queue = new Queue<Pair>();
            queue.Enqueue(new Pair(2, 1));
            queue.Enqueue(new Pair(3, 1));
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                if (p.M > max)
                    continue;
                set.Add(p);
                queue.Enqueue(new Pair(2 * p.M - p.N, p.M));
                queue.Enqueue(new Pair(2 * p.M + p.N, p.M));
                queue.Enqueue(new Pair(p.M + 2 * p.N, p.N));
            }
            return set;
        }

        struct Pair
        {
            private readonly int _m;
            private readonly int _n;

            public Pair(int m, int n)
            {
                if (m >= n)
                {
                    _m = m;
                    _n = n;
                }
                else
                {
                    _m = n;
                    _n = m;
                }
            }

            public int M
            {
                get { return _m; }
            }

            public int N
            {
                get { return _n; }
            }

            public override string ToString()
            {
                return string.Format("({0}, {1})", _m, _n);
            }
        }
    }
}
