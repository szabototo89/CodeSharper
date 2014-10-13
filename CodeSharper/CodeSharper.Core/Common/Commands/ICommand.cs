﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public interface ICommand
    {
        Argument Execute(Argument parameters);
    }

    public interface ISupportMultipleArgumentsCommand
    {
        IEnumerable<Argument> Execute(IEnumerable<Argument> parameters);
    }

    public abstract class CommandBase : ICommand, ISupportMultipleArgumentsCommand  
    {
        public abstract Argument Execute(Argument parameters);

        public virtual IEnumerable<Argument> Execute(IEnumerable<Argument> parameters)
        {
            Constraints
                .Argument(() => parameters)
                .NotNull();

            return parameters.Select(Execute);
        }
    }

    public abstract class Command<TIn, TOut> : CommandBase
        where TIn : Argument
        where TOut : Argument
    {
        public override Argument Execute(Argument parameter)
        {
            if (parameter is TIn)
                return Execute((TIn)parameter);

            return Arguments.TypeError<TIn>("Invalid type error!");
        }

        protected abstract TOut Execute(TIn parameter);
    }

    public abstract class CommandWithMultiValueSupport<TIn, TOut> : Command<ValueArgument<TIn>, TOut>
        where TOut : Argument
    {
        public override Argument Execute(Argument parameter)
        {
            var parameters = parameter as MultiValueArgument<TIn>;

            if (parameters != null)
            {
                var results = parameters.Values
                    .Select(value => Execute(Arguments.Value(value)))
                    .OfType<ValueArgument<TIn>>()
                    .Select(argument => argument.Value)
                    .ToArray();

                return Arguments.MultiValue(results);
            }

            return base.Execute(parameter);
        }
    }

    public abstract class ValueCommand<T> : Command<ValueArgument<T>, ValueArgument<T>> { }

    public abstract class ValueCommandWithMultiValueSupport<T> : ValueCommand<T>
    {
        public override Argument Execute(Argument parameter)
        {
            var parameters = parameter as MultiValueArgument<T>;

            if (parameters != null)
            {
                var results = parameters.Values
                                        .Select(value => Execute(Arguments.Value(value)))
                                        .Select(argument => argument.Value)
                                        .ToArray();

                return Arguments.MultiValue(results);
            }

            return base.Execute(parameter);
        }
    }
}