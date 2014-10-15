using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class SplitStringCommand : CommandWithMultiValueSupport<TextRange, MultiValueArgument<TextRange>>
    {
        public string Separator { get; protected set; }

        public SplitStringCommand(String separator)
        {
            Separator = separator;
        }

        protected override MultiValueArgument<TextRange> Execute(ValueArgument<TextRange> parameter)
        {
            var value = parameter.Value;
            Int32 index = 0,
                  lastIndex = 0;
            var text = value.Text;
            var ranges = new List<TextRange>();

            while ((index = text.IndexOf(Separator, lastIndex, StringComparison.Ordinal)) != -1)
            {
                ranges.Add(value.SubStringOfText(lastIndex, index - lastIndex));
                lastIndex = index + 1;
            }

            ranges.Add(value.SubStringOfText(lastIndex));

            return Arguments.MultiValue(ranges);
        }
    }
}