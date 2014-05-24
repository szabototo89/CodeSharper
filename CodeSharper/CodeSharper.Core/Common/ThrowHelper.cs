using System;

namespace CodeSharper.Core.Common
{
    static internal class ThrowHelper
    {
        public static Exception ArgumentNullException(string argumentName)
        {
            return new ArgumentNullException(argumentName);
        }

        public static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        public static void ThrowArgumentException(string argumentName = "", string message = "")
        {
            throw new ArgumentException(message, argumentName);
        }

        public static Exception ArgumentException(string argumentName = "", string message = "")
        {
            return new ArgumentException(message, argumentName);
        }
    }
}