using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class MultiValueConsumer<TParameter> : ValueConsumerBase<IEnumerable<TParameter>, TParameter>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public override Boolean IsConvertable(Object parameter)
        {
            if (parameter == null) return false;

            if (parameter is IEnumerable<TParameter>)
                return true;

            // checks edge cases
            var enumerable = parameter as IEnumerable;
            if (enumerable == null) return false;
            return enumerable.Cast<Object>().All(element => element is TParameter);
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public override Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func)
        {
            var enumerable = parameter as IEnumerable;
            return Convert(enumerable.Cast<TParameter>(), argument => func(argument));
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