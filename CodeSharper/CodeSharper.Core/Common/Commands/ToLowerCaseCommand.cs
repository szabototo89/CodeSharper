using System;

namespace CodeSharper.Core.Common.Commands
{
    public class ToLowerCaseCommand : StringTransformationCommand
    {
        public ToLowerCaseCommand()
            : base(text => text.ToLowerInvariant())
        {
        }
    }
}