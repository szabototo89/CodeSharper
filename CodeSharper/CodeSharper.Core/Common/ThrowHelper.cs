using System;

namespace CodeSharper.Core.Common
{
    static internal class ThrowHelper
    {
        public static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        public static void ThrowArgumentException(string argumentName, string message = "")
        {
            throw new ArgumentException(message, argumentName);
        }
    }
}