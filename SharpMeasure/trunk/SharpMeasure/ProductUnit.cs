namespace SharpMeasure
{
    public class ProductUnit<TFirst, TSecond> : CompositeUnitBase<TFirst, TSecond>
        where TFirst : IUnit, new()
        where TSecond : IUnit, new()
    {
        private static readonly string _symbol;
        private static readonly TFirst _first;
        private static readonly TSecond _second;

        static ProductUnit()
        {
            _first = new TFirst();
            _second = new TSecond();
            if (_first is NoUnit)
                _symbol = string.Format("{0}", _second.Symbol);
            else if (_second is NoUnit)
                _symbol = string.Format("{0}", _first.Symbol);
            else
                _symbol = string.Format("{0}.{1}", _first.Symbol, _second.Symbol);
        }

        public override string Symbol { get { return _symbol; } }

        public override double ToSIUnit(double value)
        {
            return _first.ToSIUnit(_second.ToSIUnit(value));
        }

        public override double FromSIUnit(double value)
        {
            return _second.FromSIUnit(_first.FromSIUnit(value));
        }

    }
}
