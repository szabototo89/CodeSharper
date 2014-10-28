using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<Object>)), Produces(typeof(ValueArgumentAfter<IEnumerable<Object>>))]
    public class RepeatRunnable : Runnable<Object, Object>
    {
        private readonly int _count;

        public RepeatRunnable(Int32 count)
        {
            _count = count;
        }

        public override Object Run(Object parameter)
        {
            return Enumerable.Repeat(parameter, _count);
        }
    }
}