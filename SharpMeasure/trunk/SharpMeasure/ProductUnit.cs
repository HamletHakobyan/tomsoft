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
            var numerator = new TFirst();
            var denominator = new TSecond();
            _symbol = string.Format("{0}*{1}", numerator.Symbol, denominator.Symbol);
            _valueInSIUnit = numerator.ValueInSIUnit * denominator.ValueInSIUnit;
        }

        public override string Symbol { get { return _symbol; } }
        public override double ValueInSIUnit { get { return _valueInSIUnit; } }
    }
}
