using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentAfter
    {
        Boolean IsConvertable(Object parameter);

        Object Convert(Object parameter);
    }

    public interface IArgumentAfter<in TParameter, out TArgument>
        where TArgument : Argument
    {
        TArgument Convert(TParameter parameter);
    }
}