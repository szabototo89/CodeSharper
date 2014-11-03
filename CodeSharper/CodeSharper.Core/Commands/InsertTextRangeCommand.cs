using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands
{
    public class InsertTextRangeCommand : CommandBase
    {
        private String _value;
        private Int32 _startIndex;
        private IRunnable _runnable;
        private readonly string ARGUMENT_VALUE = "value";
        private readonly string ARGUMENT_START_INDEX = "startIndex";

        protected override void InitializeCommand(ArgumentDescriptorBuilder builder)
        {
            Descriptor = new CommandDescriptor()
            {
                Name = "InsertTextRangeCommand",
                Arguments = builder
                    .Argument<String>(ARGUMENT_VALUE)
                    .Argument<Int32>(ARGUMENT_START_INDEX)
                    .Create()
            };
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);
            _value = arguments.GetArgument<String>(ARGUMENT_VALUE);
            _startIndex = arguments.GetArgument<Int32>(ARGUMENT_START_INDEX);
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