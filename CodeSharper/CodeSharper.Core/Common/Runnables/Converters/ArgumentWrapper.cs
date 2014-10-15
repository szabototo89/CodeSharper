using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentWrapper<TParameter, TArgument> : IArgumentWrapper
        where TArgument : Argument
    {
        public Boolean IsWrappable(Object parameter)
        {
            return parameter is TParameter;
        }

        Object IArgumentWrapper.Wrap(Object parameter)
        {
            return Wrap((TParameter)parameter);
        }

        public abstract TArgument Wrap(TParameter parameter);
    }
}