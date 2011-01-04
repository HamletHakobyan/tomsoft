using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
    
        public static int Min(this IEnumerable<int> source)
        {
            return source.Min(x => x);
        }

        public static int? Min(this IEnumerable<int?> source)
        {
            return source.Min(x => x);
        }

        public static int Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int min = int.MaxValue;
            foreach(TSource item in source)
            {
                int value = selector(item);
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static int? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int? min = null;
            foreach(TSource item in source)
            {
                int? value = selector(item);
                if (!(value >= min))
                    min = value;
            }
            return min;
        }
    
        public static long Min(this IEnumerable<long> source)
        {
            return source.Min(x => x);
        }

        public static long? Min(this IEnumerable<long?> source)
        {
            return source.Min(x => x);
        }

        public static long Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long min = long.MaxValue;
            foreach(TSource item in source)
            {
                long value = selector(item);
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static long? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long? min = null;
            foreach(TSource item in source)
            {
                long? value = selector(item);
                if (!(value >= min))
                    min = value;
            }
            return min;
        }
    
        public static double Min(this IEnumerable<double> source)
        {
            return source.Min(x => x);
        }

        public static double? Min(this IEnumerable<double?> source)
        {
            return source.Min(x => x);
        }

        public static double Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double min = double.MaxValue;
            foreach(TSource item in source)
            {
                double value = selector(item);
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static double? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double? min = null;
            foreach(TSource item in source)
            {
                double? value = selector(item);
                if (!(value >= min))
                    min = value;
            }
            return min;
        }
    
        public static float Min(this IEnumerable<float> source)
        {
            return source.Min(x => x);
        }

        public static float? Min(this IEnumerable<float?> source)
        {
            return source.Min(x => x);
        }

        public static float Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float min = float.MaxValue;
            foreach(TSource item in source)
            {
                float value = selector(item);
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static float? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float? min = null;
            foreach(TSource item in source)
            {
                float? value = selector(item);
                if (!(value >= min))
                    min = value;
            }
            return min;
        }
    
        public static decimal Min(this IEnumerable<decimal> source)
        {
            return source.Min(x => x);
        }

        public static decimal? Min(this IEnumerable<decimal?> source)
        {
            return source.Min(x => x);
        }

        public static decimal Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal min = decimal.MaxValue;
            foreach(TSource item in source)
            {
                decimal value = selector(item);
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static decimal? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal? min = null;
            foreach(TSource item in source)
            {
                decimal? value = selector(item);
                if (!(value >= min))
                    min = value;
            }
            return min;
        }
    
    }
}