using System;
using System.Diagnostics;

namespace CodeSharper.Core.Utilities
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

        public static void ThrowException<TException>()
            where TException : Exception, new()
        {
            throw new TException();
        }

        public static void ThrowException(String message)
        {
            throw new Exception(message);
        }

        public static void ThrowException()
        {
            ThrowException<Exception>();
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