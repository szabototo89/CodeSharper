using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Utilities
{
    public static class ObjectHelper
    {
        public static T With<T>(this T element, Action<T> withStatement)
        {
            withStatement(element);
            return element;
        }

        public static T Safe<T>(this T value) where T : new()
        {
            return !Equals(value, null) ? value : new T();
        }

        public static T SafeOrDefault<T>(this T value, T defaultValue)
        {
            return !Equals(value, null) ? value : defaultValue;
        }

        public static T[] AsArray<T>(this T that)
        {
            return new[] { that };
        }

        public static List<T> WrapToList<T>(this T that)
        {
            return new List<T>(new[] { that });
        }

        public static Boolean Is<T>(this Object that)
        {
            return that is T;
        }

        public static T As<T>(this Object that) where T : class
        {
            return that as T;
        }

        public static T To<T>(this Object that)
        {
            return (T)that;
        }

        public static IEnumerable<T> Repeat<T>(this T value, Int32 count)
        {
            for (Int32 i = 0; i < count; i++) {
                yield return value;
            }
        }
    }
}
