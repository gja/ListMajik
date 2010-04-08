using System;
using System.Collections.Generic;

namespace ListMajik
{
    public static class WrappingEnumerator
    {
        public static IEnumerator<T> GetEnumerator<S, T>(this IEnumerable<S> enumerable, Func<S, T> mapping)
        {
            return GetEnumerable(enumerable, mapping).GetEnumerator();
        }

        private static IEnumerable<T> GetEnumerable<S, T>(IEnumerable<S> enumerable, Func<S, T> mapping)
        {
            foreach (var item in enumerable)
            {
                yield return mapping(item);
            }
        }
    }
}
