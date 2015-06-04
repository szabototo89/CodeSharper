using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    public class TakeRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("count")]
        public Double Count { get; set; }

        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
            {
                return Enumerable.Empty<Object>();
            }
            return parameter.ToArray().Take((Int32) Count);
        }
    }
}