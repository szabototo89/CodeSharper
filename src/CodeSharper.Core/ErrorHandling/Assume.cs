using System;
using System.Diagnostics;
using System.IO;

namespace CodeSharper.Core.ErrorHandling
{
    public static class Assume
    {
        [DebuggerStepThrough]
        public static void NotNull<TValue>(TValue value, String parameter) where TValue : class
        {
            if (value == null)
                throw new ArgumentNullException(parameter, $"{parameter} cannot be null.");
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
