using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<Int32>))]
    [Consumes(typeof(MultiValueArgumentAfter<TextRange>)), Produces(typeof(MultiValueArgumentAfter<Int32>))]
    public class CountTextRangeLengthRunnable : Runnable<TextRange, int>
    {
        public override Int32 Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);
            return parameter.Text.Length;
        }
    }
}