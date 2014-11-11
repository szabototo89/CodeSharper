using System;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class FindTextCommandFactory : CommandFactoryBase
    {
        private FindTextRunnable _runnable;

        private String _pattern;

        protected override IRunnable CreateRunnable()
        {
            return new FindTextRunnable(_pattern);
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);
            _pattern = arguments.GetArgumentValue<String>("pattern");
        }
    }
}