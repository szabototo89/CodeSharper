using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeSharper.Core.Common
{
    public static class EnumerableHelper
    {
        public static void Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> function)
        {
            foreach (var element in source)
                function(element);
        }

        public static void Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource, Int32> function)
        {
            var index = 0;
            foreach (var element in source)
            {
                function(element, index);
                ++index;
            }
        }

        public static Boolean All(this IEnumerable enumerable, Predicate<Object> predicate)
        {
            foreach (var element in enumerable)
            {
                if (!predicate(element))
                    return false;
            }
            return true;
        }

        public static Boolean Any(this IEnumerable enumerable, Predicate<Object> predicate)
        {
            foreach (var element in enumerable)
            {
                if (predicate(element))
                    return true;
            }
            return false;
        }
    }
}