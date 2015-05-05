using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(MultiValueConsumer<IEnumerable<Object>>))]
    public class FilterRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        [Parameter("pattern")]
        public String Pattern { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> nodes)
        {
            foreach (var element in nodes.OfType<Node>())
            {
                if (Regex.IsMatch(element.TextRange.GetText(), Pattern))
                {
                    yield return element;
                }
            }
        }
    }
}