using System;
using System.Diagnostics;
using System.IO;

namespace CodeSharper.Core.ErrorHandling
{
    public static class Assume
    {
        [DebuggerStepThrough]
        public static void NotNull<TValue>(TValue value, String parameterName)
            where TValue : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
        }

        [DebuggerStepThrough]
        public static void IsRequired<TValue>(TValue value, String parameterName = null)
            where TValue : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName, $"{parameterName} is required.");
        }

        [DebuggerStepThrough]
        public static void IsRequired(Boolean condition, String parameterName)
        {
            if (!condition)
                throw new ArgumentException($"{parameterName} is required.");
        }

        [DebuggerStepThrough]
        public static void IsRequired(Boolean condition, Func<Exception> exception)
        {
            if (!condition)
                throw exception();
        }

        public static void NotBlank(String name, String message)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException($"{name} cannot be blank.");
        }

        public static void FileExists(String path, String message)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Missing specified file!", path);
        }
    }
}