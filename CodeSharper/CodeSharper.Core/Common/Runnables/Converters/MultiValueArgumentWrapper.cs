using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentWrapper<TParameter> : ArgumentWrapper<IEnumerable<TParameter>, MultiValueArgument<TParameter>>
    {
        public override Boolean IsWrappable(Object parameter)
        {
            var result = parameter as IEnumerable;
            if (result == null)
                return false;

            return result.All(element => element is TParameter);
        }

        public override Object Wrap(Object parameter)
        {
            return Wrap(((IEnumerable)parameter).Cast<TParameter>());
        }

        public override MultiValueArgument<TParameter> Wrap(IEnumerable<TParameter> parameter)
        {
            return Arguments.MultiValue(parameter);
        }
    }
}