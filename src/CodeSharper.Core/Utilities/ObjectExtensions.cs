using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Security;
using System.Security.Policy;

namespace CodeSharper.Core.Utilities
{
    /// <summary>
    /// Extension methods for general types
    /// </summary>
    public static class ObjectExtensions
    {
        public static TValue With<TValue>(this TValue value, Action<TValue> function)
        {
            function(value);
            return value;
        }

        public static TResult With<TValue, TResult>(this TValue value, Func<TValue, TResult> function)
        {
            return function(value);
        }

        public static TResult Safe<TValue, TResult>(this TValue value, Func<TValue, TResult> function)
            where TValue : class
        {
            if (value != null)
                return function(value);

            return default(TResult);
        }

        public static TTo As<TTo>(this Object value)
            where TTo : class
        {
            return value as TTo;
        }
    }
}
