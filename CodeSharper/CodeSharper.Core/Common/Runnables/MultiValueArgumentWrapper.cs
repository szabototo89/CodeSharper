using System.Collections.Generic;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public class MultiValueArgumentWrapper<TParameter> 
        : ArgumentWrapper<IEnumerable<TParameter>, MultiValueArgument<TParameter>>
    {
        protected override MultiValueArgument<TParameter> Convert(IEnumerable<TParameter> parameter)
        {
            return Arguments.MultiValue(parameter);
        }
    }
}