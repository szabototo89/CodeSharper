using System;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class FindTextCommand : CommandBase
    {
        private FindTextRunnable _runnable;

        private String _pattern;

        public FindTextCommand(CommandDescriptor descriptor) : base(descriptor)
        {
        }

        protected override void CreateRunnable()
        {
            _runnable = new FindTextRunnable(_pattern);
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);
            _pattern = arguments.GetArgument<String>("pattern");
        }

        public override IRunnable GetRunnable()
        {
            return _runnable;
        }
    }
}