using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables
{
    public class ElementAtRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("position")]
        public Double Position { get; set; }

        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
                return Enumerable.Empty<Object>();

            var element = parameter.ElementAtOrDefault((Int32)Position);
            if (element == null)
                return Enumerable.Empty<Object>();

            return new[] { element };
        }
    }
}