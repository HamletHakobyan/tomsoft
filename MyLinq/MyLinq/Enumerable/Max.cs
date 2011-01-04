using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
    
        public static int Max(this IEnumerable<int> source)
        {
            return source.Max(x => x);
        }

        public static int? Max(this IEnumerable<int?> source)
        {
            return source.Max(x => x);
        }

        public static int Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int max = int.MaxValue;
            foreach(TSource item in source)
            {
                int value = selector(item);
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static int? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int? max = null;
            foreach(TSource item in source)
            {
                int? value = selector(item);
                if (!(value <= max))
                    max = value;
            }
            return max;
        }
    
        public static long Max(this IEnumerable<long> source)
        {
            return source.Max(x => x);
        }

        public static long? Max(this IEnumerable<long?> source)
        {
            return source.Max(x => x);
        }

        public static long Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long max = long.MaxValue;
            foreach(TSource item in source)
            {
                long value = selector(item);
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static long? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long? max = null;
            foreach(TSource item in source)
            {
                long? value = selector(item);
                if (!(value <= max))
                    max = value;
            }
            return max;
        }
    
        public static double Max(this IEnumerable<double> source)
        {
            return source.Max(x => x);
        }

        public static double? Max(this IEnumerable<double?> source)
        {
            return source.Max(x => x);
        }

        public static double Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double max = double.MaxValue;
            foreach(TSource item in source)
            {
                double value = selector(item);
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static double? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double? max = null;
            foreach(TSource item in source)
            {
                double? value = selector(item);
                if (!(value <= max))
                    max = value;
            }
            return max;
        }
    
        public static float Max(this IEnumerable<float> source)
        {
            return source.Max(x => x);
        }

        public static float? Max(this IEnumerable<float?> source)
        {
            return source.Max(x => x);
        }

        public static float Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float max = float.MaxValue;
            foreach(TSource item in source)
            {
                float value = selector(item);
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static float? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float? max = null;
            foreach(TSource item in source)
            {
                float? value = selector(item);
                if (!(value <= max))
                    max = value;
            }
            return max;
        }
    
        public static decimal Max(this IEnumerable<decimal> source)
        {
            return source.Max(x => x);
        }

        public static decimal? Max(this IEnumerable<decimal?> source)
        {
            return source.Max(x => x);
        }

        public static decimal Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal max = decimal.MaxValue;
            foreach(TSource item in source)
            {
                decimal value = selector(item);
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static decimal? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal? max = null;
            foreach(TSource item in source)
            {
                decimal? value = selector(item);
                if (!(value <= max))
                    max = value;
            }
            return max;
        }
    
    }
}