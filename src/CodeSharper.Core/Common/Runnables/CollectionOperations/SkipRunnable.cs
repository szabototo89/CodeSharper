using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    public class SkipRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("count")]
        public Double Count { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
            {
                return Enumerable.Empty<Object>();
            }
            return parameter.Skip((Int32) Count);
        }
    }
}