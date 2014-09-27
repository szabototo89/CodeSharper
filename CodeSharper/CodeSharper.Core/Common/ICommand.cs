using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public interface ICommand
    {
        Argument Execute(Argument parameter);
    }

    public abstract class Command<TIn, TOut> : ICommand
        where TIn : Argument
        where TOut : Argument
    {
        public virtual Argument Execute(Argument parameter)
        {
            if (parameter is TIn)
                return Execute((TIn)parameter);

            return Arguments.TypeError<TIn>("Invalid type error!");
        }

        protected abstract TOut Execute(TIn parameter);
    }

    public abstract class ValueCommand<T> : Command<ValueArgument<T>, ValueArgument<T>> 
    {

    }

    public class MultiCommand : ICommand
    {
        private readonly ICommand _command;

        public MultiCommand(ICommand command)
        {
            Constraints
                .NotNull(() => command);
            _command = command;
        }

        public Argument Execute(Argument parameter)
        {
            var values = parameter as MultiValueArgument;
            if (values != null)
                return new MultiValueArgument(ExecuteValues(values));

            return Arguments.TypeError<MultiValueArgument>();
        }

        private IEnumerable<Argument> ExecuteValues(MultiValueArgument values)
        {
            foreach (var value in values.Source)
                yield return _command.Execute(value);
        }
    }
}