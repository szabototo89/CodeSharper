using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentUnwrapper<TParameter>: ArgumentUnwrapper<IValueArgument<TParameter>, TParameter>
    {
        public override Object Unwrap<TFunctionResult>(IValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
        {
            return func(parameter.Value);
        }
    }
}