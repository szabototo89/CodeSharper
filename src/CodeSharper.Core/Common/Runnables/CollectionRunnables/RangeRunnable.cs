using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    public class RangeRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        [Parameter("start")]
        public Double Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [Parameter("end")]
        public Double End { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
                return Enumerable.Empty<Object>();
            return parameter.Skip((Int32)Start).Take((Int32)(End - Start));
        }
    }
}