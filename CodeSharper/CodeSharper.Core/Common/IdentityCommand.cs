﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class IdentityCommand : ICommand
    {
        public Argument Execute(Argument parameter)
        {
            return parameter;
        }
    }

    public class ComposeCommand : ICommand
    {
        private readonly IEnumerable<ICommand> _commands;

        public ComposeCommand() : this(Enumerable.Empty<ICommand>().ToArray())
        {
            
        }

        public ComposeCommand(params ICommand[] commands)
        {
            Constraints.NotNull(() => commands);

            _commands = commands;
        }

        public Argument Execute(Argument parameter)
        {
            Argument argument = parameter;
            foreach (var command in _commands)
                argument = command.Execute(argument);

            return argument;
        }
    }
}
