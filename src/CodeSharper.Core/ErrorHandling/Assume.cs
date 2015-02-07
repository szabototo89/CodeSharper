using System;
using System.Diagnostics;

namespace CodeSharper.Core.ErrorHandling
{
    public static class Assume
    {
        [DebuggerStepThrough]
        public static void NotNull<TValue>(TValue value, String parameter) where TValue : class
        {
            if (value == null)
                throw new ArgumentNullException(parameter, String.Format("{0} cannot be null!", parameter));
        }

        [DebuggerStepThrough]
        public static void IsTrue(Boolean condition, String message)
        {
            if (!condition)
                throw new Exception(message);
        }

        [DebuggerStepThrough]
        public static void IsTrue(Boolean condition, Func<Exception> exception)
        {
            if (!condition)
                throw exception();
        }

    }
}
