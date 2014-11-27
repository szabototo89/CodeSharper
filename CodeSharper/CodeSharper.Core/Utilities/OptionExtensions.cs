using System;

namespace CodeSharper.Core.Utilities
{
    /// <summary>
    /// Helper class for Option(T) class
    /// </summary>
    public static class OptionHelper
    {
        /// <summary>
        /// Converts any value to Option(T) type
        /// </summary>
        public static Option<TValue> ToOption<TValue>(this TValue value)
        {
            return new Option<TValue>(value);
        }

        public static Option<TValue> Or<TValue>(this Option<TValue> left, Option<TValue> right)
        {
            if (left != Option.None)
                return left;

            return right;
        }

        public static Option<Object> Or<TLeft, TRight>(this Option<TLeft> left, Option<TRight> right)
        {
            if (left != Option.None)
                return Option.Some<Object>(left.Value);

            if (right != Option.None)
                return Option.Some<Object>(right.Value);

            return Option.None;
        }
     }
}