using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class ArgumentWrapper<TParameter, TArgument> : IArgumentWrapper
        where TArgument : Argument
    {
        public virtual Boolean IsWrappable(Object parameter)
        {
            return parameter is TParameter;
        }

        public virtual Boolean IsUnwrappable(Argument parameter)
        {
            return parameter is TArgument;
        }

        public Argument Wrap(Object parameter)
        {
            if (IsWrappable(parameter))
                return Wrap((TParameter)parameter);

            return Arguments.TypeError<TArgument>();
        }

        public Object Unwrap(Argument argument)
        {
            if (IsUnwrappable(argument))
                return Unwrap((TArgument)argument);

            return Arguments.TypeError<TArgument>();
        }

        protected abstract TArgument Wrap(TParameter parameter);

        protected abstract TParameter Unwrap(TArgument argument);
    }
}