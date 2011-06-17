﻿namespace SharpMeasure.Time
{
    public class Second : IUnit<ITime>
    {
        public string Symbol { get { return "s"; } }
        
        public double ToSIUnit(double value)
        {
            return value;
        }

        public double FromSIUnit(double value)
        {
            return value;
        }
    }
}
