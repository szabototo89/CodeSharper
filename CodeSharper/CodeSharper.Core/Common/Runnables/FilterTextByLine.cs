using System;
using System.Collections.Generic;
using System.Data;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentUnwrapper<TextRange>)), Produces(typeof(ValueArgumentWrapper<TextRange>))]
    [Consumes(typeof(MultiValueArgumentUnwrapper<TextRange>)), Produces(typeof(MultiValueArgumentWrapper<TextRange>))]
    public class FilterTextByLine : Runnable<TextRange, TextRange>
    {
        public int Line { get; protected set; }
        
        public string Separator { get; protected set; }

        public FilterTextByLine(Int32 line) : this(line, Environment.NewLine) { }

        public FilterTextByLine(Int32 line, String separator)
        {
            Constraints.IsGreaterThan(() => line, -1);
            Constraints.NotNull(() => separator);
            Line = line;
            Separator = separator;
        }

        public override TextRange Run(TextRange textRange)
        {
            Constraints.NotNull(() => textRange);
            Constraints.NotNull(() => Separator);

            var text = textRange.Text;

            Int32 line = -1, index = -Separator.Length, start = 0;

            while ((index = text.IndexOf(Separator, index + Separator.Length, StringComparison.Ordinal)) != -1 &&
                   line < Line - 1)
            {
                ++line;
                start = index;
            }

            if (index == -1)
                return null;

            return textRange.SubStringOfText(start + Separator.Length, index - start - Separator.Length);
        }
    }
}