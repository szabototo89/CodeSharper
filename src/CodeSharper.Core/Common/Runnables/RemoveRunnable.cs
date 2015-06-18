using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Transformation;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(MultiValueConsumer<Object>))]
    public class RemoveRunnable : RunnableBase<Object, Object>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override Object Run(Object parameter)
        {
            var removableElement = parameter as ICanRemove;
            if (removableElement == null) return null;

            removableElement.Remove();

            return null;
        }
    }
}
