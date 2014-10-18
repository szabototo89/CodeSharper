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
        public override MultiValueArgument<TParameter> Wrap(IEnumerable<TParameter> parameter)
        {
            return Arguments.MultiValue(parameter);
        }
    }
}