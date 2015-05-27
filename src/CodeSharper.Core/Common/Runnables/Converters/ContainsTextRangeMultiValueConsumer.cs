using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ContainsTextRangeMultiValueConsumer : ValueConsumerBase<IEnumerable<IHasTextRange>, IEnumerable<TextRange>>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public override Boolean IsConvertable(Object parameter)
        {
            if (parameter is IEnumerable)
            {
                var enumerable = (IEnumerable)parameter;
                return enumerable.Cast<Object>().All(element => element is IHasTextRange);
            }

            return base.IsConvertable(parameter);
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public override Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func)
        {
            var enumerable = (IEnumerable)parameter;
            return Convert(enumerable.Cast<IHasTextRange>(), element => func(element));
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public override Object Convert<TFunctionResult>(IEnumerable<IHasTextRange> parameter, Func<IEnumerable<TextRange>, TFunctionResult> function)
        {
            return function(parameter.Select(param => param.TextRange));
        }
    }
}