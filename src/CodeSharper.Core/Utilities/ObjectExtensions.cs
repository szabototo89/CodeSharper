using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection.Emit;
using System.Security;
using System.Security.Policy;
using CodeSharper.Core.Nodes.Selectors;

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

        public static TValue Cast<TValue>(this Object value)
        {
            return (TValue)value;
        }

        public static Boolean Is<TType>(this Object value)
        {
            return value is TType;
        }

        public static Boolean Is<TType1, TType2>(this Object value)
        {
            return value is TType1 && value is TType2;
        }

        public static TValue As<TValue>(this Object value)
            where TValue : class
        {
            return value as TValue;
        }

        public static Boolean IsNumeric(this Object expression)
        {
            if (expression == null)
                return false;

            Double number;
            return Double.TryParse(Convert.ToString(expression, CultureInfo.InvariantCulture), System.Globalization.NumberStyles.Any, NumberFormatInfo.InvariantInfo, out number);
        }

        public static Boolean Is<T>(this Object value, Action<T> function)
        {
            if (value is T)
            {
                function((T)value);
                return true;
            }

            return false;
        }

        public static TResult SafeInvoke<TParameter, TResult>(this Func<TParameter, TResult> function, TParameter parameter)
        {
            if (function != null)
                return function(parameter);
            return default(TResult);
        }

    }
}
