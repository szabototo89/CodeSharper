using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentUnwrapper<TParameter> : ArgumentUnwrapper<IMultiValueArgument<TParameter>, TParameter>
    {
        

        public override Object Unwrap<TFunctionResult>(IMultiValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
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