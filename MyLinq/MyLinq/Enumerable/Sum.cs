using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
    
        public static int Sum(this IEnumerable<int> source)
        {
            return source.Sum(x => x);
        }

        public static int Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static long Sum(this IEnumerable<long> source)
        {
            return source.Sum(x => x);
        }

        public static long Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static double Sum(this IEnumerable<double> source)
        {
            return source.Sum(x => x);
        }

        public static double Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static float Sum(this IEnumerable<float> source)
        {
            return source.Sum(x => x);
        }

        public static float Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static decimal Sum(this IEnumerable<decimal> source)
        {
            return source.Sum(x => x);
        }

        public static decimal Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static int? Sum(this IEnumerable<int?> source)
        {
            return source.Sum(x => x);
        }

        public static int? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            int? sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static long? Sum(this IEnumerable<long?> source)
        {
            return source.Sum(x => x);
        }

        public static long? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            long? sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static double? Sum(this IEnumerable<double?> source)
        {
            return source.Sum(x => x);
        }

        public static double? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            double? sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static float? Sum(this IEnumerable<float?> source)
        {
            return source.Sum(x => x);
        }

        public static float? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            float? sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
        public static decimal? Sum(this IEnumerable<decimal?> source)
        {
            return source.Sum(x => x);
        }

        public static decimal? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            decimal? sum = 0;
            foreach(TSource item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    
    }
}