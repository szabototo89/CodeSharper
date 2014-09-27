using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class StringTransformationCommand : ValueCommand<TextNode>
    {
        private readonly Func<string, string> _transformation;

        public StringTransformationCommand(Func<String, String> transformation)
        {
            Constraints.NotNull(() => transformation);
            _transformation = transformation;
        }

        protected override ValueArgument<TextNode> Execute(ValueArgument<TextNode> parameter)
        {
            Constraints
                .NotNull(() => parameter);

            var node = parameter.Value;
            node.Text = _transformation(node.Text);

            return Arguments.Value(node);
        }
    }
}