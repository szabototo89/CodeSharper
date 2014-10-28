using System;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<Object>)), Produces(typeof(ValueArgumentAfter<Object>))]
    [Consumes(typeof(MultiValueArgumentBefore<Object>)), Produces(typeof(MultiValueArgumentAfter<Object>))]
    public class IdentityRunnable : Runnable<Object, Object>
    {
        public override Object Run(Object parameter)
        {
            return parameter;
        }
    }
}