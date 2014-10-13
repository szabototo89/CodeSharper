using System;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class RegularExpressionCommand : ValueCommandWithMultiValueSupport<TextRange>
    {
        private readonly Regex _regex;

        public String Pattern { get; set; }

        public RegularExpressionCommand(String pattern)
        {
            Constraints.NotNull(() => pattern);
            Pattern = pattern;
            _regex = new Regex(pattern, RegexOptions.Multiline);
        }

        protected override ValueArgument<TextRange> Execute(ValueArgument<TextRange> parameter)
        {
            Constraints.NotNull(() => parameter);

            foreach (Match match in _regex.Matches(parameter.Value.Text))
            {
                
            }

            throw new NotImplementedException();
        }
    }
}