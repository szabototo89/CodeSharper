using System;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class SplitStringCommandFactory : CommandFactoryBase
    {
        private String _separator;
        private readonly string ARGUMENT_SEPARATOR = "separator";

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var resolver = new CommandArgumentResolver(Descriptor, arguments);
            resolver
                .UpdateArgument(ref _separator, ARGUMENT_SEPARATOR);
        }

        protected override IRunnable CreateRunnable()
        {
            return new SplitStringRunnable(_separator);
        }
    }
}