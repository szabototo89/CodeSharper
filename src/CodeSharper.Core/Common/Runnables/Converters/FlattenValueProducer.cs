using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class FlattenValueProducer<TParameter> : ValueProducerBase<IEnumerable<IEnumerable<TParameter>>, IEnumerable<TParameter>>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public override Boolean IsConvertable(Object parameter)
        {
            return parameter is IEnumerable<IEnumerable<TParameter>>;
        }

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        public override Object Convert(Object parameter)
        {
            return Convert(parameter as IEnumerable<IEnumerable<TParameter>>);
        }

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        public override IEnumerable<TParameter> Convert(IEnumerable<IEnumerable<TParameter>> parameter)
        {
            return parameter.SelectMany(element => element);
        }
    }
}