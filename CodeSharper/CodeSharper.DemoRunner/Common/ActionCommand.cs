using System;
using System.Windows.Input;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.DemoRunner.ViewModels
{
    public class ActionCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<Object> _execute;
        public ActionCommand(Action<Object> execute) : this(_ => true, execute) { }

        public ActionCommand(Predicate<Object> canExecute, Action<Object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
            Constraints.NotNull(() => canExecute).NotNull(() => execute);
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}