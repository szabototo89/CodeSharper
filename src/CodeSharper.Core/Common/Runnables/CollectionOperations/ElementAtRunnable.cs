using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    public class ElementAtRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("position")]
        public Double Position { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
            {
                return Enumerable.Empty<Object>();
            }

            var position = (Int32) Position;
            Object element = null;

            if (position >= 0)
            {
                element = parameter.ElementAtOrDefault(position);
            }
            else if (position < 0)
            {
                var elements = parameter.ToArray();
                element = elements[elements.Count() + position];
            }

            if (element != null)
            {
                return new[] {element};
            }

            return Enumerable.Empty<Object>();
        }
    }
}