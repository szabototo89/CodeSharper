using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class StringTransformationCommand : ValueCommandWithMultiValueSupport<TextRange>
    {
        private readonly Func<String, String> _transformation;

        public StringTransformationCommand(Func<String, String> transformation)
        {
            Constraints.NotNull(() => transformation);
            _transformation = transformation;
        }

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