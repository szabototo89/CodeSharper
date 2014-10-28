using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentBefore
    {
        Boolean IsConvertable(Object parameter);

        Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func);
    }

    public interface IArgumentBefore<in TArgument, out TParameter>
    {
        Object Convert<TFunctionResult>(TArgument parameter, Func<TParameter, TFunctionResult> func);
    }
}