using System;

namespace CodeSharper.Core.Commands
{
    public interface ICommandManager
    {
        void RegisterCommand<TCommand>() where TCommand: ICommand, new();

        void UnregisterCommand<TCommand>() where TCommand: ICommand, new();
        
        Boolean IsCommandRegistered<TCommand>() where TCommand: ICommand, new();
    }
}