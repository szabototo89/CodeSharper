using System.Collections.Generic;

namespace CodeSharper.Core.Common.Values
{
    public interface IMultiValueArgument
    {
        /// <summary>
        /// Value of argument object
        /// </summary>
        IEnumerable<object> Values { get; }
    }
}