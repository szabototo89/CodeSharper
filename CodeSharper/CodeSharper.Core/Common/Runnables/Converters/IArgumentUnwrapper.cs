using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentUnwrapper
    {
        Boolean IsUnwrappable(Object parameter);

        Object Unwrap<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func);
    }

    public interface IArgumentUnwrapper<in TArgument, out TParameter>
    {
        Object Unwrap<TFunctionResult>(TArgument parameter, Func<TParameter, TFunctionResult> func);
    }
}