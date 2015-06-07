using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class GreadyEnumerableValueProducer : ValueProducerBase<IEnumerable, IEnumerable<Object>>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public override Boolean IsConvertable(Object parameter)
        {
            return parameter is IEnumerable;
        }

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        public override IEnumerable<Object> Convert(IEnumerable parameter)
        {
            return parameter.Cast<Object>().ToArray();
        }
    }
}