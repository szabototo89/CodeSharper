using System;
using System.Collections.Generic;
using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public interface ICommandManager
    {
        StandardCommandManager RegisterCommandFactory(ICommandFactory commandFactory);
        Option<ICommand> TryGetCommand(CommandCallDescriptor callDescriptor);
        IEnumerable<ICommandFactory> TryGetCommandFactoriesByName(String name);
    }
}