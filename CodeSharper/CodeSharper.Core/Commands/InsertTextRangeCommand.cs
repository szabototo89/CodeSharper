using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Commands
{
    public class InsertTextRangeCommand : CommandBase
    {
        private String _value;
        private Int32 _startIndex;
        private IRunnable _runnable;
        private readonly string ARGUMENT_VALUE = "value";
        private readonly string ARGUMENT_START_INDEX = "startIndex";

        public InsertTextRangeCommand(CommandDescriptor descriptor) : base(descriptor)
        {
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var resolver = new CommandArgumentResolver(Descriptor, arguments);
            resolver
                .UpdateArgument(ref _value, ARGUMENT_VALUE)
                .UpdateArgument(ref _startIndex, ARGUMENT_START_INDEX);
        }

        protected override void CreateRunnable()
        {
            base.CreateRunnable();
            _runnable = new InsertTextRangeRunnable(_startIndex, _value);
        }

        public override IRunnable GetRunnable()
        {
            return _runnable;
        }
    }
}