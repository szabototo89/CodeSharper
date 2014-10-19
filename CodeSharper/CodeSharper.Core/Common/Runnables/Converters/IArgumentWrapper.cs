using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentWrapper
    {
        Boolean IsWrappable(Object parameter);

        Object Wrap(Object parameter);
    }

    public interface IArgumentWrapper<in TParameter, out TArgument>
        where TArgument : Argument
    {
        TArgument Wrap(TParameter parameter);
    }
}