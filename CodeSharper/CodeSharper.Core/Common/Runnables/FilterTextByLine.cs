using System;
using System.Collections.Generic;
using System.Data;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<IEnumerable<TextRange>>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(FlattenArgumentAfter<TextRange>))]
    public class FilterTextByLine : Runnable<TextRange, IEnumerable<TextRange>>
    {
        public FilterPosition FilterPosition { get; protected set; }

        public int Line { get; protected set; }

        public string Separator { get; protected set; }

        public FilterTextByLine(Int32 line) : this(line, Environment.NewLine) { }

        public FilterTextByLine(FilterPosition position) : this(position, Environment.NewLine) { }

        public FilterTextByLine(Int32 line, String separator)
            : this(FilterPositions.ByLine(line), separator)
        {
            
        }

        public FilterTextByLine(FilterPosition position, String separator)
        {
            Constraints.NotNull(() => position);
            Constraints.NotNull(() => separator);
            FilterPosition = position;
            Separator = separator;
        }

        public override IEnumerable<TextRange> Run(TextRange textRange)
        {
            Constraints.NotNull(() => textRange);

            var text = textRange.Text;

            Int32 line = -1, index = -Separator.Length, start = 0;

            var list = new List<TextRange>();

            while ((index = text.IndexOf(Separator, index + Separator.Length, StringComparison.Ordinal)) != -1)
            {
                line = line + 1;

                if (FilterPosition.Filter(line))
                {
                    list.Add(line == 0
                        ? textRange.SubStringOfText(start, index)
                        : textRange.SubStringOfText(start + Separator.Length, index - start - Separator.Length));
                }

                start = index;
            }

            line = line + 1;
            if (FilterPosition.Filter(line))
            {
                list.Add(line == 0
                    ? textRange.SubStringOfText(start, index)
                    : textRange.SubStringOfText(start + Separator.Length, text.Length - start - Separator.Length));
            }

            return list;
            // return new[] { textRange.SubStringOfText(start + Separator.Length, index - start - Separator.Length) };
        }
    }
}