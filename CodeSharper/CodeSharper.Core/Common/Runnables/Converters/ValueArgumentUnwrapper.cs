using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentUnwrapper<TParameter>: ArgumentUnwrapper<ValueArgument<TParameter>, TParameter>
    {
        public override Object Unwrap<TFunctionResult>(ValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
        {
            return func(parameter.Value);
        }
    }
}