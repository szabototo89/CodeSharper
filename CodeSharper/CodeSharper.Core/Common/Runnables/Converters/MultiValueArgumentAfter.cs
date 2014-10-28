using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentAfter<TParameter> : ArgumentAfter<IEnumerable<TParameter>, MultiValueArgument<TParameter>>
    {
        public override Boolean IsConvertable(Object parameter)
        {
            var result = parameter as IEnumerable;
            if (result == null)
                return false;

            return result.All(element => element is TParameter);
        }

        public override Object Convert(Object parameter)
        {
            return Convert(((IEnumerable)parameter).Cast<TParameter>());
        }

        public override MultiValueArgument<TParameter> Convert(IEnumerable<TParameter> parameter)
        {
            return Arguments.MultiValue(parameter);
        }
    }
}