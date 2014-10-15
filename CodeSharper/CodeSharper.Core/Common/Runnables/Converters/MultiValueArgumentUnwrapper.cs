using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentUnwrapper<TParameter> : ArgumentUnwrapper<MultiValueArgument<TParameter>, TParameter>
    {
        public override Object Unwrap<TFunctionResult>(MultiValueArgument<TParameter> parameter, Func<TParameter, TFunctionResult> func)
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