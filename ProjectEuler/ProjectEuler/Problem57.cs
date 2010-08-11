using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Developpez.Dotnet.Collections;

namespace ProjectEuler
{
    class Problem57 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            //return Enumerable.Range(1, 1000)
            //        .Select(n => 1 + new Fraction(1, GetDenominatorExpansion(n)))
            //        .Where(NumeratorLongerThanDenominator)
            //        .Count();

            var numerators =
                Unfold<BigInteger, BigInteger>(
                    (s1, s2) => Tuple.Create(
                                    2 * s1 + s2,
                                    2 * s1 + s2),
                1, 1);

            var denominators =
                Unfold<BigInteger, BigInteger>(
                    (s1, s2) => Tuple.Create(
                                    2 * s1 + s2,
                                    2 * s1 + s2),
                    1, 0);

            var fractions =
                numerators.Zip(
                    denominators,
                    (n, d) => new Fraction(n, d));

            //foreach (var item in fractions.Take(10))
            //{
            //    Console.WriteLine(item);
            //}

            return fractions.Take(1000).Where(NumeratorLongerThanDenominator).Count();

        }

        #endregion

        public IEnumerable<TResult> Unfold<TState, TResult>(Func<TState, TState, Tuple<TResult, TState>> generator, TState seed1, TState seed2)
        {
            TState state1 = seed1;
            TState state2 = seed2;
            while (true)
            {
                var res = generator(state1, state2);
                if (res == null)
                    yield break;
                yield return res.Item1;
                state2 = state1;
                state1 = res.Item2;
            }
        }

        private bool NumeratorLongerThanDenominator(Fraction f)
        {
            int n = f.Numerator.GetDigitsFromEnd().Count();
            int d = f.Denominator.GetDigitsFromEnd().Count();
            //Console.WriteLine(f);
            return n > d;
        }

        private Fraction GetDenominatorExpansion(int n)
        {
            if (n == 0)
                return 2;
            else
                return 2 + new Fraction(1, GetDenominatorExpansion(n - 1));
        }

        struct Fraction
        {
            public Fraction(BigInteger value)
                : this(value, 1)
            {
            }

            public Fraction(BigInteger numerator, BigInteger denominator)
                : this()
            {
                Numerator = numerator;
                Denominator = denominator;
                if (Util.GCD(Numerator, Denominator) > 1)
                    this = this.Reduce();
            }

            public Fraction(BigInteger numerator, Fraction denominator)
                : this()
            {
                // x / (y / z) = x * (z / y) = x * z / y
                Numerator = numerator * denominator.Denominator;
                Denominator = denominator.Numerator;
                if (Util.GCD(Numerator, Denominator) > 1)
                    this = this.Reduce();
            }

            public BigInteger Numerator { get; private set; }
            public BigInteger Denominator { get; private set; }

            public Fraction Reduce()
            {
                BigInteger gcd = Util.GCD(Numerator, Denominator);
                return new Fraction(Numerator / gcd, Denominator / gcd);
            }

            public Fraction Invert()
            {
                return new Fraction(Denominator, Numerator);
            }

            public static implicit operator Fraction(BigInteger value)
            {
                return new Fraction(value, 1);
            }

            public static implicit operator Fraction(long value)
            {
                return new Fraction(value, 1);
            }

            public static Fraction operator +(Fraction f1, Fraction f2)
            {
                f1 = f1.Reduce();
                f2 = f2.Reduce();
                BigInteger lcm = Util.LCM(f1.Denominator, f2.Denominator);
                BigInteger n1 = f1.Numerator * (lcm / f1.Denominator);
                BigInteger n2 = f2.Numerator * (lcm / f2.Denominator);
                return new Fraction(n1 + n2, lcm);
            }

            public override string ToString()
            {
                return string.Format("{0} / {1}", Numerator, Denominator);
            }
        }
    }
}
