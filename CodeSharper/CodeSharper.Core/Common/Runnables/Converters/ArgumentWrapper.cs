using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentWrapper<TParameter, TArgument> : IArgumentWrapper, IArgumentWrapper<TParameter, TArgument>
        where TArgument : Argument
    {
        public virtual Boolean IsWrappable(Object parameter)
        {
            return parameter is TParameter;
        }

        public virtual Object Wrap(Object parameter)
        {
            return Wrap((TParameter)parameter);
        }

        public abstract TArgument Wrap(TParameter parameter);
    }
}