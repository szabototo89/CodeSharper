using System;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentUnwrapper<Object>)), Produces(typeof(ValueArgumentWrapper<Object>))]
    [Consumes(typeof(MultiValueArgumentUnwrapper<Object>)), Produces(typeof(MultiValueArgumentWrapper<Object>))]
    public class IdentityRunnable : Runnable<Object, Object>
    {
        public override Object Run(Object parameter)
        {
            return parameter;
        }
    }
}