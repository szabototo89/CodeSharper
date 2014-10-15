using System;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
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

            return String.Join(String.Empty, Enumerable.Repeat(fillPattern, text.Length / fillPattern.Length))
                .Substring(0, text.Length);
        }

        private static String FillString(Char character, String text)
        {
            Constraints.NotNull(() => text);

            return new String(character, text.Length);
        }

    }
}