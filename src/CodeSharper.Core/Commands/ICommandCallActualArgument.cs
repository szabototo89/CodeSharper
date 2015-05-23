using System;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCallActualArgument
    {
        /// <summary>
        /// Gets the value of command call actual parameter
        /// </summary>
        Object Value { get; }
    }
}