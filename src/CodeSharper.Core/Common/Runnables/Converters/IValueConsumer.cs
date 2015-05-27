using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IValueConsumer
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        Boolean IsConvertable(Object parameter);

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func);
    }

    public interface IValueConsumer<in TArgument, out TParameter>
    {
        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        Object Convert<TFunctionResult>(TArgument parameter, Func<TParameter, TFunctionResult> function);
    }
}