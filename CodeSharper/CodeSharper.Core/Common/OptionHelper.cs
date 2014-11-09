using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common
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
    }
}