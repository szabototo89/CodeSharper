using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class FlattenArgumentWrapper<TParameter>
        : ArgumentWrapper<IEnumerable<IEnumerable<TParameter>>, MultiValueArgument<TParameter>>
    {
        public override MultiValueArgument<TParameter> Wrap(IEnumerable<IEnumerable<TParameter>> parameter)
        {
            return Arguments.MultiValue(parameter.SelectMany(value => value));
        }
    }
}