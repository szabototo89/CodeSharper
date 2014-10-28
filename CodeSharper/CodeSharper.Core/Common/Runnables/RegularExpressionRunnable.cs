using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(FlattenArgumentAfter<TextRange>))]
    public class RegularExpressionRunnable : Runnable<TextRange, IEnumerable<TextRange>>
    {
        private readonly Regex _regex;

        public String Pattern { get; set; }

        public RegularExpressionRunnable(String pattern)
        {
            Constraints.NotNull(() => pattern);
            Pattern = pattern;
            _regex = new Regex(pattern, RegexOptions.Multiline);
        }

        public override IEnumerable<TextRange> Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);
            var matches = _regex.Matches(parameter.Text)
                .OfType<Match>()
                .Select(match => parameter.SubStringOfText(parameter.Start + match.Index, match.Length));
            return matches.ToArray();
        }
    }
}