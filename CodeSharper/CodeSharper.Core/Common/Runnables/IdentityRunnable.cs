using System;

namespace CodeSharper.Core.Common.Runnables
{
    public class IdentityRunnable : Runnable<object, object>
    {
        public override Object Run(Object parameter)
        {
            return parameter;
        }
    }
}