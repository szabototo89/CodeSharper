using System;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    public class StringTransformationRunnable : Runnable<TextRange, TextRange>
    {
        private readonly Func<TextRange, TextRange> _transformation;

        public StringTransformationRunnable(Func<String, String> transformation)
            : this(parameter => parameter.ReplaceText(transformation(parameter.Text)))
        {
        }

        public StringTransformationRunnable(Func<TextRange, TextRange> transformation)
        {
            Constraints.NotNull(() => transformation);
            _transformation = transformation;
        }

        public override TextRange Run(TextRange parameter)
        {
            return _transformation(parameter);
        }
    }

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