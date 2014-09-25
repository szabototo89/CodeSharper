using System;
using System.Diagnostics;

namespace CodeSharper.Core.Common
{
    [DebuggerStepThrough]
    internal static class ThrowHelper
    {
        [DebuggerStepThrough]
        public static Exception ArgumentNullException(string argumentName)
        {
            return new ArgumentNullException(argumentName);
        }

        [DebuggerStepThrough]
        public static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        [DebuggerStepThrough]
        public static void ThrowArgumentException(string argumentName = "", string message = "")
        {
            throw new ArgumentException(message, argumentName);
        }

        [DebuggerStepThrough]
        public static Exception ArgumentException(string argumentName = "", string message = "")
        {
            return new ArgumentException(message, argumentName);
        }

        [DebuggerStepThrough]
        public static Exception InvalidOperationException(string message = "")
        {
            return new InvalidOperationException(message);
        }
    }
}