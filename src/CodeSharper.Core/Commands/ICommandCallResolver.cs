using System;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCallResolver
    {
        /// <summary>
        /// Creates the command.
        /// </summary>
        Command CreateCommand(CommandCallDescriptor descriptor);
    }
}