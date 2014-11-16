using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    [Consumes(typeof(ValueArgumentBefore<TextRange>)), Produces(typeof(ValueArgumentAfter<TextRange>))]
    [Consumes(typeof(MultiValueArgumentBefore<TextRange>)), Produces(typeof(MultiValueArgumentAfter<TextRange>))]
    public class ReplaceTextRunnable : StringTransformationRunnable
    {
        private readonly String _replacedText;

        public ReplaceTextRunnable(String replacedText)
        {
            _replacedText = replacedText;
        }

        public override TextRange Run(TextRange parameter)
        {
            Constraints.NotNull(() => parameter);
            parameter.ReplaceText(_replacedText.Replace("$", parameter.Text));
            return parameter;
        }
    }
}