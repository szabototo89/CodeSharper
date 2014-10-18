using System;

namespace CodeSharper.Core.Common.Values
{
    public interface IValueArgument
    {
        /// <summary>
        /// Value of argument object
        /// </summary>
        Object Value { get; }
    }

    public interface IValueArgument<out TValue>
    {
        TValue Value { get; }
    }
} 