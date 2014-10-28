using System;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    public class FillStringRunnable : StringTransformationRunnable
    {
        public FillStringRunnable(Char character) :
            base(text => FillString(character, text))
        {
        }

        public FillStringRunnable(String fillPattern)
            : base(text => FillString(fillPattern, text)) { }

        private static String FillString(String fillPattern, String text)
        {
            Constraints
                .Argument(() => text)
                .NotNull()
                .Argument(() => fillPattern)
                .NotNull()
                .NotBlank();

            return String.Join(String.Empty, Enumerable.Repeat(fillPattern, text.Length / fillPattern.Length + 1))
                .Substring(0, text.Length);
        }

        private static String FillString(Char character, String text)
        {
            Constraints.NotNull(() => text);

            return new String(character, text.Length);
        }

    }
}