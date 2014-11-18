using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Tests.Core.Mocks
{
    public static partial class RunnableMocks
    {
        [Consumes(typeof(ValueArgumentBefore<Object>)), Produces(typeof(ValueArgumentAfter<IEnumerable<Object>>))]
        public class TestRunnable : Runnable<Object, Object>
        {
            [BindTo("count")]
            public Int32 Count { get; set; }

            public override Object Run(Object parameter)
            {
                return Enumerable.Repeat(parameter, Count);
            }
        }
    }
}
