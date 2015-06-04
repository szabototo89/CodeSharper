using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    // [Consumes(typeof(MultiValueConsumer<Object>))]
    public class LengthRunnable : RunnableBase<Object, IEnumerable<Object>>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(Object parameter)
        {
            if (parameter is String)
            {
                var text = (String)parameter;
                return new Object[] { text.Length };
            }
            if (parameter is IEnumerable)
            {
                var enumerable = ((IEnumerable)parameter).Cast<Object>();
                return new Object[] { enumerable.Count() };
            }
            if (parameter is TextRange)
            {
                var textRange = (TextRange)parameter;
                return new Object[] {textRange.Length};
            }

            return Enumerable.Empty<Object>();
        }
    }
}