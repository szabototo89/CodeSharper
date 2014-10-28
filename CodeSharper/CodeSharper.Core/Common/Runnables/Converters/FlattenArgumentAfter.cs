using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class FlattenArgumentAfter<TParameter>
        : ArgumentAfter<IEnumerable<IEnumerable<TParameter>>, MultiValueArgument<TParameter>>
    {
        public override Boolean IsConvertable(Object parameter)
        {
            var result = parameter as IEnumerable;
            if (result == null) return false;
            return result.All(enumerable => enumerable is IEnumerable && ((IEnumerable)enumerable).All(element => element is TParameter));
        }

        public override Object Convert(Object parameter)
        {
            var result = ((IEnumerable) parameter).Cast<IEnumerable<TParameter>>();
            return Convert(result);
        }

        public override MultiValueArgument<TParameter> Convert(IEnumerable<IEnumerable<TParameter>> parameter)
        {
            return Arguments.MultiValue(parameter.SelectMany(value => value));
        }
    }
}