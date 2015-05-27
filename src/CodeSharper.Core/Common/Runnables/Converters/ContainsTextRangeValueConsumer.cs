using System;
using System.Linq.Expressions;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ContainsTextRangeValueConsumer : ValueConsumerBase<IHasTextRange, TextRange>
    {
        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public override Object Convert<TFunctionResult>(IHasTextRange parameter, Func<TextRange, TFunctionResult> function)
        {
            return function(parameter.TextRange);
        }
    }
}