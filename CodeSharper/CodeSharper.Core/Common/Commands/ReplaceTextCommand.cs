using System;

namespace CodeSharper.Core.Common.Commands
{
    public class ReplaceTextCommand : StringTransformationCommand
    {
        public ReplaceTextCommand(String replacedText)
            : base(text => ReplaceText(replacedText))
        {
        }

        private static String ReplaceText(String replacedText)
        {
            return replacedText;
        }
    }
}