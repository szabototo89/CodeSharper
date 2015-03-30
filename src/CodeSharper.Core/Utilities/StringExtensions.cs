using System;

namespace CodeSharper.Core.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces the format item in a specified String with the String representation of a corresponding Object in a specified array.
        /// </summary>
        public static String Format(this String format, params Object[] parameters)
        {
            return String.Format(format, parameters);
        }
    }
}