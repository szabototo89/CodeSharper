using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentBefore<TParameter>: ArgumentBefore<IValueArgument<TParameter>, TParameter>
    {
        public override Object Convert<TFunctionResult>(IValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
        {
            return func(parameter.Value);
        }
    }
}