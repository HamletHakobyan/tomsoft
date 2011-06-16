﻿namespace SharpMeasure
{
    public class FractionalUnit<TNumerator, TDenominator> : CompositeUnitBase<TNumerator, TDenominator>
        where TNumerator : IUnit, new()
        where TDenominator : IUnit, new()
    {
        private static readonly string _symbol;
        private static readonly double _valueInSIUnit;

        static FractionalUnit()
        {
            var numerator = new TNumerator();
            var denominator = new TDenominator();
            _symbol = string.Format("{0}/{1}", numerator.Symbol, denominator.Symbol);
            _valueInSIUnit = numerator.ValueInSIUnit / denominator.ValueInSIUnit;
        }

        public override string Symbol { get { return _symbol; } }
        public override double ValueInSIUnit { get { return _valueInSIUnit; } }
    }
}