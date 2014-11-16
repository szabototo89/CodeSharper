using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    public class StringTransformationRunnable : Runnable<TextRange, TextRange>
    {
        private readonly Func<TextRange, TextRange> _transformation;

        protected StringTransformationRunnable() { }

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
}