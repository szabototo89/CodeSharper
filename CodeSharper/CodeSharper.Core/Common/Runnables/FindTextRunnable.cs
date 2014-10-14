
using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    public class FindTextRunnable : Runnable<TextRange, IEnumerable<TextRange>>
    {
        public String Pattern { get; set; }

        public FindTextRunnable(String pattern)
        {
            Constraints.NotNull(() => pattern);

            Pattern = pattern;
        }

        public override IEnumerable<TextRange> Run(TextRange range)
        {
            Constraints.NotNull(() => range);

            var document = range.Text;

            var results = new List<TextRange>();
            var index = -Pattern.Length;

            while ((index = document.IndexOf(Pattern, index + Pattern.Length, StringComparison.Ordinal)) != -1)
            {
                results.Add(range.SubStringOfText(index, Pattern.Length));
            }

            return results;
        }
    }
}