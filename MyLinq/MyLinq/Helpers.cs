using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static class Helpers
    {
        public static void CheckArgumentNotNull<T>(this T value, string paramName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void CheckArgumentGreaterThanOrEqual<T>(this T value, string paramName, T min)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(value, min) < 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    string.Format(
                        "{0} should be greater than or equal to {1}",
                        paramName,
                        min));
            }
        }

        public static void CheckArgumentLessThanOrEqual<T>(this T value, string paramName, T max)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(value, max) > 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    string.Format(
                        "{0} should be less than or equal to {1}",
                        paramName,
                        max));
            }
        }
    }
}
