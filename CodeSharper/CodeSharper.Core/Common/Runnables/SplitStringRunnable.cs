using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentUnwrapper<TextRange>)), Produces(typeof(MultiValueArgumentWrapper<TextRange>))]
    [Consumes(typeof(MultiValueArgumentUnwrapper<TextRange>)), Produces(typeof(FlattenArgumentWrapper<TextRange>))]
    public class SplitStringRunnable : Runnable<TextRange, IEnumerable<TextRange>>
    {
        public string Separator { get; protected set; }

        public SplitStringRunnable(String separator)
        {
            Separator = separator;
        }

        public override IEnumerable<TextRange> Run(TextRange parameter)
        {
            Int32 index = 0,
                lastIndex = 0;
            var text = parameter.Text;
            var ranges = new List<TextRange>();

            while ((index = text.IndexOf(Separator, lastIndex, StringComparison.Ordinal)) != -1)
            {
                ranges.Add(parameter.SubStringOfText(parameter.Start + lastIndex, index - lastIndex));
                lastIndex = index + Separator.Length;
            }

            ranges.Add(parameter.SubStringOfText(parameter.Start + lastIndex, parameter.Text.Length - lastIndex));

            return ranges;
        }
    }
}