using System;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class RegularExpressionCommandFactory : CommandFactoryBase
    {
        private String _pattern;
        private readonly String ARGUMENT_PATTERN = "pattern";

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var resolver = new CommandArgumentResolver(Descriptor, arguments);
            resolver.UpdateArgument(ref _pattern, ARGUMENT_PATTERN);
        }

        protected override IRunnable CreateRunnable()
        {
            return new RegularExpressionRunnable(_pattern);    
        }
    }
}