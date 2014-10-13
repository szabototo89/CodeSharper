using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class FindTextCommand : CommandBase
    {
        public string Value { get; protected set; }

        public FindTextCommand(string value)
        {
            Constraints.NotBlank(() => value);

            Value = value;
        }

        protected MultiValueArgument<TextRange> Execute(TextRange range)
        {
            Constraints.NotNull(() => range);

            var document = range.Text;

            var results = new List<TextRange>();
            var index = -Value.Length;

            while ((index = document.IndexOf(Value, index + Value.Length, StringComparison.Ordinal)) != -1)
            {
                results.Add(range.SubStringOfText(index, Value.Length));
            }

            return Arguments.MultiValue(results);
        }

        public override Argument Execute(Argument parameter)
        {
            Constraints
                .Argument(() => parameter).NotNull();

            if (parameter == null)
                return Arguments.Error("Input for find command must be a non-null value!");

            if (parameter is MultiValueArgument<TextRange>)
            {
                var values = ((MultiValueArgument<TextRange>) parameter).Values;
                var result = values.Select(Execute).SelectMany(multiValue => multiValue.Values).ToArray();
                return Arguments.MultiValue(result);
            }

            if (parameter is ValueArgument<TextRange>)
                return Execute(((ValueArgument<TextRange>)parameter).Value);

            return Arguments.Error("Input type error of FindCommand");
        }
    }
}