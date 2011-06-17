namespace SharpMeasure
{
    public class FractionalUnit<TNumerator, TDenominator> : CompositeUnitBase<TNumerator, TDenominator>
        where TNumerator : IUnit, new()
        where TDenominator : IUnit, new()
    {
        private static readonly string _symbol;
        private static readonly TNumerator _numerator;
        private static readonly TDenominator _denominator;

        static FractionalUnit()
        {
            _numerator = new TNumerator();
            _denominator = new TDenominator();
            if (_numerator is NoUnit)
                _symbol = string.Format("1/{0}", _denominator.Symbol);
            else if (_denominator is NoUnit)
                _symbol = string.Format("{0}", _numerator.Symbol);
            else
                _symbol = string.Format("{0}/{1}", _numerator.Symbol, _denominator.Symbol);
        }

        public override string Symbol { get { return _symbol; } }

        public override double ToSIUnit(double value)
        {
            return _denominator.FromSIUnit(_numerator.ToSIUnit(value));
            // Ex : km/h to m/s
            //                  / 3600                * 1000
        }

        public override double FromSIUnit(double value)
        {
            return _numerator.FromSIUnit(_denominator.ToSIUnit(value));
            // Ex : m/s to km/h
            //                / 1000                  * 3600
        }
    }
}
