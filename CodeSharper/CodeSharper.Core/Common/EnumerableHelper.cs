using System;
using System.Collections;

namespace CodeSharper.Core.Common
{
    public static class EnumerableHelper
    {
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