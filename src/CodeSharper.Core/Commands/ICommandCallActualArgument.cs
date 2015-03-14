using System;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCallActualArguments
    {
        /// <summary>
        /// Gets the value of command call actual parameter
        /// </summary>
        Object Value { get; }
    }
}