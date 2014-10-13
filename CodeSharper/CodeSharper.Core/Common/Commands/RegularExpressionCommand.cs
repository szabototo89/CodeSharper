using System;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class RegularExpressionCommand : CommandWithMultiValueSupport<TextRange, MultiValueArgument<TextRange>>
    {
        private readonly Regex _regex;

        public String Pattern { get; set; }

        public RegularExpressionCommand(String pattern)
        {
            Constraints.NotNull(() => pattern);
            Pattern = pattern;
            _regex = new Regex(pattern, RegexOptions.Multiline);
        }

        protected override MultiValueArgument<TextRange> Execute(ValueArgument<TextRange> parameter)
        {
            Constraints.NotNull(() => parameter);

            var range = parameter.Value;

            var matches = _regex.Matches(range.Text)
                         .OfType<Match>()
                         .Select(match => range.SubStringOfText(match.Index, match.Length));
            return Arguments.MultiValue(matches.ToArray());
        }
    }
}