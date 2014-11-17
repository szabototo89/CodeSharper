using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class ReplaceTextCommandFactory : CommandFactoryBase
    {
        private String _replacedText;
        private String ARGUMENT_REPLACED_TEXT = "text";

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var resolver = new CommandArgumentResolver(Descriptor, arguments);
            resolver.UpdateArgument(ref _replacedText, ARGUMENT_REPLACED_TEXT);
        }

        protected override IRunnable CreateRunnable()
        {
            return new ReplaceTextRunnable(_replacedText);
        }
    }
}