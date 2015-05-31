using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class GreadyMultiValueConsumer<TParameter> : MultiValueConsumer<TParameter>
    {
        public override Object Convert<TResult>(IEnumerable<TParameter> parameter, Func<TParameter, TResult> function)
        {
            return base.Convert(parameter.ToArray(), function);
        }
    }
}