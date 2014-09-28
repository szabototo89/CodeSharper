using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class FindTextCommand : ICommand
    {
        public string Value { get; protected set; }

        public FindTextCommand(string value)
        {
            Constraints.NotBlank(() => value);

            Value = value;
        }

        protected Argument Execute(TextNode node)
        {
            var document = node.Parent;

            var results = new List<TextNode>();
            var index = -Value.Length;

            while ((index = document.Text.IndexOf(Value, index + Value.Length, StringComparison.Ordinal)) != -1)
            {
                results.Add(document.SubStringOfText(index, index + Value.Length));
            }

            return Arguments.Value(results as IEnumerable<TextNode>);
        }

        public Argument Execute(Argument parameter)
        {
            Constraints
                .Argument(() => parameter).NotNull();

            if (parameter == null)
                return Arguments.Error("Input for find command must be a non-null value!");

            if (parameter is ValueArgument<TextNode>)
                return Execute(((ValueArgument<TextNode>)parameter).Value);

            return Arguments.Error("Input type error of FindCommand");
        }
    }
}