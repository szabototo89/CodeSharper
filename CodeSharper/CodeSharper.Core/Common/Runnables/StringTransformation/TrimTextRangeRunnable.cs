using System;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    public class TrimTextRangeRunnable : StringTransformationRunnable
    {
        public TrimTextRangeRunnable(TrimOptions options = TrimOptions.TrimBoth)
            : base(text => TrimTextRange(text, options))
        {

        }

        private static String TrimTextRange(String value, TrimOptions options)
        {
            switch (options)
            {
                case TrimOptions.TrimStart:
                    return value.TrimStart();
                case TrimOptions.TrimEnd:
                    return value.TrimEnd();
                case TrimOptions.TrimBoth:
                    return value.Trim();
            }

            return null;
        }
    }
}