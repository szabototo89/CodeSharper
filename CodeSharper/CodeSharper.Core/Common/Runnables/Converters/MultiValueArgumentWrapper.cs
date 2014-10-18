using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueArgumentWrapper<TParameter> 
        : ArgumentWrapper<IEnumerable<TParameter>, MultiValueArgument<TParameter>>
    {
        public override Boolean IsWrappable(Object parameter)
        {
            if (base.IsWrappable(parameter))
                return true;

            var array = parameter as IEnumerable;
            return array != null && array.OfType<TParameter>().Any();
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