using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    public class InsertTextRangeRunnable : StringTransformationRunnable
    {
        public InsertTextRangeRunnable(Int32 startIndex, String value) 
            : base(text => text.Insert(startIndex, value))
        {
            Constraints.NotNull(() => value);
            Constraints.IsGreaterThan(() => startIndex, -1);
        }
    }
}