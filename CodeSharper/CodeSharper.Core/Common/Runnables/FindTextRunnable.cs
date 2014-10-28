
using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>))]
    [Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    [Produces(typeof(FlattenArgumentAfter<TextRange>))]
    public class FindTextRunnable : Runnable<TextRange, IEnumerable<TextRange>>
    {
        public String Pattern { get; set; }

        public FindTextRunnable(String pattern)
        {
            Constraints.NotNull(() => pattern);
            Pattern = pattern;
        }

        public override IEnumerable<TextRange> Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);

            var text = parameter.Text;

            var results = new List<TextRange>();
            var index = -Pattern.Length;

            while ((index = text.IndexOf(Pattern, index + Pattern.Length, StringComparison.Ordinal)) != -1)
            {
                results.Add(parameter.SubStringOfText(parameter.Start + index, Pattern.Length));
            }

            return results;
        }
    }
}