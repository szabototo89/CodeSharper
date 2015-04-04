using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueConsumer<TParameter> : ValueConsumerBase<IEnumerable<TParameter>, TParameter>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public override Boolean IsConvertable(Object parameter)
        {
            return parameter is IEnumerable<TParameter>;
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public override Object Convert<TResult>(IEnumerable<TParameter> parameter, Func<TParameter, TResult> func)
        {
            var result = new List<TResult>();
            foreach (var value in parameter)
            {
                result.Add(func(value));
            }
            return result;
        }
    }
}