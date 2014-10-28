using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentBefore<TParameter> : ArgumentBefore<IMultiValueArgument<TParameter>, TParameter>
    {
        public override Boolean IsConvertable(Object parameter)
        {
            var i = 0;
            return base.IsConvertable(parameter);
        }

        public override Object Convert<TFunctionResult>(IMultiValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
        {
            var result = new List<TFunctionResult>();
            foreach (var value in parameter.Values)
            {
                result.Add(func(value));
            }

            return result.AsEnumerable();
        }
    }
}