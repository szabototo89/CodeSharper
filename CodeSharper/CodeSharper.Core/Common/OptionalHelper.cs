namespace CodeSharper.Core.Common
{
    /// <summary>
    /// Helper class for Optional(T) class
    /// </summary>
    public static class OptionalHelper
    {
        /// <summary>
        /// Converts any value to Optional(T) type
        /// </summary>
        public static Optional<TType> ToOptional<TType>(this TType value)
        {
            return new Optional<TType>(value);
        } 
    }
}