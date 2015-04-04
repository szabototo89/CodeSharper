using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables.Attributes;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(Object)), Produces(typeof(IEnumerable<Object>))]
    public class RepeatRunnable : Runnable<Object, Object>
    {
        /// <summary>
        /// Gets or sets the count of repeating
        /// </summary>
        public Int32 Count { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatRunnable"/> class.
        /// </summary>
        public RepeatRunnable(Int32 count)
        {
            Count = count;
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override Object Run(Object parameter)
        {
            return Enumerable.Repeat(parameter, Count);
        }
    }
}