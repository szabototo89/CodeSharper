using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Commands
{
    public class StandardCommandManager : ICommandManager
    {
        private readonly List<Type> _commands;

        public StandardCommandManager()
        {
            _commands = new List<Type>();
        }

        public void RegisterCommand<TCommand>()
            where TCommand : ICommand, new()
        {
            if (IsCommandRegistered<TCommand>())
                ThrowHelper.ThrowArgumentException("TCommand");

            _commands.Add(typeof(TCommand));
        }

        public void UnregisterCommand<TCommand>() where TCommand : ICommand, new()
        {
            if (!IsCommandRegistered<TCommand>())
                ThrowHelper.ThrowArgumentException("TCommand");

            _commands.Remove(typeof(TCommand));
        }

        public Boolean IsCommandRegistered<TCommand>() where TCommand : ICommand, new()
        {
            return _commands.Contains(typeof(TCommand));
        }
    }
}
