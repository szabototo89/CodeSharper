using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common.Values
{
    public interface IMultiValueArgument
    {
        /// <summary>
        /// Value of argument object
        /// </summary>
        IEnumerable<Object> Values { get; }
    }

    public interface IMultiValueArgument<out TValue>
    {
        /// <summary>
        /// Value of argument object
        /// </summary>
        IEnumerable<TValue> Values { get; }
    }

}