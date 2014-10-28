using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentAfter<TParameter, TArgument> : IArgumentAfter, IArgumentAfter<TParameter, TArgument>
        where TArgument : Argument
    {
        public virtual Boolean IsConvertable(Object parameter)
        {
            return parameter is TParameter;
        }

        public virtual Object Convert(Object parameter)
        {
            return Convert((TParameter)parameter);
        }

        public abstract TArgument Convert(TParameter parameter);
    }
}