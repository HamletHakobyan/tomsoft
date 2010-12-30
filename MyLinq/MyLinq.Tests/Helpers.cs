using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MyLinq.Tests
{
    static class Helpers
    {
        public static void AssertSequenceEqual<T>(this IEnumerable<T> actual, params T[] expected)
        {
            bool result = actual.SequenceEqual(expected);
            if (result)
                return;
            string sActual = actual.ToCommaSeparatedList();
            string sExpected = expected.ToCommaSeparatedList();
            Assert.Fail("Expected {{{0}}}, but was {{{1}}}", sActual, sExpected);
        }

        public static string ToCommaSeparatedList<T>(this IEnumerable<T> source)
        {
            return source.ToCommaSeparatedList(8);
        }

        public static string ToCommaSeparatedList<T>(this IEnumerable<T> source, int count)
        {
            return source.ToCommaSeparatedList(count, ToStringOrNullString);
        }

        public static string ToCommaSeparatedList<T>(this IEnumerable<T> source, int count, Func<T, string> formatter)
        {
            StringBuilder sb = new StringBuilder();
            int n = 0;
            foreach (var item in source)
            {
                if (n > count)
                    break;

                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(formatter(item));

                n++;
            }
            return sb.ToString();
        }

        private static string ToStringOrNullString<T>(T value)
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            return value == null
// ReSharper restore CompareNonConstrainedGenericWithNull
                ? "(null)"
                : value.ToString();
        }
    }
}
