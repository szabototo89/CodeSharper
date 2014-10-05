using System;
using System.Diagnostics;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class StringTransformationCommand : ValueCommand<TextRange>
    {
        private readonly Func<string, string> _transformation;

        public StringTransformationCommand(Func<String, String> transformation)
        {
            Constraints.NotNull(() => transformation);
            _transformation = transformation;
        }

        [DebuggerStepThrough]
        protected override ValueArgument<TextRange> Execute(ValueArgument<TextRange> parameter)
        {
            Constraints
                .NotNull(() => parameter);

            var node = parameter.Value;
            node.ReplaceText(_transformation(node.Text));

            return Arguments.Value(node);
        }
    }
}