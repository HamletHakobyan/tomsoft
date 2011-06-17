namespace SharpMeasure
{
    public class ProductUnit<TFirst, TSecond> : CompositeUnitBase<TFirst, TSecond>
        where TFirst : IUnit, new()
        where TSecond : IUnit, new()
    {
        private static readonly string _symbol;
        private static readonly double _valueInSIUnit;

        static ProductUnit()
        {
            var first = new TFirst();
            var second = new TSecond();
            if (first is NoUnit)
                _symbol = string.Format("{0}", first.Symbol);
            else if (second is NoUnit)
                _symbol = string.Format("{0}", second.Symbol);
            else
                _symbol = string.Format("{0}.{1}", first.Symbol, second.Symbol);
            _valueInSIUnit = first.ValueInSIUnit * second.ValueInSIUnit;
        }

        public override string Symbol { get { return _symbol; } }
        public override double ValueInSIUnit { get { return _valueInSIUnit; } }
    }
}
