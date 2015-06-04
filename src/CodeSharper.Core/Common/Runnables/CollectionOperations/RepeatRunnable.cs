using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.CollectionOperations
{
    public class RepeatRunnable : RunnableBase<Object, IEnumerable<Object>>
    {
        /// <summary>
        /// Gets or sets the count of repeating
        /// </summary>
        [Parameter("count")]
        public Double Count { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatRunnable"/> class.
        /// </summary>
        public RepeatRunnable() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatRunnable"/> class.
        /// </summary>
        public RepeatRunnable(Double count)
        {
            Count = count;
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(Object parameter)
        {
            return Enumerable.Repeat(parameter, (Int32)Count);
        }
    }
}