using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class TakeRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("count")]
        public Double Count { get; set; }

        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            if (parameter == null)
                return Enumerable.Empty<Object>();
            return parameter.ToArray().Take((Int32)Count);
        }
    }

    // [Consumes(typeof(MultiValueConsumer<Object>))]
    public class LengthRunnable : RunnableBase<Object, IEnumerable<Object>>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(Object parameter)
        {
            if (parameter is IEnumerable<Object>)
            {
                var enumerable = (IEnumerable<Object>)parameter;
                yield return enumerable.Count();
            }

            if (parameter is TextRange)
            {
                var textRange = (TextRange) parameter;
                yield return textRange.Length;
            }

            if (parameter is String)
            {
                var text = (String) parameter;
                yield return text.Length;
            }

            yield break;
        }
    }
}