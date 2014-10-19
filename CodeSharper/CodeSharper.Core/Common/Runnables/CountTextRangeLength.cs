using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(ValueArgumentUnwrapper<TextRange>)), Produces(typeof(ValueArgumentWrapper<Int32>))]
    [Consumes(typeof(MultiValueArgumentWrapper<TextRange>)), Produces(typeof(MultiValueArgumentWrapper<Int32>))]
    public class CountTextRangeLength : Runnable<TextRange, int>
    {
        public override Int32 Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);
            return parameter.Text.Length;
        }
    }
}