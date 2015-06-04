using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.CollectionRunnables
{
    [Consumes(typeof (MultiValueConsumer<IEnumerable<Object>>))]
    public class FilterRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("pattern")]
        public String Pattern { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> elements)
        {
            foreach (var element in elements)
            {
                String input = null;

                if (element is IHasTextRange)
                {
                    var node = (IHasTextRange) element;
                    input = node.TextRange.GetText();
                }
                else if (element is TextRange)
                {
                    input = ((TextRange) element).GetText();
                }
                else if (element is String)
                {
                    input = (String) element;
                }

                if (input != null && Regex.IsMatch(input, Pattern))
                {
                    yield return element;
                }
            }
        }
    }
}