using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(FlattenArgumentAfter<TextRange>))]
    public class FilterTextByColumnRunnable : Runnable<TextRange, IEnumerable<TextRange>>
    {
        public int Column { get; protected set; }

        public string Separator { get; protected set; }

        public FilterTextByColumnRunnable(Int32 column) : this(column, " ") { }

        public FilterTextByColumnRunnable(Int32 column, String separator)
        {
            Constraints.IsGreaterThan(() => column, -1);
            Column = column;
            Separator = separator;
        }

        public override IEnumerable<TextRange> Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);

            var lineSeparator = new SplitStringRunnable(Environment.NewLine);
            var columnSeparator = new SplitStringRunnable(Separator);
            var lines = lineSeparator.Run(parameter);
            var result = lines
                .Select(line =>
                {
                    var columns = columnSeparator.Run(line).ToArray();
                    if (columns.Length < Column)
                        return null;

                    return columns[Column];
                })
                .Where(column => column != null)
                .ToArray();

            return result;
        }
    }
}