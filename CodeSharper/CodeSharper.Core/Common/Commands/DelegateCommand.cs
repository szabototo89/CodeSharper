using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<Argument, Argument> _function;

        public DelegateCommand(Func<Argument, Argument> function)
        {
            Constraints
                .NotNull(() => function);
            _function = function;
        }

        public Argument Execute(Argument parameter)
        {
            return _function(parameter);
        }
    }
}