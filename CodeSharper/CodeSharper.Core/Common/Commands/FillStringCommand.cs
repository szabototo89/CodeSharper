using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using Microsoft.Win32.SafeHandles;

namespace CodeSharper.Core.Common.Commands
{
    public class FillStringCommand : StringTransformationCommand
    {
        public FillStringCommand(char character) :
            base(text => FillString(character, text))
        {
        }

        public FillStringCommand(string fillPattern)
            : base(text => FillString(fillPattern, text)) { }

        private static string FillString(string fillPattern, string text)
        {
            Constraints
                .Argument(() => text)
                    .NotNull()
                .Argument(() => fillPattern)
                    .NotNull()
                    .NotBlank();

            return string.Join(string.Empty, Enumerable.Repeat(fillPattern, text.Length / fillPattern.Length))
                         .Substring(0, text.Length);
        }

        private static string FillString(char character, string text)
        {
            Constraints.NotNull(() => text);

            return new string(character, text.Length);
        }
    }
}