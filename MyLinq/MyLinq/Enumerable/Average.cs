using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
    
        public static int Average(this IEnumerable<int> source)
        {
            return source.Average(x => x);
        }

        public static int Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static long Average(this IEnumerable<long> source)
        {
            return source.Average(x => x);
        }

        public static long Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static double Average(this IEnumerable<double> source)
        {
            return source.Average(x => x);
        }

        public static double Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static float Average(this IEnumerable<float> source)
        {
            return source.Average(x => x);
        }

        public static float Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static decimal Average(this IEnumerable<decimal> source)
        {
            return source.Average(x => x);
        }

        public static decimal Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static int? Average(this IEnumerable<int?> source)
        {
            return source.Average(x => x);
        }

        public static int? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int? sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static long? Average(this IEnumerable<long?> source)
        {
            return source.Average(x => x);
        }

        public static long? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long? sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static double? Average(this IEnumerable<double?> source)
        {
            return source.Average(x => x);
        }

        public static double? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double? sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static float? Average(this IEnumerable<float?> source)
        {
            return source.Average(x => x);
        }

        public static float? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float? sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
        public static decimal? Average(this IEnumerable<decimal?> source)
        {
            return source.Average(x => x);
        }

        public static decimal? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal? sum = 0;
            int n = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
                n++;
            }
            return sum / n;
        }
    
    }
}